using UnityEngine;

namespace MoonYoHanStudy
{
    public abstract class Enemy_Base : MonoBehaviour
    {
        internal float MaxHP;
        internal float MaxST;

        public abstract void TakeDamage(float Damage);
    }
}