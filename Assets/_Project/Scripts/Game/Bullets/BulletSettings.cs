using System;
using UnityEngine;

namespace Siberian25.Game.Bullets
{
	[CreateAssetMenu(menuName = "Game/Bullet Settings", fileName = "SO_" + nameof(BulletSettings), order = 0)]
	public class BulletSettings : ScriptableObject
	{
		[SerializeField] private BulletTypeData m_destroyableBulletsType;
		[SerializeField] private BulletTypeData m_immortalBulletsType;
		[SerializeField] private BulletTypeData m_deflectableBulletsType;
		[SerializeField] private BulletTypeData m_deflectedBullets;

		public BulletTypeData DeflectedBulletsData => m_deflectedBullets;

		public BulletTypeData GetBulletTypeData(EBulletType bulletType)
		{
			return bulletType switch
			{
				EBulletType.Destroyable => m_destroyableBulletsType,
				EBulletType.Immortal => m_immortalBulletsType,
				EBulletType.Deflectable => m_deflectableBulletsType,
				_ => throw new ArgumentOutOfRangeException(nameof(bulletType), bulletType, null)
			};
		}
	}
}