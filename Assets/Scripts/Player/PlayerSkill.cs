using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCB.Player
{
    public enum SkillType
    {
        AccelTime,
        BrokeAlarm,
        FastForward,
        SlowPoke
    }

    public class PlayerSkill : MonoBehaviour
    {
        [SerializeField] PlayerSkillConfig skillConfig;
        

        void Start()
        {
            SetUpInputAction();
        }

        void SetUpInputAction()
        {
            PlayerManager.Instance.PlayerController.onActiveSkill += ActiveSkill;
            PlayerManager.Instance.PlayerController.onSwapSkill += SwapSkill;
        }

        void RemoveInputAction()
        {
            PlayerManager.Instance.PlayerController.onActiveSkill -= ActiveSkill;
            PlayerManager.Instance.PlayerController.onSwapSkill -= SwapSkill;
        }

        void ActiveSkill()
        {
            // skillConfig.Skill1.Skill();
        }

        void SwapSkill(float value)
        {
            if(value > 0)
                SwapUp();
            else if(value < 0)
                SwapDown();
        }

        void SwapUp()
        {
            Debug.Log("Swap Up");
        }

        void SwapDown()
        {
            Debug.Log("Swap Down");
        }

        void OnDestroy()
        {
            RemoveInputAction();
        }

        void OnDrawGizmos() 
        {
            // if(skillConfig.Skill1 != null)
            // {
            //     Gizmos.color = Color.yellow;

            //     switch (skillConfig.Skill1.GetSkillType())
            //     {
            //         case SkillType.BrokeAlarm :
            //             Gizmos.DrawWireSphere(transform.position,((BrokeAlarm)skillConfig.Skill1).GetSkillRadius());
            //             break;
            //         case SkillType.SlowPoke:
            //             Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position + (transform.forward * (((SlowPoke)skillConfig.Skill1).skillRange.z/2)), transform.rotation, transform.lossyScale);
            //             Gizmos.matrix = rotationMatrix;
            //             Gizmos.DrawWireCube(Vector3.zero, ((SlowPoke)skillConfig.Skill1).GetSkillRange());
            //             break;
            //         case SkillType.AccelTime:
            //             break;
            //         case SkillType.FastForward:
            //             break;
            //     }
            // }
        }
    }
}
