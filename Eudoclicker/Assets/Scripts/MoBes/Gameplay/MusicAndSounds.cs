using UnityEngine;
using System;
using nsGameStateSwitch;
using nsMobSpawner;
using nsPlayerInput;
using nsMob;
using nsBoosterPanel;
using nsICoinDropper;
using nsIKillable;
using System.Collections;
using nsIDamageable;
using nsVolumedClip;
using nsOnDeathEventArgs;
using nsOnCoinDropEventArgs;
using nsOnHealthDecreaceEventArgs;

namespace nsMusicAndSounds
{
    public class MusicAndSounds : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameStateSwitch _gameStateSwitch;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private MobSpawner _mobSpawner;
        [SerializeField] private BoosterPanel[] _boosterPanels;

        [Header("Music")]
        [SerializeField] private bool _isMusicEnabled;
        [SerializeField] [Range(0f, 1f)] private float _musicVolumeWhenPaused;
        [SerializeField] private VolumedClip _musicClip;

        [Header("Sounds")]
        [SerializeField] private bool _isSoundEnabled;
        [SerializeField] [Range(0f, 1f)] private float _soundVolume;
        [SerializeField] private VolumedClip _gameoverClip;

        private AudioSource _musicSource;

        public event Action<bool> OnMusicToggle;
        public event Action<bool> OnSoundToggle;

        private void Awake()
        {
            if (!PlayerPrefs.HasKey("_isMusicEnabled")) PlayerPrefs.SetInt("_isMusicEnabled", 1);
            if (!PlayerPrefs.HasKey("_isSoundEnabled")) PlayerPrefs.SetInt("_isSoundEnabled", 1);
            PlayerPrefs.Save();
            _isMusicEnabled = PlayerPrefs.GetInt("_isMusicEnabled") == 1;
            _isSoundEnabled = PlayerPrefs.GetInt("_isSoundEnabled") == 1;
            _musicSource = GetComponent<AudioSource>();
            _gameStateSwitch.OnGameStateChange += GameStateSwitch_OnGameStateChange;
            _mobSpawner.OnMobCreate += MobSpawner_OnMobCreate;
            _playerInput.OnPewPew += PlayerInput_OnPewPew;
            foreach (BoosterPanel boosterPanel in _boosterPanels)
            {
                boosterPanel.OnButtonClick += BoosterPanel_OnButtonClick;
            }
        }

        private void Start()
        {
            OnMusicToggle?.Invoke(_isMusicEnabled);
            OnSoundToggle?.Invoke(_isSoundEnabled);
            _musicSource.volume = _musicClip.Volume;
            _musicSource.loop = true;
            CheckIfMusicShouldPlay();
        }

        private void CheckIfMusicShouldPlay()
        {
            if (_musicSource.isPlaying == _isMusicEnabled) return;
            if (!_isMusicEnabled) _musicSource.Stop();
            if (_isMusicEnabled) PlayMusic();
        }

        private void PlayMusic()
        {
            if (!_musicClip.AudioClip || !_musicSource) return;
            _musicSource.Stop();
            _musicSource.clip = _musicClip.AudioClip;
            _musicSource.Play();
        }

        public void ToggleMusic()
        {
            _isMusicEnabled = !_isMusicEnabled;
            CheckIfMusicShouldPlay();
            OnMusicToggle?.Invoke(_isMusicEnabled);
            PlayerPrefs.SetInt("_isMusicEnabled", _isMusicEnabled ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void ToggleSound()
        {
            _isSoundEnabled = !_isSoundEnabled;
            OnSoundToggle?.Invoke(_isSoundEnabled);
            PlayerPrefs.SetInt("_isSoundEnabled", _isSoundEnabled ? 1 : 0);
            PlayerPrefs.Save();
        }

        private void PlayClip(VolumedClip clip)
        {
            if (!_isSoundEnabled || (clip == null)) return;
            float volumeClamped = Mathf.Clamp(_soundVolume * clip.Volume, 0.05f, 1f);
            AudioSource.PlayClipAtPoint(clip.AudioClip, transform.position, volumeClamped);
        }

        private void GameStateSwitch_OnGameStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Playing:
                    _musicSource.volume = _musicClip.Volume;
                    break;
                case GameState.Paused:
                    _musicSource.volume = _musicVolumeWhenPaused;
                    break;
                case GameState.Over:
                    _musicSource.volume = _musicVolumeWhenPaused;
                    StartCoroutine(DelayClip(_gameoverClip, .8f));
                    break;
            }
        }

        private void PlayerInput_OnPewPew(OnPewPewEventArgs onPewPewEventArgs)
        {
            PlayClip(onPewPewEventArgs.VolumedClip);
        }

        private void BoosterPanel_OnButtonClick(OnButtonClickEventArgs onButtonClickEventArgs)
        {
            PlayClip(onButtonClickEventArgs.VolumedClip);
        }

        private void MobSpawner_OnMobCreate(Mob mob)
        {
            PlayClip(mob.SpawnClip);
            if (mob is IDamageable damageable) damageable.OnHealthDecreace += Damageable_OnHealthDecreace;
            if (mob is ICoinDropper coinDropper) coinDropper.OnCoinDrop += CoinDropper_OnCoinDrop;
            if (mob is IKillable killable) killable.OnDeath += Killable_OnDeath;
        }

        private void Damageable_OnHealthDecreace(OnHealthDecreaceEventArgs onHealthDecreaceEventArgs)
        {
            PlayClip(onHealthDecreaceEventArgs.VolumedClip);
        }

        private void CoinDropper_OnCoinDrop(OnCoinDropEventArgs onCoinDropEventArgs)
        {
            StartCoroutine(DelayClip(onCoinDropEventArgs.VolumedClip, .5f));
        }

        private void Killable_OnDeath(OnDeathEventArgs onDeathEventArgs)
        {
            PlayClip(onDeathEventArgs.VolumedClip);
        }

        private IEnumerator DelayClip(VolumedClip volumedClip, float delay)
        {
            yield return new WaitForSeconds(delay);
            PlayClip(volumedClip);
        }
    }
}
