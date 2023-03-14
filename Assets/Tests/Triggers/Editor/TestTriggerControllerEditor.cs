using System;
using UnityEditor;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Tests.Triggers.Editor
{
    [CustomEditor(typeof(TestTriggerController))]
    public class TestTriggerControllerEditor : UnityEditor.Editor
    {
        private TestTriggerController _target;
        private void OnEnable()
        {
            _target = target as TestTriggerController;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Add Trigger"))
            {
                _target.AddTrigger();
            }
        }
    }
}