using System;
using UnityEditor;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Tests.Cards.Editor
{
    [CustomEditor(typeof(TestCardController))]
    public class TestCardControllerEditor : UnityEditor.Editor
    {
        private TestCardController _target;

        private void OnEnable()
        {
            _target = target as TestCardController;
        }

        public override async void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Simulate Play"))
            {
                await _target.TestPlay();
            }
        }
    }
}