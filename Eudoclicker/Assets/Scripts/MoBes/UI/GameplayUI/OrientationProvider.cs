using nsIOrientation2Provider;
using nsIOrientation4Provider;
using System;
using UnityEngine;

namespace nsOrientationProvider
{
    public class OrientationProvider : MonoBehaviour, IOrientation2Provider, IOrientation4Provider
    {
        private DeviceOrientation _orientation;

        public event Action<Orientation2> OnOrientation2Change;
        public event Action<Orientation4> OnOrientation4Change;

        private void Start()
        {
            _orientation = DeviceOrientation.Unknown;
            OnRectTransformDimensionsChange();
        }

        private void OnRectTransformDimensionsChange()
        {
            switch (Input.deviceOrientation)
            {
                case DeviceOrientation.Unknown:
                case DeviceOrientation.FaceUp:
                case DeviceOrientation.FaceDown:
                    OnOrientation2Change?.Invoke(Orientation2.Unknown);
                    OnOrientation4Change?.Invoke(Orientation4.Unknown);
                    break;
                case DeviceOrientation.Portrait:
                case DeviceOrientation.PortraitUpsideDown:
                case DeviceOrientation.LandscapeLeft:
                case DeviceOrientation.LandscapeRight:
                default:
                    InvokeEvents();
                    break;
            }
        }

        private void InvokeEvents()
        {
            if (_orientation != Input.deviceOrientation)
            {
                _orientation = Input.deviceOrientation;
                Orientation2 orientation2 = Orientation2.Unknown;
                Orientation4 orientation4 = Orientation4.Unknown;
                switch (Input.deviceOrientation)
                {
                    case DeviceOrientation.Portrait:
                        orientation2 = Orientation2.Portrait;
                        orientation4 = Orientation4.PortraitUp;
                        break;
                    case DeviceOrientation.PortraitUpsideDown:
                        orientation2 = Orientation2.Portrait;
                        orientation4 = Orientation4.PortraitDown;
                        break;
                    case DeviceOrientation.LandscapeLeft:
                        orientation2 = Orientation2.Landscape;
                        orientation4 = Orientation4.LandscapeLeft;
                        break;
                    case DeviceOrientation.LandscapeRight:
                        orientation2 = Orientation2.Landscape;
                        orientation4 = Orientation4.LandscapeRight;
                        break;
                }
                OnOrientation2Change?.Invoke(orientation2);
                OnOrientation4Change?.Invoke(orientation4);
            }
        }
    }
}
