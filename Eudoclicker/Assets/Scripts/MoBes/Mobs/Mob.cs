using nsILevelable;
using nsIMobBehaviour;
using System.Collections.Generic;
using UnityEngine;

namespace nsMob
{
    public abstract class Mob : MonoBehaviour
    {
        protected IMobBehaviour[] _mobBehaviours;
        protected List<ILevelable> _otherLevelables;

        protected void SetMobBehavioursActive(bool value)
        {
            foreach (IMobBehaviour mobBehaviour in _mobBehaviours)
            {
                mobBehaviour.SetComponentActive(value);
            }
        }

        protected List<ILevelable> GetOtherLevelables()
        {
            List<ILevelable> otherLevelables = new List<ILevelable>();
            ILevelable[] allLevelables = GetComponentsInChildren<ILevelable>();
            foreach (ILevelable levelable in allLevelables)
            {
                if (!levelable.Equals(this)) otherLevelables.Add(levelable);
            }
            return otherLevelables;
        }

        protected void UpdateOtherLevelables(int level)
        {
            foreach (ILevelable levelable in _otherLevelables)
            {
                levelable.SetLevel(level);
            }
        }
    }
}
