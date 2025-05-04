using UnityEngine;

namespace Siberian25.Game.Characters.Boss
{
    [ExecuteAlways]
    public class AdjustHitboxFromLineRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private float _lineWidth;

        private void Update()
        {
            if (Application.isEditor)
                Adjust();
        }

        private void FixedUpdate()
        {
            Adjust();
        }

        private void Adjust()
        {
            Vector3 leftPoint = _lineRenderer.GetPosition(0);
            Vector3 rightPoint =_lineRenderer.GetPosition(1);

            Vector3 lineCenter = (leftPoint + rightPoint) / 2f;
            Vector3 boundsScale = new ((leftPoint - rightPoint).magnitude, _lineWidth, 1f);

            float angle = Vector3.SignedAngle((leftPoint - rightPoint).normalized, Vector3.right, Vector3.forward);

            _collider.transform.eulerAngles = new Vector3(0f, 0f, -angle);
            _collider.transform.position = lineCenter;
            _collider.transform.localScale = boundsScale;
        }
    }
}
