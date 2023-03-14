using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCB.Player
{
    public enum SkillType
    {
        BrokeAlarm,
        FastForward,
        SlowPoke
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
            Gizmos.color = Color.yellow;
            

            switch (skillConfig.Skill1.GetSkillType())
            {
                case SkillType.BrokeAlarm :
                    Gizmos.DrawWireSphere(transform.position,((BrokeAlarm)skillConfig.Skill1).GetSkillRadius());
                    break;
                case SkillType.SlowPoke:
                    Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position + (transform.forward * (((SlowPoke)skillConfig.Skill1).skillRange.z/2)), transform.rotation, transform.lossyScale);
                    Gizmos.matrix = rotationMatrix;
                    Gizmos.DrawWireCube(Vector3.zero, ((SlowPoke)skillConfig.Skill1).GetSkillRange());
                    break;
            }
        }
    }
}
