using UnityEngine;
using System.Collections;

namespace MoonYoHanStudy
{
    public abstract class SceneBase : MonoBehaviour
    {
        public abstract IEnumerator OnStart();

        public abstract IEnumerator OnEnd();
    }
}
