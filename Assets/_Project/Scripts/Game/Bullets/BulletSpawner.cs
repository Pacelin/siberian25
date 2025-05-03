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
			BulletTypeData data = m_bulletsSettings.GetBulletTypeData(_bulletType);

			ParticleSystem.MainModule main = ps.main;
			main.startColor = data.startColor;

			ParticleSystem.TextureSheetAnimationModule animation = ps.textureSheetAnimation;
			animation.SetSprite(0, data.sprite);

			ParticleSystem.ShapeModule shape = ps.shape;
			shape.alignToDirection = data.alignToDirection;

			ParticleSystemRenderer renderer = ps.GetComponent<ParticleSystemRenderer>();
			renderer.rotateWithStretchDirection = data.alignToDirection;
		}
	}
}