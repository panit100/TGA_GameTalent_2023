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
        [SerializeField] float angle = 120;
        [SerializeField] float radius = 5;
        [SerializeField] int segments = 100;

        void OnDrawGizmos() 
        {
            Gizmos.color = Color.yellow;
            float deltaAngle = angle / segments;
            Vector3 forward = transform.forward;
            Vector3[] vertices = new Vector3[segments + 2];
            vertices[0] = transform.position;
            for (int i = 1; i < vertices.Length; i++)
            {
                Vector3 pos = Quaternion.Euler(0f, -angle / 2 + deltaAngle * (i - 1), 0f) * forward * radius + transform.position;
                vertices[i] = pos;
            }
            for (int i = 1; i < vertices.Length - 1; i++)
            {
                Gizmos.DrawLine(vertices[i], vertices[i + 1]);
            }
            Gizmos.DrawLine(vertices[0], vertices[vertices.Length - 1]);
            Gizmos.DrawLine(vertices[0], vertices[1]);

            switch (skillConfig.Skill1.GetSkillType())
            {
                case SkillType.BrokeAlarm :
                    Gizmos.DrawWireSphere(transform.position,((BrokeAlarm)skillConfig.Skill1).GetSkillRadius());
                    break;
                case SkillType.SlowPoke:
                    break;
            }
        }
    }
}
