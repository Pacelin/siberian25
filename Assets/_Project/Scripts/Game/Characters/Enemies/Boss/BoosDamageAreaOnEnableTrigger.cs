using System.Collections.Generic;
using UnityEngine;

namespace Siberian25.Game.Characters.Boss
{
    public class BoosDamageAreaOnEnableTrigger : MonoBehaviour
    {
        [SerializeField] private Collider2D _damageArea;
        [SerializeField] private LayerMask _playerLayerMask;
        
        private void OnEnable()
        {
            var results = new List<Collider2D>();
            _damageArea.Overlap(new ContactFilter2D()
            {
                useLayerMask = true,
                layerMask = _playerLayerMask
            }, results);
            foreach (var result in results)
            {
                if (result.TryGetComponent<HealthComponent>(out var health))
                {
                    health.TakeDamage(1);
                    break;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<PlayerComposition>(out _))
                col.GetComponent<HealthComponent>().TakeDamage(1);
        }
    }
}