using UnityEngine;

namespace Player
{
    public class SelfDestroy : MonoBehaviour
    {
        private ParticleSystem ps;

        private void Awake()
        {
            ps = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (ps && !ps.IsAlive())
            {
                DestroySelfAnimationEvent();
            }
        }

        public void DestroySelfAnimationEvent()
        {
            Destroy(gameObject);
        }
    }
}
