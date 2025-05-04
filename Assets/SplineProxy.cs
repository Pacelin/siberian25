using UnityEngine;
using UnityEngine.Splines;


[ExecuteAlways]
public class SplineProxy : MonoBehaviour
{
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;
    [SerializeField] private Transform _point3;

    private void Update()
    {
        if (_splineContainer == null || _parent == null || _point1 == null || _point2 == null || _point3 == null)
            return;

        _splineContainer.Spline.SetKnot(0, GetKnot(_point1));
        _splineContainer.Spline.SetKnot(1, GetKnot(_point2));
        _splineContainer.Spline.SetKnot(2, GetKnot(_point3));
        _splineContainer.Spline.SetTangentMode(TangentMode.AutoSmooth);
    }

    private BezierKnot GetKnot(Transform tr)
    {
        BezierKnot knot = new()
        {
            Position = tr.position - _parent.position,
            Rotation = tr.rotation,
        };

        return knot;
    }
}
