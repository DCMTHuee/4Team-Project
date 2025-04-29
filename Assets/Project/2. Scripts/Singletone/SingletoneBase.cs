using System;
using UnityEngine;

namespace MoonYoHanStudy
{
    public class DontDestroyOnLoadOn : MonoBehaviour { }
    public class DontDestroyOnLoadOff : MonoBehaviour { }


    public class SingletoneBase<T, TDontDestroyOnLoad> : MonoBehaviour
        where T : MonoBehaviour
        where TDontDestroyOnLoad : MonoBehaviour
    {
        public static T Singletone
        {
            get
            {
                return _Instance.Value;
            }
        }

        private static readonly Lazy<T> _Instance = new Lazy<T>(() =>
        {
            T instance = FindAnyObjectByType<T>();

            if (instance == null)
            {
                GameObject obj = new GameObject(typeof(T).ToString());
                instance = obj.AddComponent(typeof(T)) as T;

#if UNITY_EDITOR

                if (typeof(TDontDestroyOnLoad) == typeof(DontDestroyOnLoadOn))
                {
                    if (UnityEditor.EditorApplication.isPlaying)
                    {
                        DontDestroyOnLoad(obj);
                    }
#else
                DontDestroyOnLoad(obj);
#endif
                }
            }

            return instance;
        });

        protected virtual void Awake()
        {
            // TPersistence°¡ Persistent¸é DontDestroyOnLoad
            if (typeof(TDontDestroyOnLoad) == typeof(DontDestroyOnLoadOn))
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}
