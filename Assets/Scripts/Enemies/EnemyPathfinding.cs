using Misc;
using UnityEngine;

namespace Enemies
{
    public class EnemyPathfinding : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
    
        private Rigidbody2D myRigidbody;
        private Vector2 moveDirection;
        private Knockback knockBack;
        private SpriteRenderer mySpriteRenderer;

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            knockBack = GetComponent<Knockback>();
            mySpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (knockBack.GettingKnockedBack)
            {
                return;
            }
            myRigidbody.MovePosition(myRigidbody.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));

            mySpriteRenderer.flipX = moveDirection.x < 0;
        }

        public void MoveTo(Vector2 roamingPosition)
        {
            moveDirection = roamingPosition;
        }
    }
}
