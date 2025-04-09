using UnityEngine;

namespace MoonYoHanStudy
{
    

    public enum PlayerType
    {
        None,
        Man,
        Woman,
    }

    public enum MonsterType
    {
        None,
        Tree,
        Boss,
    }

    public enum MonsterState
    {
        Idle,
        Move,
        Attack,
    }

    [System.Flags]
    public enum StatusEffect
    {
        None = 0,
        Stunned = 1 << 0,  // 1
        Slowed = 1 << 1,  // 2
        Airborne = 1 << 2,  // 4
        Burning = 1 << 3,  // 8
        Frozen = 1 << 4,  // 16
        Bleed = 1 << 5, // 32
                          // ...추가 가능
    }



    public enum TargetObject
    {
        Lightbringer_Stone,
        Player,
        Trap_Object,
        Normal_Object,
    }
}
