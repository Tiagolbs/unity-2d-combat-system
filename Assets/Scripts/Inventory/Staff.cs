using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    public void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
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
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
