using System.Diagnostics;
using UnityEngine;

namespace Siberian25.Game.Bullets
{
	[DisallowMultipleComponent, RequireComponent(typeof(ParticleSystem))]
	public class BulletSpawner : MonoBehaviour
	{
		[SerializeField] private BulletSettings m_bulletsSettings;
		[SerializeField] private EBulletType _bulletType = EBulletType.Destroyable;

		[Conditional("UNITY_EDITOR")]
		private void OnValidate()
		{
			if (m_bulletsSettings == null)
				return;

			ParticleSystem ps = GetComponent<ParticleSystem>();
			ParticleSystem.MainModule main = ps.main;

			BulletTypeData data = m_bulletsSettings.GetBulletTypeData(_bulletType);
			main.startColor = data.startColor;
		}
	}
}