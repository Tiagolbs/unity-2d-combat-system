using System;
using Player;
using UnityEngine;

namespace Scene_Management
{
    public class AreaEntrance : MonoBehaviour
    {
        [SerializeField] private string transitionName;

        private void Start()
        {
            if (transitionName != SceneManagement.Instance.SceneTransitionName)
            {
                return;
            }
            
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            UIFade.Instance.FadeToClear();
        }
    }
}
