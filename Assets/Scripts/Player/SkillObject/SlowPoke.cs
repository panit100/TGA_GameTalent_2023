using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB.Player;
using CCB.Enemy;

namespace CCB.Player
{
    [CreateAssetMenu(fileName = "SlowPoke", menuName = "PlayerSkill/SlowPoke")]
    public class SlowPoke : SkillObject
    {
        [SerializeField] float damage;
        [SerializeField] float skillRange;
        [SerializeField] float slowDuration;

        List<BaseEnemy> allEnemyList;
        public override void Skill()
        {
            Debug.Log("OK");
            var allEnemyList = FindObjectsOfTypeAll(typeof(BaseEnemy));
            foreach (BaseEnemy baseEnemy in allEnemyList)
            {
                baseEnemy.OnSlowPokeActivated(slowDuration);

            }
        }
        
    }

}
