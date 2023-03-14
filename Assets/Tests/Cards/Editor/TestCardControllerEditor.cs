using System;
using UnityEditor;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Tests.Cards.Editor
{
    [CustomEditor(typeof(TestCardCastController))]
    public class TestCardControllerEditor : UnityEditor.Editor
    {
        private TestCardCastController _target;

        private void OnEnable()
        {
            _target = target as TestCardCastController;
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