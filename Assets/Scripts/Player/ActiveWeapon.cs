using System;
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
        }

        private void Update()
        {
            Attack();
        }

        public void NewWeapon(MonoBehaviour newWeapon)
        {
            CurrentActiveWeapon = newWeapon;
        }

        public void WeaponNull()
        {
            CurrentActiveWeapon = null;
        }

        public void ToggleIsAttacking(bool value)
        {
            isAttacking = value;
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
            
            isAttacking = true;
            (CurrentActiveWeapon as IWeapon).Attack();

        }
    }
}
