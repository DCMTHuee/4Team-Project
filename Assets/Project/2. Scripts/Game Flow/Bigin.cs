using System.Collections;
using UnityEngine;

namespace MoonYoHanStudy
{
    public enum SceneType
    {
        None = 0,
        Bigin,
        Title,
        InGame,

        // 튜토리얼 스테이지

        // 도심지 스테이지
    }

    public class Bigin : SingletoneBase<Bigin>
    {
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
                case SceneType.Bigin:

                    break;

                case SceneType.Title:

                    break;

                case SceneType.InGame:

                    break;
            }
        }

        private void ChangeScene<T>(SceneType sceneType, System.Action sceneLoadedCallback = null) where T : SceneBase
        {

        }

        private IEnumerator ChangeSceneAsync<T>(SceneType sceneType, System.Action sceneLoadedCallback = null) where T : SceneBase
        {
            IsOnProgressSceneChange = true;

            

            yield return null;
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
