using System;
using ChiciStudios.BrigittesPlight.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace ChiciStudios.BrigittesPlight.Tests.ScenefabTests
{
    public class ScenefabTestSceneController : MonoBehaviour
    {
        [SerializeField] private Toggle _additiveToggle;
        [SerializeField] private Button _loadActScene;
        [SerializeField] private Button _loadRoomScene;
        [SerializeField] private Button _loadBattleScene;
        
        [SerializeField] private ScenefabManager _scenefabManager;

        private void Start()
        {
            _loadActScene.onClick.AddListener(delegate { _scenefabManager.LoadScene(ScenefabType.Act, _additiveToggle.isOn); });
            _loadRoomScene.onClick.AddListener(delegate { _scenefabManager.LoadScene(ScenefabType.Room, _additiveToggle.isOn); });
            _loadBattleScene.onClick.AddListener(delegate { _scenefabManager.LoadScene(ScenefabType.Battle, _additiveToggle.isOn); });
        }
    }
}