using System.Collections;
using System.Collections.Generic;
using CCB.Player;
using UnityEngine;

namespace CCB.Player
{
    [CreateAssetMenu(fileName = "AccelTime", menuName = "PlayerSkill/AccelTime")]
    public class AccelTime : SkillObject
    {
        [SerializeField]
        private float skillDurationSec;
        [SerializeField]
        private float skillSpeedMultiplier;

        public override void Skill()
        {
            PlayerManager.Instance.PlayerMovement.OnAccelTimeActivated(skillDurationSec,skillSpeedMultiplier);
        }
        
        
    }

}
