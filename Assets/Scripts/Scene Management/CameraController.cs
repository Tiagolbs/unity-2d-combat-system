using Cinemachine;
using Player;

namespace Scene_Management
{
    public class CameraController : Singleton<CameraController>
    {
        private CinemachineVirtualCamera cinemachineVirtualCamera;
    
        public void SetPlayerCameraFollow()
        {
            cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
        }
    }
}
