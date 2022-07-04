using nsAudioButton;

namespace nsSoundButton
{
    public class SoundButton : AudioButton
    {
        protected override void OnClick()
        {
            _musicAndSounds.ToggleSound();
        }

        private void Awake()
        {
            _musicAndSounds.OnSoundToggle += SetImage;
        }
    }
}
