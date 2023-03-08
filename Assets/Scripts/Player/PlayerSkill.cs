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
