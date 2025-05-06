using System.Collections.Generic;
using UnityEngine;

namespace MoonYoHanStudy
{
    [System.Serializable]
    public struct Skill
    {
        public string skillName;
        public float[] AttackPercent;
        public GameObject skillPrefab;
    }

    public class SkillData : MonoBehaviour
    {
        [SerializeField] private List<Skill> Skill_List;
        public Dictionary<string, Skill> Skill_Dictionary;

        public void Initialize()
        {
            Skill_Dictionary = new Dictionary<string, Skill>();

            foreach (var data in Skill_List)
            {
                if (!Skill_Dictionary.ContainsKey(data.skillName))
                    Skill_Dictionary.Add(data.skillName, data);
                else
                    Debug.LogWarning($"[Skill] Duplicate type: {data.skillName}");
            }
        }

        public Skill GetData(string skillName)
        {
            if (Skill_Dictionary == null)
                Initialize();

            if (Skill_Dictionary.TryGetValue(skillName, out var result))
                return result;

            Debug.LogError($"[Skill] No data found for MonsterType: {skillName}");
            return default;
        }
    }
}
