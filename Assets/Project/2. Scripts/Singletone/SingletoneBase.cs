using System;
using Unity.VisualScripting;
using UnityEngine;

namespace MoonYoHanStudy
{
    // 싱글톤인데 다음씬에 자기를 유지할 지 판단 여부를 가르는 클래스
    public class DontDestroyOnLoadOn { }
    public class DontDestroyOnLoadOff { }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T_ClassType">싱글톤 시키려는 클래스타입</typeparam>
    /// <typeparam name="T_DontDestroyOnLoad">DontDestroyOnLoad On/Off 타입</typeparam>
    public class SingletoneBase<T_ClassType, T_DontDestroyOnLoad> : MonoBehaviour where T_ClassType : MonoBehaviour
    {
        public static T_ClassType Singletone
        {
            get
            {
                return _Instance.Value;
            }
        }

        private static readonly Lazy<T_ClassType> _Instance = new Lazy<T_ClassType>(() =>
        {
            T_ClassType instance = FindAnyObjectByType<T_ClassType>(); // 클래스를 검색해서 instance안에 집어넣음

            if (instance == null) // instance가 null이라면 T_ClassType를 가진 GameObject를 생성해서 AddComponent로 집어넣음
            {
                GameObject obj = new GameObject(typeof(T_ClassType).ToString());
                instance = obj.AddComponent(typeof(T_ClassType)) as T_ClassType;

                DontDestroyOnLoad_Execution(obj);
            }

            return instance;
        });

        protected virtual void Awake()
        {
            DontDestroyOnLoad_Execution(this.gameObject);
        }

        // 조건부를 확인하고, 받아온 GameObject를 파괴시키지 않는다.
        private static void DontDestroyOnLoad_Execution(GameObject obj)
        {
                // #if UNITY_EDITOR

            if (typeof(T_DontDestroyOnLoad) == typeof(DontDestroyOnLoadOn)) // T_DontDestroyOnLoad의 타입이 On이면 DontDestroyOnLoad() 함수가 실행됨.
            {
                DontDestroyOnLoad(obj);

                /*                    if (UnityEditor.EditorApplication.isPlaying)
                                    {
                                        DontDestroyOnLoad(obj);
                                    }

                #else
                                DontDestroyOnLoad(obj);
                #endif*/
            }
        }
    }
}
