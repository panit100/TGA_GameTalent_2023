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

        private int currentSkill;
        

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
            skillConfig.skillObjects[currentSkill].Skill();
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
            currentSkill += 1;
            currentSkill %= skillConfig.skillObjects.Count;
        }

        void SwapDown()
        {
            currentSkill -= 1;
            if(currentSkill < 0)
            {
                currentSkill = skillConfig.skillObjects.Count - 1;
            }
            currentSkill %= skillConfig.skillObjects.Count;
        }

        void OnDestroy()
        {
            RemoveInputAction();
        }

        void OnDrawGizmos() 
        {
            if(skillConfig == null)
                return;

            if(skillConfig.skillObjects.Count == 0)
                return;

            Gizmos.color = Color.yellow;
            switch (skillConfig.skillObjects[currentSkill].GetSkillType())
            {
                case SkillType.BrokeAlarm :
                    Gizmos.DrawWireSphere(transform.position,((BrokeAlarm)skillConfig.skillObjects[currentSkill]).GetSkillRadius());
                    break;
                case SkillType.SlowPoke:
                    Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position + (transform.forward * (((SlowPoke)skillConfig.skillObjects[currentSkill]).skillRange.z/2)), transform.rotation, transform.lossyScale);
                    Gizmos.matrix = rotationMatrix;
                    Gizmos.DrawWireCube(Vector3.zero, ((SlowPoke)skillConfig.skillObjects[currentSkill]).GetSkillRange());
                    break;
                case SkillType.AccelTime:
                    break;
                case SkillType.FastForward:
                    break;
            }
        }
    }
}
