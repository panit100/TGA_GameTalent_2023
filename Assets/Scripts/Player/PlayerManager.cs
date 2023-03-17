using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB.Utility;

namespace CCB.Player
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerTimeDependent))]
    [RequireComponent(typeof(PlayerSkill))]
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerManager : Singleton<PlayerManager>
    {
        private PlayerController playerController;

        private PlayerMovement playerMovement;

        private PlayerTimeDependent playerTimeDependent;

        public PlayerController PlayerController {get {return playerController;}}

        public PlayerMovement PlayerMovement { get { return playerMovement;}}
        public PlayerTimeDependent PlayerTimeDependent { get { return playerTimeDependent;}}

        public Animator playerAnimator;

        protected override void InitAfterAwake()
        {
            playerController = GetComponent<PlayerController>();
            playerMovement = GetComponent<PlayerMovement>();
            playerTimeDependent = GetComponent<PlayerTimeDependent>();
        }   
    }
}
