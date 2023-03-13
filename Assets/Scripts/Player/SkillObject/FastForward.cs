using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB.Utility;
using CCB.Enemy;
using CCB.Enviroment;
using CCB.Gameplay;
using System;


namespace CCB.Player
{
    [CreateAssetMenu(fileName = "FastForward", menuName = "PlayerSkill/FastForward")]
    public class FastForward : SkillObject
    {

        //private List<Trap> trapList;

        [SerializeField]
        private float skillDuration;

        public override void Skill()
        {
            var allEnemyList = FindObjectsOfTypeAll(typeof(BaseEnemy));

            foreach (BaseEnemy baseEnemy in allEnemyList)
            {
                Debug.Log(baseEnemy.name);
                baseEnemy.OnFastForwardActivated(skillDuration);
            }

            PlayerManager.Instance.PlayerMovement.OnFastForwardActivated(skillDuration);
            
        }
    }
}
