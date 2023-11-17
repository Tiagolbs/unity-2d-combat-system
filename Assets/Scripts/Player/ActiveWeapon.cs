using System;
using System.Collections;
using Inventory;
using Scene_Management;
using UnityEngine;

namespace Player
{
    public class ActiveWeapon : Singleton<ActiveWeapon>
    {
        public MonoBehaviour CurrentActiveWeapon { get; private set; }
        
        private PlayerControls playerControls;
        private bool attackButtonDown = false;
        private bool isAttacking = false;
        private float timeBetweenAttacks;

        protected override void Awake()
        {
            base.Awake();
            playerControls = new PlayerControls();
        }
        
        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void Start()
        {
            playerControls.Combat.Attack.started += _ => StartAttacking();
            playerControls.Combat.Attack.canceled += _ => StopAttacking();
            
            AttackCooldown();
        }

        private void Update()
        {
            Attack();
        }

        public void NewWeapon(MonoBehaviour newWeapon)
        {
            CurrentActiveWeapon = newWeapon;
            AttackCooldown();
            timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
        }

        public void WeaponNull()
        {
            CurrentActiveWeapon = null;
        }

        private void AttackCooldown()
        {
            isAttacking = true;
            StopAllCoroutines();
            StartCoroutine(TimeBetweenAttackRoutine());
        }

        private IEnumerator TimeBetweenAttackRoutine()
        {
            yield return new WaitForSeconds(timeBetweenAttacks);
            isAttacking = false;
        }

        private void StartAttacking()
        {
            attackButtonDown = true;
        }
    
        private void StopAttacking()
        {
            attackButtonDown = false;
        }

        private void Attack()
        {
            if (!attackButtonDown || isAttacking)
            {
                return;
            }
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack();

        }
    }
}
