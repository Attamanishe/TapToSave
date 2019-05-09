using UnityEngine;

namespace Help
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance;

        public virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = (T) this;
        }
    }
}