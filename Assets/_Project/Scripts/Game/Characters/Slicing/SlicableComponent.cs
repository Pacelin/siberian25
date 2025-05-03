using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnitySpriteCutter;
using GameObject = UnityEngine.GameObject;
using Vector2 = UnityEngine.Vector2;

namespace Siberian25.Game.Characters
{
    public class SlicableComponent : MonoBehaviour, ISlicable
    {
        [SerializeField] private Behaviour[] _disableOnCut;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private LineRenderer _lineRendererPrefab;
        [Header("Settings")]
        [SerializeField] private int _maxSlices = 3;
        [SerializeField] private float _sliceLineLength = 2;
        [SerializeField] private Vector2 _explosionForceRange;
        [SerializeField] private Vector2 _explosionInheritDirectionForceRange;
        [SerializeField] private Vector2 _debrisLifetimeRange;
        [SerializeField] private float _linearDampingForPieces = 0.1f;
        [SerializeField] private float _spread;
        
        private readonly List<(Vector2, Vector2)> _slices = new();
        
        public void OnSlicePerform(Vector2 point, Vector2 direction)
        {
            if (!_healthComponent)
                return;
            if (_healthComponent.IsDead)
                Slice(direction);
        }
        
        public void OnSlice(Vector2 point, Vector2 direction)
        {
            if (!_healthComponent)
                return;
            if (_healthComponent.IsDead)
                return;
            _healthComponent.TakeDamage(1);
            _slices.Add((point, direction));
            var newLineRenderer = Instantiate(_lineRendererPrefab, transform);
            newLineRenderer.positionCount = 2;
            var point1 = point - direction * 10;
            var point2 = point + direction * 10;
            _collider.bounds.IntersectRay(new Ray(point1, direction * 20), out var distance1);
            _collider.bounds.IntersectRay(new Ray(point2, -direction * 20), out var distance2);
            point1 += direction * distance1;
            point2 -= direction * distance2;
            newLineRenderer.SetPosition(0, transform.InverseTransformPoint(point1));
            newLineRenderer.SetPosition(1, transform.InverseTransformPoint(point2));
        }

        private void Slice(Vector2 explodeDirection)
        {
            foreach (var behaviour in _disableOnCut)
                behaviour.enabled = false;
            
            List<GameObject> result = new List<GameObject>();
            
            for (int i = 0; i < _slices.Count && i < _maxSlices; i++)
                Slice(result, _slices[^(i + 1)].Item1, _slices[^(i + 1)].Item2);
            if (result.Count == 0)
                result.Add(gameObject);

            List<Vector2> piecesCenters = new List<Vector2>();
            List<Rigidbody2D> rbs = new List<Rigidbody2D>();
            List<DebrisComponent> debrises = new List<DebrisComponent>();
            foreach (var go in result)
            {
                var rb = go.GetComponent<Rigidbody2D>();
                if (!rb)
                    rb = go.AddComponent<Rigidbody2D>();
                rbs.Add(rb);
                var col = go.GetComponent<PolygonCollider2D>();
                var debris = go.AddComponent<DebrisComponent>();
                debrises.Add(debris);
                var center = new Vector2(col.points.Average(p => p.x), col.points.Average(p => p.y));
                piecesCenters.Add(center);
                rb.linearDamping = _linearDampingForPieces;
                rb.constraints = RigidbodyConstraints2D.None;
                rb.useAutoMass = true;
                rb.bodyType = RigidbodyType2D.Dynamic;
                col.enabled = false;
            }
            var explosionCenter = new Vector2(piecesCenters.Average(p => p.x), piecesCenters.Average(p => p.y));
            for (int i = 0; i < result.Count; i++)
            {
                var direction = (piecesCenters[i] - explosionCenter).normalized.Spread(_spread);
                rbs[i].AddForce(direction * Random.Range(_explosionForceRange.x, _explosionForceRange.y));
                rbs[i].AddForce(explodeDirection * Random.Range(_explosionInheritDirectionForceRange.x, _explosionInheritDirectionForceRange.y));
                debrises[i].Activate(Random.Range(_debrisLifetimeRange.x, _debrisLifetimeRange.y));
            }
            
            Destroy(gameObject);
        }

        private void Slice(List<GameObject> gameObjects, Vector2 point, Vector2 direction)
        {
            if (gameObjects.Count > 0)
            {
                var cutCount = gameObjects.Count;
                for (int i = 0; i < cutCount; i++)
                {
                    var input = new SpriteCutterInput()
                    {
                        dontCutColliders = false,
                        gameObject = gameObjects[i],
                        gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_OFF_NEW,
                        lineStart = point,
                        lineEnd = point + direction
                    };
                    try
                    {
                        var output = SpriteCutter.Cut(input);
                        if (output != null)
                        {
                            if (output.secondSideGameObject)
                                gameObjects.Add(output.secondSideGameObject);
                        }
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }
            else
            {
                var input = new SpriteCutterInput()
                {
                    dontCutColliders = false,
                    gameObject = gameObject,
                    gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_INTO_TWO,
                    lineStart = point,
                    lineEnd = point + direction
                };
                try
                {
                    var output = SpriteCutter.Cut(input);
                    if (output != null)
                    {
                        if (output.firstSideGameObject)
                            gameObjects.Add(output.firstSideGameObject);
                        if (output.secondSideGameObject)
                            gameObjects.Add(output.secondSideGameObject);
                    }
                }
                catch
                {
                    // ignore
                }
            }
        }
    }
}