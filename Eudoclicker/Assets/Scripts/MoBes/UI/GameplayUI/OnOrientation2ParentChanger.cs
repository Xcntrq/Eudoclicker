using nsIOrientation2Provider;
using nsOrientationProvider;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace nsOnOrientation2ParentChanger
{
    [Serializable]
    public struct Orientation2Action
    {
        [SerializeField] private Orientation2 _orientation2;
        [SerializeField] private Transform _parentTransform;

        public Orientation2 Orientation2 => _orientation2;
        public Transform ParentTransform => _parentTransform;
    }

    public class OnOrientation2ParentChanger : MonoBehaviour
    {
        [SerializeField] private List<Orientation2Action> _items;

        private IOrientation2Provider _orientation2Provider;

        private void Awake()
        {
            _orientation2Provider = FindObjectOfType<OrientationProvider>(false);
            _orientation2Provider.OnOrientation2Change += Orientation2Provider_OnOrientation2Change;
        }

        private void Orientation2Provider_OnOrientation2Change(Orientation2 orientation2)
        {
            foreach (Orientation2Action orientation2Action in _items)
            {
                if ((orientation2Action.Orientation2 == orientation2) && (orientation2Action.ParentTransform != null)) transform.SetParent(orientation2Action.ParentTransform, false);
            }
        }
    }
}
