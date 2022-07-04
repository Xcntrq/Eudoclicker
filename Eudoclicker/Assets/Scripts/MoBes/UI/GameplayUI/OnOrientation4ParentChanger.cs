using nsIOrientation4Provider;
using nsOrientationProvider;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace nsOnOrientation4ParentChanger
{
    [Serializable]
    public struct Orientation4Action
    {
        [SerializeField] private Orientation4 _orientation4;
        [SerializeField] private Transform _parentTransform;

        public Orientation4 Orientation4 => _orientation4;
        public Transform ParentTransform => _parentTransform;
    }

    public class OnOrientation4ParentChanger : MonoBehaviour
    {
        [SerializeField] private List<Orientation4Action> _items;

        private IOrientation4Provider _orientation4Provider;

        private void Awake()
        {
            _orientation4Provider = FindObjectOfType<OrientationProvider>(false);
            _orientation4Provider.OnOrientation4Change += Orientation4Provider_OnOrientation4Change;
        }

        private void Orientation4Provider_OnOrientation4Change(Orientation4 orientation4)
        {
            foreach (Orientation4Action orientation4Action in _items)
            {
                if ((orientation4Action.Orientation4 == orientation4) && (orientation4Action.ParentTransform != null)) transform.SetParent(orientation4Action.ParentTransform, false);
            }
        }
    }
}
