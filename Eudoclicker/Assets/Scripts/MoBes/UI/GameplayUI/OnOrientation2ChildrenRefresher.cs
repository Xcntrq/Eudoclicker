using nsIOrientation2Provider;
using nsOrientationProvider;
using System.Collections;
using UnityEngine;

namespace nsOnOrientation2ChildrenRefresher
{
    public class OnOrientation2ChildrenRefresher : MonoBehaviour
    {
        private IOrientation2Provider _orientation2Provider;

        private void Awake()
        {
            _orientation2Provider = FindObjectOfType<OrientationProvider>(false);
        }

        private void OnEnable()
        {
            _orientation2Provider.OnOrientation2Change += Orientation2Provider_OnOrientation2Change;
        }

        private void OnDisable()
        {
            _orientation2Provider.OnOrientation2Change -= Orientation2Provider_OnOrientation2Change;
        }

        private void Orientation2Provider_OnOrientation2Change(Orientation2 orientation2)
        {
            StopAllCoroutines();
            StartCoroutine(RefreshChildren());
        }

        IEnumerator RefreshChildren()
        {
            yield return 1;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            yield return 1;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
