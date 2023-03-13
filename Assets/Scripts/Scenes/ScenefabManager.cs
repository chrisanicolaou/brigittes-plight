using System;
using System.Collections.Generic;
using System.Linq;
using ChiciStudios.BrigittesPlight.Exceptions;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChiciStudios.BrigittesPlight.Scenes
{
    [CreateAssetMenu(fileName = "ScenefabManager", menuName = "ScriptableObjects/Scenes/ScenefabManager")]
    public class ScenefabManager : ScriptableObject
    {
        [FormerlySerializedAs("_scenefabs")]
        [SerializeField]
        [ReorderableList]
        private Scenefab[] _scenefabStore;

        public void LoadScene(ScenefabType scenefabType, bool loadAdditively = false)
        {
            var scenefab = _scenefabStore.FirstOrDefault(sf => sf.ScenefabType == scenefabType);
            
            if (scenefab == null)
            {
                Debug.LogException(new ScenefabException($"Cannot load scenefab '{scenefabType}'. Please make sure the scene exists in the {nameof(ScenefabManager)}."));
                return;
            }

            var existingScenefabs = FindObjectsOfType<Scenefab>(true);
            var existingScenefab = existingScenefabs
                .FirstOrDefault(sf => sf.ScenefabType == scenefabType);

            if (!scenefab.IsTransient && existingScenefab != null)
            {
                existingScenefab.Activate();
            }
            else if (scenefab.IsTransient && existingScenefab != null)
            {
                Debug.LogWarning($"You are trying to load an existing transient scenefab {scenefabType} found in scene. ScenefabManager will attempt to resolve this " +
                                 $"by destroying the existing transient scenefab. This can cause unwanted behaviour - please consider if this needs to be a scenefab.");
                Destroy(existingScenefab.gameObject);
                Instantiate(scenefab.gameObject);
            }
            else
            {
                Instantiate(scenefab.gameObject);
            }

            if (loadAdditively) return;

            for (var i = existingScenefabs.Length - 1; i >= 0; i--)
            {
                if (existingScenefabs[i] == existingScenefab) continue;
                existingScenefabs[i].Deactivate();
            }
        }
    }
}