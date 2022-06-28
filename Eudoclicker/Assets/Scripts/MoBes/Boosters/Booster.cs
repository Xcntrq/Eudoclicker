using nsBoosterCondition;
using nsBoosterData;
using nsBoosterEffect;
using System;
using System.Collections;
using UnityEngine;

namespace nsBooster
{
    public class Booster : MonoBehaviour
    {
        [SerializeField] private BoosterData _boosterData;
        [SerializeField] private BoosterCondition[] _boosterConditions;
        [SerializeField] private BoosterEffect[] _boosterEffects;

        public BoosterData BoosterData => _boosterData;

        public event Action<float> OnReadyPercentagesCheck;

        private void Awake()
        {
            _boosterConditions = GetComponentsInChildren<BoosterCondition>();
            _boosterEffects = GetComponentsInChildren<BoosterEffect>();
        }

        private void Start()
        {
            StartCoroutine(CheckReadyPercentages());
        }

        private IEnumerator CheckReadyPercentages()
        {
            for (; ; )
            {
                float currentReadyPercentage;
                float lowestReadyPercentage = float.MaxValue;
                foreach (BoosterCondition boosterCondition in _boosterConditions)
                {
                    currentReadyPercentage = boosterCondition.ReadyPercentage;
                    if (currentReadyPercentage < lowestReadyPercentage) lowestReadyPercentage = currentReadyPercentage;
                }
                OnReadyPercentagesCheck?.Invoke(lowestReadyPercentage);
                yield return new WaitForSeconds(.1f);
            }
        }

        public bool AreAllConditionsSatisfied(out string allMessages)
        {
            bool areAllConditionsSatisfied = true;
            allMessages = string.Empty;

            foreach (BoosterCondition boosterCondition in _boosterConditions)
            {
                if (!boosterCondition.IsSatisfied(out string message))
                {
                    allMessages = (allMessages == string.Empty) ? message : string.Concat(allMessages, "<br>", message);
                    areAllConditionsSatisfied = false;
                }
            }

            if (!areAllConditionsSatisfied) return false;

            foreach (BoosterCondition boosterCondition in _boosterConditions)
            {
                boosterCondition.Proc(out string message);
                allMessages = (allMessages == string.Empty) ? message : string.Concat(allMessages, "<br>", message);
            }

            return true;
        }

        public void ProcAllEffects(out string allMessages)
        {
            allMessages = string.Empty;
            foreach (BoosterEffect boosterEffect in _boosterEffects)
            {
                boosterEffect.Proc(out string message);
                allMessages = (allMessages == string.Empty) ? message : string.Concat(allMessages, "<br>", message);
            }
        }
    }
}
