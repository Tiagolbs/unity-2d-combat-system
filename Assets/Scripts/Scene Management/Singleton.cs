using UnityEngine;

namespace Scene_Management
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && this.gameObject != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = (T)this;
            }

            if (!gameObject.transform.parent)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
