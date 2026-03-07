using System.Linq;
using Siberian25.Game.Characters;
using UnityEngine;

namespace Siberian25.Game.Bullets
{
    public class GetDamageOnBulletHit : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private int _damage = 1;

        [SerializeField] private EBulletType[] _checkTypes;

        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out BulletSpawner spawner) && _checkTypes.Any(bulletType => bulletType == spawner.BulletType))
                _health.TakeDamage(_damage);
        }
    }
}
