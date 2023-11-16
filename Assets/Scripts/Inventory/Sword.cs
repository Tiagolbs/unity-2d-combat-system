using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class Sword : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject slashAnimationPrefab;
        [SerializeField] private float attackCooldown = 0.5f;
    
        private Animator myAnimator;
        private GameObject slashAnimation;
        private Transform weaponCollider;
        private Transform slashAnimationSpawnPoint;
    
        private static readonly int AttackAnimHash = Animator.StringToHash("Attack");

        private void Awake()
        {
            myAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            weaponCollider = PlayerController.Instance.GetWeaponCollider();
            slashAnimationSpawnPoint = PlayerController.Instance.GetSlashAnimationSpawnPoint();
        }

        private void Update()
        {
            MouseFollowWithOffset();
        }

        public void DoneAttackingAnimationEvent()
        {
            weaponCollider.gameObject.SetActive(false);
        }

        public void SwingUpFlipAnimationEvent()
        {
            slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

            if (PlayerController.Instance.FacingLeft)
            {
                slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        public void SwingDownFlipAnimationEvent()
        {
            slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        
            if (PlayerController.Instance.FacingLeft)
            {
                slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
            }    
        }
    
        public void Attack()
        {
            myAnimator.SetTrigger(AttackAnimHash);
            weaponCollider.gameObject.SetActive(true);
            slashAnimation = Instantiate(slashAnimationPrefab, slashAnimationSpawnPoint.position, Quaternion.identity);
            slashAnimation.transform.parent = this.transform.parent;
            StartCoroutine(AttackCooldownRoutine());
        }

        private IEnumerator AttackCooldownRoutine()
        {
            yield return new WaitForSeconds(attackCooldown);
            ActiveWeapon.Instance.ToggleIsAttacking(false);
        }

        private void MouseFollowWithOffset()
        {
            Vector2 mousePosition = PlayerController.Instance.PlayerControls.Movement.MousePosition.ReadValue<Vector2>();
            Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

            float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

            if (mousePosition.x < playerScreenPoint.x)
            {
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
                weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
                weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
