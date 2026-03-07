using UnityEngine;

[ExecuteAlways]
public class LineRendererProxy : MonoBehaviour
{
	[SerializeField] private LineRenderer _lineRenderer;
	[SerializeField] private Transform _point1;
	[SerializeField] private Transform _point2;

	private void Update()
	{
		if (_lineRenderer == null || _point1 == null || _point2 == null)
			return;

		_lineRenderer.SetPosition(0, _point1.position);
		_lineRenderer.SetPosition(1, _point2.position);
	}
}