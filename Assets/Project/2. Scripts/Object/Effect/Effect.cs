using System.Collections.Generic;
using UnityEngine;

namespace MoonYoHanStudy
{
    public class Effect : MonoBehaviour
    {
        public List<EffectBase> Effects;

        public bool Use(GameObject target)
        {
            bool isUsed = true;

            foreach (EffectBase effect in Effects)
            {
                isUsed &= effect.ExecuteRole(target);
            }
            return isUsed;
        }
    }
}
