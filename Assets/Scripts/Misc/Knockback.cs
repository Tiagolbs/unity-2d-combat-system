using System.Collections;
using UnityEngine;

namespace Misc
{
    public class Knockback : MonoBehaviour
    {
        [SerializeField] private float knockBackTime = 0.2f;
        public bool GettingKnockedBack { get; private set; }
    
        private Rigidbody2D myRigidbody;

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }

        public void GetKnockedBack(Transform damageSource, float knockBackThrust)
        {
            GettingKnockedBack = true;
        
            Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust *
                                 myRigidbody.mass;
            myRigidbody.AddForce(difference, ForceMode2D.Impulse);

            StartCoroutine(KnockRoutine());
        }

        private IEnumerator KnockRoutine()
        {
            yield return new WaitForSeconds(knockBackTime);
        
            myRigidbody.velocity = Vector2.zero;
            GettingKnockedBack = false;
        }
    }
}
