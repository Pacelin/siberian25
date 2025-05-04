using UnityEngine;

namespace Siberian25.Game.Bullets
{
	public class DeflectedBulletProxy : MonoBehaviour
	{
		public DeflectedBulletEngine Engine;

		private void OnParticleCollision(GameObject other)
		{
			Engine.OnParticleCollision(other);
		}
	}
}