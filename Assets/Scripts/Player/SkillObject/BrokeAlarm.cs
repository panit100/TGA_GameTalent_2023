using System.Collections;
using System.Collections.Generic;
using CCB.Enemy;
using CCB.Player;
using UnityEngine;

namespace CCB.Player
{
    [CreateAssetMenu(fileName = "BrokeAlarm", menuName = "PlayerSkill/BrokeAlarm")]
    public class BrokeAlarm : SkillObject
    {
        [SerializeField] float TimeToStop;
        [SerializeField] float skillRadius;

        public override void Skill()
        {
            Collider[] overlapCollider = Physics.OverlapSphere(PlayerManager.Instance.transform.position,skillRadius);

            foreach(var n in overlapCollider)
            {
                if(n.TryGetComponent<BaseEnemy>(out var enemy))
                {
                    enemy.OnTimeStop(TimeToStop);
                }
            }
        }

        public float GetSkillRadius()
        {
            return skillRadius;
        }
    }
}
