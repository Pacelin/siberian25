using TSS.Core;
using UnityEngine;

namespace Siberian25.Game.World
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private float _xMultiplier = 0.2f;
        [SerializeField] private float _yMultiplier = 0.2f;

        private Vector2 _lastCameraPos;
        private void Awake() => _lastCameraPos = SceneCameraProvider.MainCamera.transform.position;

        private void Update()
        {
            var curPos = (Vector2) SceneCameraProvider.MainCamera.transform.position;
            var delta = curPos - _lastCameraPos;
            var myDelta = new Vector2(delta.x * _xMultiplier, delta.y * _yMultiplier);

            transform.position += (Vector3) myDelta;
            _lastCameraPos = curPos;
        }
    }
}