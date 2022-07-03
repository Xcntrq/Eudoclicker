using nsBoosterList;
using UnityEditor;
using UnityEngine;

namespace nsBoosterListEditor
{
    [CustomEditor(typeof(BoosterList))]
    [CanEditMultipleObjects]
    public class BoosterListEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            BoosterList boosterList = (BoosterList)target;

            if (GUILayout.Button("PopulateArrays"))
            {
                boosterList.PopulateArrays();
            }

            base.OnInspectorGUI();
        }
    }
}
