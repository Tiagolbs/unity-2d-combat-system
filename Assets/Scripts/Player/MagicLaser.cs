using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class MagicLaser : MonoBehaviour
    {
        [SerializeField] private float laserGrowTime = 2f;
        
        private float laserRange;
        private SpriteRenderer spriteRenderer;
        private CapsuleCollider2D myCapsuleCollider2D;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }

        private void Start()
        {
            LaserFaceMouse();
        }

        public void UpdateLaserRange(float laserRange)
        {
            this.laserRange = laserRange;
            StartCoroutine(IncreaseLaserLengthRoutine());
        }

        private IEnumerator IncreaseLaserLengthRoutine()
        {
            float timePassed = 0f;

            while (spriteRenderer.size.x < laserRange)
            {
                timePassed += Time.deltaTime;
                float linerTime = timePassed / laserGrowTime;
                spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linerTime), 1f);
                
                myCapsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, laserRange, linerTime), myCapsuleCollider2D.size.y);
                myCapsuleCollider2D.offset = new Vector2((Mathf.Lerp(1f, laserRange, linerTime)) / 2, myCapsuleCollider2D.offset.y);
                
                yield return null;
            }

            StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
        }

        private void LaserFaceMouse()
        {
            Vector3 mousePosition = PlayerController.Instance.PlayerControls.Movement.MousePosition.ReadValue<Vector2>();
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = transform.position - mousePosition;

            transform.right = -direction;
        }
    }
}
