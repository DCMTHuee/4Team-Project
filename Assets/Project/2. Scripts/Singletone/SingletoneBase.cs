using System;
using Unity.VisualScripting;
using UnityEngine;

namespace MoonYoHanStudy
{
    // �̱����ε� �������� �ڱ⸦ ������ �� �Ǵ� ���θ� ������ Ŭ����
    public class DontDestroyOnLoadOn { }
    public class DontDestroyOnLoadOff { }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T_ClassType">�̱��� ��Ű���� Ŭ����Ÿ��</typeparam>
    /// <typeparam name="T_DontDestroyOnLoad">DontDestroyOnLoad On/Off Ÿ��</typeparam>
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
            T_ClassType instance = FindAnyObjectByType<T_ClassType>(); // Ŭ������ �˻��ؼ� instance�ȿ� �������

            if (instance == null) // instance�� null�̶�� T_ClassType�� ���� GameObject�� �����ؼ� AddComponent�� �������
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

        // ���Ǻθ� Ȯ���ϰ�, �޾ƿ� GameObject�� �ı���Ű�� �ʴ´�.
        private static void DontDestroyOnLoad_Execution(GameObject obj)
        {
                // #if UNITY_EDITOR

            if (typeof(T_DontDestroyOnLoad) == typeof(DontDestroyOnLoadOn)) // T_DontDestroyOnLoad�� Ÿ���� On�̸� DontDestroyOnLoad() �Լ��� �����.
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
