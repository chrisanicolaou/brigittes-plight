using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChiciStudios.BrigittesPlight.Scenes
{
    public class Scenefab : MonoBehaviour
    {
        [SerializeField]
        private ScenefabType _scenefabType;
        
        [SerializeField]
        [Tooltip("Default behaviour for switching scenes is for active scenefabs to deactivate. Transient scenefabs will destroy instead." +
                 "Useful for when you want to load a scenefab from a clean state.")]
        private bool _transient;
        
        [EnableIf("_transient")] 
        [SerializeField] 
        private int _delayUntilDestroy = 2;
        
        public ScenefabType ScenefabType => _scenefabType;
        public bool IsTransient => _transient;

        public void Deactivate()
        {
            gameObject.SetActive(false);

            if (!_transient) return;
            
            Destroy(gameObject, _delayUntilDestroy);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }
    }
}