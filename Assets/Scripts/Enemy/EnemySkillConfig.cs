using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace CCB.Enemy
{
    [Serializable]
    public class EnemySkillConfig
    {
        [SerializeField] EnemySkill onAttackSkill;
        [SerializeField] EnemySkill onDieSkill;

        public EnemySkill OnAttackSkill {get {return onAttackSkill;}}
        public EnemySkill OnDieSkill {get {return onDieSkill;}}
    }
}
