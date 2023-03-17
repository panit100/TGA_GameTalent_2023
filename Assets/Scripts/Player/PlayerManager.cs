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
        PlayerController playerController;
        PlayerMovement playerMovement;
        PlayerTimeDependent playerTimeDependent;
        Weapon playerWeapon;

        public PlayerController PlayerController {get {return playerController;}}

        public PlayerMovement PlayerMovement { get { return playerMovement;}}
        public PlayerTimeDependent PlayerTimeDependent { get { return playerTimeDependent;}}
        public Weapon PlayerWeapon {get {return playerWeapon;} set {playerWeapon = value;}}

        public Animator playerAnimator;

        protected override void InitAfterAwake()
        {
            playerController = GetComponent<PlayerController>();
            playerMovement = GetComponent<PlayerMovement>();
            playerTimeDependent = GetComponent<PlayerTimeDependent>();
        }   
    }
}
