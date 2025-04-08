using System.Collections.Generic;
using UnityEngine;

namespace MoonYoHanStudy
{
    public abstract class ObjectStatusBase : MonoBehaviour
    {
        internal StatusEffect currentSE = StatusEffect.None;

        internal float MaxHP;
        internal float MaxST;
        internal float MoveSpeed;

        protected Dictionary<StatusEffect, float> statusTimers = new();
        protected GameObject statusUI;

        protected virtual void Update()
        {
            UpdateStatusEffects();
        }

        protected void UpdateStatusEffects()
        {
            List<StatusEffect> expired = new();

            foreach (var kvp in statusTimers)
            {
                statusTimers[kvp.Key] -= Time.deltaTime;
                if (statusTimers[kvp.Key] <= 0)
                    expired.Add(kvp.Key);
            }

            foreach (var effect in expired)
            {
                currentSE &= ~effect;
                statusTimers.Remove(effect);
                RemoveStatusIcon(effect);
            }
        }

        public virtual void ApplyStatusEffect(StatusEffect effect, float duration)
        {
            currentSE |= effect;
            statusTimers[effect] = duration;
            ShowStatusIcon(effect);
        }

        protected virtual void ShowStatusIcon(StatusEffect effect)
        {
            StatusIconManager.Instance.ShowIcon(statusUI, effect);
        }

        protected virtual void RemoveStatusIcon(StatusEffect effect)
        {
            StatusIconManager.Instance.RemoveIcon(statusUI, effect);
        }

        public abstract void TakeDamage(float amount);
    }
}
