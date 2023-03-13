using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCB.Player
{
    public enum SkillType
    {
        BrokeAlarm,
    }

    public class PlayerSkill : MonoBehaviour
    {
        [SerializeField] PlayerSkillConfig skillConfig;

        void Start()
        {
            PlayerManager.Instance.PlayerController.onActiveSkill += ActiveSkill;
        }

        void ActiveSkill()
        {
            skillConfig.Skill1.Skill();
        }

        void OnDrawGizmos() 
        {

            switch(skillConfig.Skill1.GetSkillType())
            {
                case SkillType.BrokeAlarm :
                    Gizmos.DrawWireSphere(transform.position,((BrokeAlarm)skillConfig.Skill1).GetSkillRadius());
                    break;
            }
        }
    }
}
