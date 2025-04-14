using System.Collections.Generic;
using UnityEngine;

namespace MoonYoHanStudy
{
    public class Effect : MonoBehaviour
    {
        public List<EffectBase> itemEffects;

        public bool Use(GameObject target)
        {
            bool isUsed = true;

            foreach (EffectBase effect in itemEffects)
            {
                isUsed &= effect.ExecuteRole(target);
            }
            return isUsed;
        }
    }
}
