using UnityEngine;

namespace Siberian25.Game.Bullets
{
	[DisallowMultipleComponent, RequireComponent(typeof(ParticleSystem))]
	public class BulletSpawner : MonoBehaviour
	{
		[SerializeField] private BulletSettings m_bulletsSettings;
		[SerializeField] private EBulletType _bulletType = EBulletType.Destroyable;

		private ParticleSystem _deflectedPs;

		#if UNITY_EDITOR

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

			ParticleSystem.CollisionModule collision = ps.collision;
			collision.lifetimeLoss = _bulletType == EBulletType.Deflectable ? 0f : 1f;

			UnityEditor.EditorApplication.delayCall += ProcessDeflectableBullets;
		}

		private void ProcessDeflectableBullets()
		{
			UnityEditor.EditorApplication.delayCall -= ProcessDeflectableBullets;

			if (_bulletType == EBulletType.Deflectable)
				AddDeflectedPs(GetComponent<ParticleSystem>());
			else
				TryDestroyDeflectedPs();
		}

		private void AddDeflectedPs(ParticleSystem ps)
		{
			if (_deflectedPs != null) return;

			GameObject childGo = new();
			childGo.transform.parent = transform;
			_deflectedPs = childGo.AddComponent<ParticleSystem>();

			UnityEditor.EditorUtility.CopySerialized(ps, _deflectedPs);

			ParticleSystem.CollisionModule collision = _deflectedPs.collision;
			collision.collidesWith = m_bulletsSettings.DeflectedLayers;
			collision.lifetimeLoss = 1f;

			ParticleSystem.ShapeModule shape = _deflectedPs.shape;
			shape.enabled = false;

			ParticleSystem.EmissionModule emission = _deflectedPs.emission;
			emission.enabled = false;
		}

		private void TryDestroyDeflectedPs()
		{
			if (_deflectedPs)
				DestroyImmediate(_deflectedPs.gameObject);
		}
		#endif
	}
}