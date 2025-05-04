using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace Siberian25.Game.Characters.Boss
{
	[ExecuteAlways]
	public class AttachHeadToSpline : MonoBehaviour
	{
		[SerializeField]
		private SplineContainer _splineContainer;
		[SerializeField]
		private GameObject _head;

		private void Update()
		{
			float3 position = _splineContainer.EvaluatePosition(1f);
			_head.transform.position = (Vector3)position;
		}
	}
}
