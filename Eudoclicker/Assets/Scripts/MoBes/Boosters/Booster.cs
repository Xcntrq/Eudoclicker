using nsBoosterCondition;
using nsBoosterData;
using nsBoosterEffect;
using nsIBooster;
using nsICooldownCondition;
using System;
using System.Collections;
using UnityEngine;

namespace nsBooster
{
    public class Booster : MonoBehaviour, IBooster
    {
        [SerializeField] private float _updateFreq;
        [SerializeField] private BoosterData _boosterData;
        [SerializeField] private BoosterCondition[] _boosterConditions;
        [SerializeField] private BoosterEffect[] _boosterEffects;

        public BoosterData BoosterData => _boosterData;

        public Sprite ButtonSprite => _boosterData.ButtonSprite;

        public event Action<float> OnReadyPercentagesCheck;
        public event Action<float> OnHighestCooldownCheck;

        private void Awake()
        {
            if (_boosterConditions.Length == 0) _boosterConditions = GetComponentsInChildren<BoosterCondition>();
            if (_boosterEffects.Length == 0) _boosterEffects = GetComponentsInChildren<BoosterEffect>();
        }

        private void Start()
        {
            StartCoroutine(CheckConditions());
        }

        private IEnumerator CheckConditions()
        {
            for (; ; )
            {
                float currentCooldown;
                float currentReadyPercentage;
                float lowestReadyPercentage = (_boosterConditions.Length == 0) ? 1f : float.MaxValue;
                float highestCooldown = (_boosterConditions.Length == 0) ? 0f : float.MinValue;
                foreach (BoosterCondition boosterCondition in _boosterConditions)
                {
                    currentReadyPercentage = boosterCondition.ReadyPercentage;
                    if (currentReadyPercentage < lowestReadyPercentage) lowestReadyPercentage = currentReadyPercentage;

                    if (boosterCondition is ICooldownCondition cooldownCondition)
                    {
                        currentCooldown = cooldownCondition.SecondsLeft;
                        if (currentCooldown > highestCooldown) highestCooldown = currentCooldown;
                    }
                }
                OnReadyPercentagesCheck?.Invoke(lowestReadyPercentage);
                OnHighestCooldownCheck?.Invoke(highestCooldown);
                yield return new WaitForSeconds(_updateFreq);
            }
        }

        public bool AreAllConditionsSatisfied(out string allMessages)
        {
            bool areAllConditionsSatisfied = _boosterConditions.Length != 0;
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
                if (message == string.Empty) continue;
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
