using UnityEngine;

namespace MoonYoHanStudy
{
    /*

    public abstract class ItemEffect : ScriptableObject
{
    public abstract bool ExecuteRole(GameObject target);
}

    [CreateAssetMenu(fileName = "StunEffect", menuName = "Scriptable Objects/Effects/StunEffect")]
public class StunEffect : ItemEffect
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
public class BleedEffect : ItemEffect
{
    public float tickInterval = 0.2f;
    public float damagePerTick = 10f;
    public float duration = 3f;

    public override bool ExecuteRole(GameObject target)
    {
        var enemy = target.GetComponent<Enemy_Test1>();
        if (enemy != null)
        {
            enemy.ApplyStatusEffect(StatusEffect.Bleeding, duration);
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

    public class Item
{
    public List<ItemEffect> itemEffects;

    public bool Use(GameObject target)
    {
        bool isUsed = true;
        foreach (ItemEffect effect in itemEffects)
        {
            isUsed &= effect.ExecuteRole(target);
        }
        return isUsed;
    }
}













    public class CharacterStats : MonoBehaviour
{
    public float MaxHP;
    public float CurrentHP;

    public float MaxStamina;
    public float CurrentStamina;

    public float MoveSpeed;
    public float AttackPower;
    public float Defense;
    public float CriticalChance;
    public float CriticalDamage;

    public float AttackSpeed;
    public float KnockbackResistance;

    public float StatusResistance; // 상태이상 저항률
    public float RegenerationRate; // 체력 재생

    // 예: 회피율, 히트리액션 저항 등도 가능
}


    public virtual void TakeDamage(float damage)
{
    float finalDamage = Mathf.Max(damage - Defense, 1f);
    CurrentHP -= finalDamage;

    if (CurrentHP <= 0)
    {
        Die();
    }
}


    protected virtual void Update()
{
    UpdateStatusEffects();
    RegenerateHealth();
}

void RegenerateHealth()
{
    if (CurrentHP < MaxHP)
    {
        CurrentHP += RegenerationRate * Time.deltaTime;
        CurrentHP = Mathf.Min(CurrentHP, MaxHP);
    }
}


    public void ApplyStatusEffect(StatusEffect effect, float duration)
{
    float resistChance = Random.value;
    if (resistChance < StatusResistance)
    {
        return; // 저항 성공
    }

    statusEffectManager.ApplyEffect(effect, duration);
}


    public abstract class CharacterBase : MonoBehaviour
{
    public CharacterStats Stats;
    protected StatusEffectManager statusEffectManager;

    protected virtual void Awake()
    {
        statusEffectManager = GetComponent<StatusEffectManager>();
    }

    protected virtual void Update()
    {
        statusEffectManager?.UpdateEffects();
        RegenerateHealth();
    }

    public abstract void Die();
}


     */
}
