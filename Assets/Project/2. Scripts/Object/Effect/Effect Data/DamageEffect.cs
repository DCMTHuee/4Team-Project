using UnityEngine;

namespace MoonYoHanStudy
{
    [CreateAssetMenu(fileName = "DamageEffect", menuName = "Scriptable Objects/Effects/DamageEffect")]
    public class DamageEffect : EffectBase
    {
        public override bool ExecuteRole(GameObject target, float AttackDamageValue, float PersentValue)
        {
            var targetData = target.GetComponent<ObjectStatusBase>();

            if (targetData != null)
            {
                targetData.TakeDamage(AttackDamageValue * PersentValue);

                return true;
            }

            return false;
        }
    }
}
