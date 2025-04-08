using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace MoonYoHanStudy
{
    [System.Serializable]
    public struct EnemyDataBase
    {
        public MonsterType type;
        public float MaxHP;
        public float MaxST;
        public float Speed;
        public GameObject[] skillPrefab;
    }

    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private List<EnemyDataBase> MONSTER_LIST;
        public Dictionary<MonsterType, EnemyDataBase> MONSTER_Dictionary;

        public void Initialize()
        {
            MONSTER_Dictionary = new Dictionary<MonsterType, EnemyDataBase>();

            foreach (var data in MONSTER_LIST)
            {
                if (!MONSTER_Dictionary.ContainsKey(data.type))
                    MONSTER_Dictionary.Add(data.type, data);
                else
                    Debug.LogWarning($"[EnemyData] Duplicate type: {data.type}");
            }
        }

        public EnemyDataBase GetData(MonsterType type)
        {
            if (MONSTER_Dictionary == null)
                Initialize();

            if (MONSTER_Dictionary.TryGetValue(type, out var result))
                return result;

            Debug.LogError($"[EnemyData] No data found for MonsterType: {type}");
            return default;
        }
    }
}
