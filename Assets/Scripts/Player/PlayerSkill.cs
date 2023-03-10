using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCB.Player
{
    enum SkillType
    {

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
            Debug.Log("Active Skill!!");
        }
    }
}
