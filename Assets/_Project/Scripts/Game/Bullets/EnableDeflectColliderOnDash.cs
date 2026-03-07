using Siberian25.Game.Characters;
using UnityEngine;

namespace _Project.Scripts.Game.Bullets
{
	public class EnableDeflectColliderOnDash : MonoBehaviour
	{
		[SerializeField] private Collider2D _collider;
		[SerializeField] private PlayerSliceTrigger _player;

		private void Update()
		{
			_collider.gameObject.SetActive(_player.EnableSlice);
		}
	}
}