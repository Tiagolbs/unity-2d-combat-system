using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene_Management
{
    public class AreaExit : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;
        [SerializeField] private string sceneTransitionName;

        private float waitToLoadTime = 1f;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.GetComponent<PlayerController>())
            {
                return;
            }
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }

        private IEnumerator LoadSceneRoutine()
        {
            yield return new WaitForSeconds(waitToLoadTime);
            
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
