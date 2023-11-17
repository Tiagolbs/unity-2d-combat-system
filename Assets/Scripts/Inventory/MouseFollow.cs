using Player;
using UnityEngine;

namespace Inventory
{
    public class MouseFollow : MonoBehaviour
    {
        private void Update()
        {
            FaceMouse();
        }

        private void FaceMouse()
        {
            Vector3 mousePosition = PlayerController.Instance.PlayerControls.Movement.MousePosition.ReadValue<Vector2>();
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = transform.position - mousePosition;

            transform.right = -direction;
        }
    }
}
