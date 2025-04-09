using System.Collections;
using UnityEngine;

namespace MoonYoHanStudy
{
    public abstract class EffectBase : ScriptableObject
    {
        public abstract bool ExecuteRole(GameObject target);
    }

    [CreateAssetMenu(fileName = "StunEffect", menuName = "Scriptable Objects/Effects/StunEffect")]
    public class StunEffect : EffectBase
    {
        public float duration = 2f;

        public override bool ExecuteRole(GameObject target)
        {
            var enemy = target.GetComponent<Enemy_Test1>();
            if (enemy != null)
            {
                enemy.ApplyStatusEffect(StatusEffect.Stunned, duration);
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

        public override bool ExecuteRole(GameObject target)
        {
            var enemy = target.GetComponent<Enemy_Test1>();
            if (enemy != null)
            {
                enemy.ApplyStatusEffect(StatusEffect.Bleed, duration);
                enemy.StartCoroutine(ApplyBleed(enemy));
                return true;
            }
            return false;
        }

        IEnumerator ApplyBleed(Enemy_Test1 enemy)
        {
            float timer = duration;
            while (timer > 0)
            {
                enemy.TakeDamage(damagePerTick);
                timer -= tickInterval;
                yield return new WaitForSeconds(tickInterval);
            }
        }
    }
}
