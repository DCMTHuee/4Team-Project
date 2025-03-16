using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoonYoHanStudy
{
    public enum SceneType
    {
        None = 0,
        Empty,
        Title,
        InGame,

        // 튜토리얼 스테이지

        // 도심지 스테이지
    }

    public class Bigin : SingletoneBase<Bigin>
    {
        private void Start()
        {
            Initialized();
            ChangeScene(SceneType.Title);
        }

        public void Initialized()
        {
            UIManager.Singletone.Initialized();
        }

        public bool IsOnProgressSceneChange { get; private set; } = false;

        private SceneType currentSceneType = SceneType.None;
        private SceneBase currentSceneController = null;

        public void ChangeScene(SceneType sceneType, System.Action sceneLoadedCallback = null)
        {
            if(currentSceneType == sceneType)
            {
                return;
            }

            Time.timeScale = 1f;

            switch(sceneType)
            {
                case SceneType.Title:
                    ChangeScene<TitleScene>(sceneType, sceneLoadedCallback);
                    break;

                case SceneType.InGame:
                    ChangeScene<InGameScene>(sceneType, sceneLoadedCallback);
                    break;

                default:
                    throw new System.NotImplementedException();
            }
        }

        private void ChangeScene<T>(SceneType sceneType, System.Action sceneLoadedCallback = null) where T : SceneBase
        {
            StartCoroutine(ChangeSceneAsync<T>(sceneType, sceneLoadedCallback));
        }

        private IEnumerator ChangeSceneAsync<T>(SceneType sceneType, System.Action sceneLoadedCallback = null) where T : SceneBase
        {
            IsOnProgressSceneChange = true;


            UIManager.Show<LoadingUI>(UIList.LoadingUI);


            if(currentSceneController != null)
            {
                yield return StartCoroutine(currentSceneController.OnEnd());
                Destroy(currentSceneController.gameObject);
                currentSceneController = null;
            }


            var asyncToEmpty = SceneManager.LoadSceneAsync(SceneType.Empty.ToString(), LoadSceneMode.Single);
            yield return new WaitUntil(() => asyncToEmpty.isDone);


            GameObject sceneGO = new GameObject(typeof(T).Name);
            sceneGO.transform.parent = transform;
            currentSceneController = sceneGO.AddComponent<T>();
            currentSceneType = sceneType;

            yield return StartCoroutine(currentSceneController.OnStart());

            IsOnProgressSceneChange = false;
            sceneLoadedCallback?.Invoke();


            UIManager.Hide<LoadingUI>(UIList.LoadingUI);
        }

        // 게임을 종료
        public void WindowProgressQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

    }
}
