using System.Collections;
using Misc;
using Scene_Management;
using UnityEngine;

namespace Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float dashSpeed = 4f;
        [SerializeField] private float dashTime = 0.2f;
        [SerializeField] private float dashCooldown = 2f;
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private Transform weaponCollider;
        [SerializeField] private Transform slashAnimationSpawnPoint;
        
        private Vector2 movement;
        private Rigidbody2D myRigidbody;
        private Animator myAnimator;
        private SpriteRenderer mySpriteRenderer;
        private bool isDashing = false;
        private float startingMoveSpeed;
        private Knockback knockback;

        private static readonly int MoveX = Animator.StringToHash("moveX");
        private static readonly int MoveY = Animator.StringToHash("moveY");
    
        public bool FacingLeft { get; private set; } = false;
        public PlayerControls PlayerControls { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            PlayerControls = new PlayerControls();
            myRigidbody = GetComponent<Rigidbody2D>();
            myAnimator = GetComponent<Animator>();
            mySpriteRenderer = GetComponent<SpriteRenderer>();
            knockback = GetComponent<Knockback>();
        }

        private void Start()
        {
            PlayerControls.Combat.Dash.performed += _ => Dash();
            startingMoveSpeed = moveSpeed;
        }

        private void OnEnable()
        {
            PlayerControls.Enable();
        }

        private void Update()
        {
            PlayerInput();
        }

        private void FixedUpdate()
        {
            AdjustPlayerFacingDirection();
            Move();
        }

        public Transform GetWeaponCollider()
        {
            return weaponCollider;
        }
        
        public Transform GetSlashAnimationSpawnPoint()
        {
            return slashAnimationSpawnPoint;
        }

        private void PlayerInput()
        {
            movement = PlayerControls.Movement.Move.ReadValue<Vector2>();
        
            myAnimator.SetFloat(MoveX, movement.x);
            myAnimator.SetFloat(MoveY, movement.y);
        }

        private void Move()
        {
            if (knockback.GettingKnockedBack)
            {
                return;
            }
            
            myRigidbody.MovePosition(myRigidbody.position + movement * (moveSpeed * Time.fixedDeltaTime));
        }

        private void AdjustPlayerFacingDirection()
        {
            Vector2 mousePosition = PlayerControls.Movement.MousePosition.ReadValue<Vector2>();
            Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

            if (mousePosition.x < playerScreenPoint.x)
            {
                FacingLeft = true;
                mySpriteRenderer.flipX = true;
            }
            else
            {
                FacingLeft = false;
                mySpriteRenderer.flipX = false;
            }
        }

        private void Dash()
        {
            if (isDashing)
            {
                return;
            }

            isDashing = true;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }

        private IEnumerator EndDashRoutine()
        {
            yield return new WaitForSeconds(dashTime);
            moveSpeed = startingMoveSpeed;
            trailRenderer.emitting = false;
            yield return new WaitForSeconds(dashCooldown);
            isDashing = false;
        }
    }
}
