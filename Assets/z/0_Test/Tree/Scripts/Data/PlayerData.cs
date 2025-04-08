using System.Collections.Generic;
using UnityEngine;

namespace MoonYoHanStudy
{
    [System.Serializable]
    public struct PlayerDataBase
    {
        public PlayerType type;
        public float MaxHP;
        public float MaxST;
        public float Speed;
        public GameObject[] skillPrefab;
    }

    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private List<PlayerDataBase> Player_LIST;
        public Dictionary<PlayerType, PlayerDataBase> Player_Dictionary;

        public void Initialize()
        {
            Player_Dictionary = new Dictionary<PlayerType, PlayerDataBase>();

            foreach (var data in Player_LIST)
            {
                if (!Player_Dictionary.ContainsKey(data.type))
                    Player_Dictionary.Add(data.type, data);
                else
                    Debug.LogWarning($"[EnemyData] Duplicate type: {data.type}");
            }
        }

        public PlayerDataBase GetData(PlayerType type)
        {
            if (Player_Dictionary == null)
                Initialize();

            if (Player_Dictionary.TryGetValue(type, out var result))
                return result;

            Debug.LogError($"[EnemyData] No data found for MonsterType: {type}");
            return default;
        }
    }
}

