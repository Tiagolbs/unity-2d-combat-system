using Player;
using UnityEngine;

namespace Misc
{
    public class Destructible : MonoBehaviour
    {
        [SerializeField] private GameObject destroyVFX;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.GetComponent<DamageSource>())
            {
                return;
            }
        
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}