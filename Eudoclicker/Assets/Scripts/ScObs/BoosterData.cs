using UnityEngine;

namespace nsBoosterData
{
    [CreateAssetMenu(menuName = "ScObs/BoosterData")]
    public class BoosterData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _buttonSprite;

        public string Name => _name;
        public Sprite ButtonSprite => _buttonSprite;
    }
}
