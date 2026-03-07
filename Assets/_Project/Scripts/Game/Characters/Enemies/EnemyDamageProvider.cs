using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyDamageProvider : MonoBehaviour
    {
        private bool _isActive;

        public void SetActive(bool isActive) =>
            _isActive = isActive;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!_isActive)
                return;
            
            if (col.TryGetComponent<PlayerComposition>(out _))
                col.GetComponent<HealthComponent>().TakeDamage(1);
        }
    }
}