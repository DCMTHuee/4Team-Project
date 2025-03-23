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

            UIManager.Show<InGameUI>(UIList.InGameUI);
        }

        public override IEnumerator OnEnd()
        {
            UIManager.Hide<InGameUI>(UIList.TitleUI);

            yield return null;
        }
    }
}
