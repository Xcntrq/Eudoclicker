using nsMobSpawner;
using TMPro;
using UnityEngine;

namespace nsMobCountText
{
    public class MobCountText : MonoBehaviour
    {
        [SerializeField] private MobSpawner _mobSpawner;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _mobSpawner.OnMobCountChange += MobSpawner_OnMobCountChange; ;
        }

        private void MobSpawner_OnMobCountChange(string text)
        {
            _text.SetText(text);
        }
    }
}
