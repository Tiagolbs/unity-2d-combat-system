using Enemies;
using UnityEngine;

namespace Player
{
    public class DamageSource : MonoBehaviour
    {
        [SerializeField] private int damageAmount = 1;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.GetComponent<EnemyHealth>())
            {
                return;
            }
        
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageAmount);
        }
    }
}
