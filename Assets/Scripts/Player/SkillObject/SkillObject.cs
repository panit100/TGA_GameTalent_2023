using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCB.Player
{
    public abstract class SkillObject : ScriptableObject
    {
        SkillType skillType;
        public abstract void Skill();
    }
}