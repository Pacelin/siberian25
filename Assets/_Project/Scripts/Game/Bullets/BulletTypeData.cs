using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Siberian25.Game.Bullets
{
	[Serializable]
	public class BulletTypeData
	{
		public Color startColor;
        public Sprite sprite;
        [FormerlySerializedAs("alignWithDirection")]
        public bool alignToDirection = true;
	}
}