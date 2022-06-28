using nsMobSpawner;
using TMPro;
using UnityEngine;

namespace nsSpawnTimerText
{
    public class SpawnTimerText : MonoBehaviour
    {
        [SerializeField] private MobSpawner _mobSpawner;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _mobSpawner.OnSpawnTimerChange += MobSpawner_OnSpawnTimerChange;
        }

        private void MobSpawner_OnSpawnTimerChange(string text)
        {
            _text.SetText(text);
        }
    }
}
