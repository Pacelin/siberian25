using UnityEngine;

namespace Siberian25.Game.Bullets
{
	[DisallowMultipleComponent, RequireComponent(typeof(ParticleSystem))]
	public class BulletSpawner : MonoBehaviour
	{
		[SerializeField] private BulletSettings m_bulletsSettings;
		[SerializeField] private EBulletType _bulletType = EBulletType.Destroyable;

		#if UNITY_EDITOR
		private void OnValidate()
		{
			if (m_bulletsSettings == null)
				return;

			ParticleSystem ps = GetComponent<ParticleSystem>();
			BulletTypeData data = m_bulletsSettings.GetBulletTypeData(_bulletType);

			ApplyData(data, ps);

			ParticleSystem.CollisionModule collision = ps.collision;
			collision.lifetimeLoss = _bulletType == EBulletType.Deflectable ? 0f : 1f;

			UnityEditor.EditorApplication.delayCall += ProcessDeflectableBullets;
		}

		private void ApplyData(BulletTypeData data, ParticleSystem ps)
		{
			ParticleSystem.MainModule main = ps.main;
			main.startColor = data.startColor;

			ParticleSystem.TextureSheetAnimationModule animation = ps.textureSheetAnimation;
			animation.SetSprite(0, data.sprite);

			ParticleSystem.ShapeModule shape = ps.shape;
			shape.alignToDirection = data.alignToDirection;

			ParticleSystemRenderer renderer = ps.GetComponent<ParticleSystemRenderer>();
			renderer.rotateWithStretchDirection = data.alignToDirection;

			ParticleSystem.CollisionModule collision = ps.collision;
			collision.collidesWith = data.collisionMask;
		}

		private void ProcessDeflectableBullets()
		{
			UnityEditor.EditorApplication.delayCall -= ProcessDeflectableBullets;

			if (this == null)
				return;

			if (_bulletType == EBulletType.Deflectable)
				AddDeflectedPs(GetComponent<ParticleSystem>());
			else
				TryDestroyDeflectedPs();
		}

		private void AddDeflectedPs(ParticleSystem ps)
		{
			ParticleSystem deflectedPs = transform.childCount > 0 && transform.GetChild(0).TryGetComponent(out ParticleSystem dps) ? dps : null;

			if (deflectedPs != null || GetComponent<DeflectedBulletEngine>() != null) return;

			BulletTypeData data = m_bulletsSettings.DeflectedBulletsData;

			GameObject childGo = new();
			childGo.transform.parent = transform;
			deflectedPs = childGo.AddComponent<ParticleSystem>();

			ApplyData(data, deflectedPs);

			UnityEditor.EditorUtility.CopySerialized(ps, deflectedPs);

			ParticleSystem.CollisionModule collision = deflectedPs.collision;
			collision.lifetimeLoss = 1f;

			ParticleSystem.ShapeModule shape = deflectedPs.shape;
			shape.enabled = false;

			ParticleSystem.EmissionModule emission = deflectedPs.emission;
			emission.enabled = false;

			DeflectedBulletEngine engine = gameObject.AddComponent<DeflectedBulletEngine>();
			engine.Initiate(ps, deflectedPs);
		}

		private void TryDestroyDeflectedPs()
		{
			ParticleSystem deflectedPs = transform.childCount > 0 && transform.GetChild(0).TryGetComponent(out ParticleSystem dps) ? dps : null;

			if (deflectedPs == null || !TryGetComponent(out DeflectedBulletEngine engine))
				return;

			DestroyImmediate(deflectedPs.gameObject);
			DestroyImmediate(engine);
		}
		#endif
	}
}