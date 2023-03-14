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
        public Vector3 skillRange;
        [SerializeField] float slowDuration;

        public override void Skill()
        {
            Collider[] enemyList = Physics.OverlapBox(GetStartPosition(), skillRange/2 , PlayerManager.Instance.transform.rotation);
            foreach (var n in enemyList)
            {
                if(n.GetComponent<BaseEnemy>() != null)
                {
                    n.GetComponent<BaseEnemy>().OnSlowPokeActivated(slowDuration);
                }
            }
        }

        public Vector3 GetStartPosition()
        {
            Vector3 newPosition = PlayerManager.Instance.transform.position + (PlayerManager.Instance.transform.forward * (skillRange.z/2));
            return newPosition;
        }
        public Vector3 GetSkillRange()
        {
            return skillRange;
        }
    }

}
