using System;
using Enemies;
using Inventory;
using UnityEngine;

namespace Player
{
    public class DamageSource : MonoBehaviour
    {
        private int damageAmount;

        private void Start()
        {
            MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
            damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
        }

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
