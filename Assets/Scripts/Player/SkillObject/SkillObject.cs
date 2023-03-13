using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCB.Player
{
    public abstract class SkillObject : ScriptableObject
    {
        [SerializeField] SkillType skillType;
        [SerializeField] float skillCooldown;
        public abstract void Skill();

        public SkillType GetSkillType()
        {
            return skillType;
        }

        public float GetSkillCooldown()
        {
            return skillCooldown;
        }
    }
}