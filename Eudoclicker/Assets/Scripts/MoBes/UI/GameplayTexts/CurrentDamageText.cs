using nsDamage;
using TMPro;
using UnityEngine;

namespace nsCurrentDamageText
{
    public class CurrentDamageText : MonoBehaviour
    {
        [SerializeField] private Damage _damage;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _damage.OnValueChange += Damage_OnValueChange;
        }

        private void Damage_OnValueChange(int damage)
        {
            _text.SetText(damage.ToString());
        }
    }
}
