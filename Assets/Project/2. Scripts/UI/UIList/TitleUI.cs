using UnityEngine;

namespace MoonYoHanStudy
{
    public class TitleUI : UIBase
    {
        public void Option_Button()
        {
            Debug.Log("�ɼ�â ���");
        }

        public void Start_Button()
        {
            Bigin.Singletone.ChangeScene(SceneType.InGame);
        }

        public void Exit_Button()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
