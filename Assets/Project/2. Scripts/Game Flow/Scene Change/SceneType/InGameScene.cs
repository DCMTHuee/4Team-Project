using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace MoonYoHanStudy
{
    public class InGameScene : SceneBase
    {
        public override IEnumerator OnStart()
        {
            var asyncLoadScene = SceneManager.LoadSceneAsync(SceneType.InGame.ToString(), LoadSceneMode.Single);
            yield return new WaitUntil(() => asyncLoadScene.isDone);

            GameManager.Singletone.SetCursorVisible(false);
            UIManager.Show<InGameUI>(UIList.InGameUI);
            UIManager.Hide<CreaftingUI>(UIList.CreaftingUI);
        }

        public override IEnumerator OnEnd()
        {
            GameManager.Singletone.SetCursorVisible(true);
            UIManager.Hide<InGameUI>(UIList.InGameUI);

            yield return null;
        }
    }
}
