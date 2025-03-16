using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using System.Linq;

namespace MoonYoHanStudy
{

    public class SingletoneBase<T> : MonoBehaviour where T : class
    {
        [Obsolete]
        public static T Singletone
        {
            get
            {
                return _Instance.Value;
            }
        }

        [Obsolete]
        private static readonly Lazy<T> _Instance = new Lazy<T>(() =>
        {
            T instance = FindObjectOfType(typeof(T)) as T;

            if (instance == null)
            {
                GameObject obj = new GameObject(typeof(T).ToString());
                instance = obj.AddComponent(typeof(T)) as T;

#if UNITY_EDITOR
                if(UnityEditor.EditorApplication.isPlaying)
                {
                    DontDestroyOnLoad(obj);
                }
#else
                DontDestroyOnLoad(obj);
#endif
            }

            return instance;
        });

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
