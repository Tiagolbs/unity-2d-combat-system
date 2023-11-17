using Player;
using UnityEngine;

namespace Inventory
{
    public class Staff : MonoBehaviour, IWeapon
    {
        [SerializeField] private WeaponInfo weaponInfo;
        [SerializeField] private GameObject magicLaserPrefab;
        [SerializeField] private Transform magicLaserSpawnFound;

        private Animator myAnimator;
        private static readonly int Fire = Animator.StringToHash("Fire");

        private void Awake()
        {
            myAnimator = GetComponent<Animator>();
        }

        public void Update()
        {
            MouseFollowWithOffset();
        }

        public void Attack()
        {
            myAnimator.SetTrigger(Fire);
        }

        public void SpawnStaffProjectileAnimationEvent()
        {
            GameObject newMagicLaser =
                Instantiate(magicLaserPrefab, magicLaserSpawnFound.position, ActiveWeapon.Instance.transform.rotation);
            
            newMagicLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
        }
    
        public WeaponInfo GetWeaponInfo()
        {
            return weaponInfo;
        }
    
        private void MouseFollowWithOffset()
        {
            Vector2 mousePosition = PlayerController.Instance.PlayerControls.Movement.MousePosition.ReadValue<Vector2>();
            Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

            float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

            if (mousePosition.x < playerScreenPoint.x)
            {
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            }
            else
            {
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}
