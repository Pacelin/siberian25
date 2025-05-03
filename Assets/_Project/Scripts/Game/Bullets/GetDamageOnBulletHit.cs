using Siberian25.Game.Characters;
using UnityEngine;

namespace Siberian25.Game.Bullets
{
    public class GetDamageOnBulletHit : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private int _damage = 1;

        private void OnParticleCollision(GameObject other)
        {
            _health.TakeDamage(_damage);
        }
    }
}
