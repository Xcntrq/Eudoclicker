using nsAudioButton;

namespace nsMusicButton
{
    public class MusicButton : AudioButton
    {
        protected override void OnClick()
        {
            _musicAndSounds.ToggleMusic();
        }

        private void Awake()
        {
            _musicAndSounds.OnMusicToggle += SetImage;
        }
    }
}
