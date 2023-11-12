using System.Collections;
using Misc;
using Player;
using Unity.Mathematics;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int startingHealth = 3;
        [SerializeField] private float knockBackThrust = 15f;
        [SerializeField] private GameObject deathVFXPrefab;

        private int currentHealth;
        private Knockback knockback;
        private Flash flash;

        private void Awake()
        {
            knockback = GetComponent<Knockback>();
            flash = GetComponent<Flash>();
        }

        private void Start()
        {
            currentHealth = startingHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
            flash.GetFlashEffect();
            StartCoroutine(CheckDetectDeathRoutine());
        }

        private IEnumerator CheckDetectDeathRoutine()
        {
            yield return new WaitForSeconds(flash.GetRestoreDefaultMaterialTime());
        
            DetectDeath();
        }

        private void DetectDeath()
        {
            if (currentHealth > 0)
            {
                return;
            }
        
            Instantiate(deathVFXPrefab, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
    }
}
