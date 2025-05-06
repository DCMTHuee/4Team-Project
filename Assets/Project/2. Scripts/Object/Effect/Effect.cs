using System.Collections.Generic;
using UnityEngine;

namespace MoonYoHanStudy
{
    public class Effect : MonoBehaviour
    {
        public List<EffectBase> Effects;

        public ObjectStatusBase OJT; // 프리팹 문제 해결해야함. >>
        public float AttackDamageValue;
        public float PersentValue;

        private void OnTriggerEnter(Collider other)
        {
            if (OJT.gameObject.tag != other.gameObject.tag)
            {
                Use(other.gameObject);
            }
        }

        public bool Use(GameObject target)
        {
            bool isUsed = true;

            foreach (EffectBase effect in Effects)
            {
                if (effect.ExecuteRole(target, AttackDamageValue, PersentValue))
                {
                    isUsed &= effect.ExecuteRole(target, AttackDamageValue, PersentValue);
                }
            }

            return isUsed;
        }
    }
}
