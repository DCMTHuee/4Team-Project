using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoonYoHanStudy
{
    // , float AttackDamageValue, float PersentValue
    public abstract class EffectBase : ScriptableObject
    {
        public abstract bool ExecuteRole(GameObject target, float AttackDamageValue, float PersentValue);
    }

    [CreateAssetMenu(fileName = "StunEffect", menuName = "Scriptable Objects/Effects/StunEffect")]
    public class StunEffect : EffectBase
    {
        public float duration = 2f;

        public override bool ExecuteRole(GameObject target, float AttackDamageValue, float PersentValue)
        {
            Debug.Log("Ω∫≈œ");
            
            var targetData = target.GetComponent<ObjectStatusBase>();
            if (targetData != null)
            {
                targetData.ApplyStatusEffect(StatusEffect.Stunned, duration);
                return true;
            }
            return false;
        }
    }

    [CreateAssetMenu(fileName = "BleedEffect", menuName = "Scriptable Objects/Effects/BleedEffect")]
    public class BleedEffect : EffectBase
    {
        public float tickInterval = 0.2f;
        public float damagePerTick = 10f;
        public float duration = 3f;

        public override bool ExecuteRole(GameObject target, float AttackDamageValue, float PersentValue)
        {
            var targetData = target.GetComponent<ObjectStatusBase>();
            if (targetData != null)
            {
                targetData.ApplyStatusEffect(StatusEffect.Bleed, duration);
                targetData.StartCoroutine(ApplyBleed(targetData));
                return true;
            }
            return false;
        }

        IEnumerator ApplyBleed(ObjectStatusBase target)
        {
            float timer = duration;
            while (timer > 0)
            {
                target.TakeDamage(damagePerTick);
                timer -= tickInterval;
                yield return new WaitForSeconds(tickInterval);
            }
        }
    }
}
