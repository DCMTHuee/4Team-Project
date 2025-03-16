using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoonYoHanStudy
{
    public class TitleScene : SceneBase
    {
        public override IEnumerator OnStart()
        {
            var asyncLoadScene = SceneManager.LoadSceneAsync(SceneType.Title.ToString(), LoadSceneMode.Single);
            yield return new WaitUntil(() => asyncLoadScene.isDone);

            UIManager.Show<TitleUI>(UIList.TitleUI);
        }

        public override IEnumerator OnEnd()
        {
            UIManager.Hide<TitleUI>(UIList.TitleUI);

            yield return null;
        }
    }
}
