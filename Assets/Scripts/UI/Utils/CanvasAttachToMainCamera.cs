using System;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.UI.Utils
{
    public class CanvasAttachToMainCamera : MonoBehaviour
    {
        private Canvas _canvas;
        private void Start()
        {
            _canvas = GetComponent<Canvas>();

            if (_canvas is null)
            {
                Debug.LogError($"Canvas not found on {gameObject}! A {nameof(CanvasAttachToMainCamera)} requires a Canvas component.");
                Destroy(this);
                return;
            }
            
            if (_canvas.renderMode is not (RenderMode.ScreenSpaceCamera or RenderMode.ScreenSpaceOverlay))
            {
                Debug.LogWarning($"Canvas for object {gameObject} is not set to ScreenSpaceCamera or Overlay (render mode: {_canvas.renderMode}). Aborting attach...");
                Destroy(this);
                return;
            }
            
            _canvas.worldCamera = Camera.main;
        }
    }
}