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

        // Ʃ�丮�� ��������

        // ������ ��������
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

        // ������ ����
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
