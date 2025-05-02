using UnityEngine;

namespace Siberian25.Game.Characters
{
    public static class Vector2Extensions
    {
        public static Vector2 Spread(this Vector2 vector, float spread)
        {
            if (spread <= 0)
                return vector.normalized;
        
            float randomAngle = Random.Range(-spread * 0.5f, spread * 0.5f);
            return Quaternion.Euler(0, 0, randomAngle) * vector.normalized;
        }
    }
}