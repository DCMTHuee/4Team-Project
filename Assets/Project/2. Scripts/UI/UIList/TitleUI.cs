using UnityEngine;

namespace MoonYoHanStudy
{
    public class TitleUI : UIBase
    {
        public void OnClickGameStartButton()
        {
            Bigin.Singletone.ChangeScene(SceneType.InGame);
        }
    }
}
