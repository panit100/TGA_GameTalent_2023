using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB.Utility;

namespace CCB.Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerManager : Singleton<PlayerManager>
    {
        PlayerController playerController;

        public PlayerController PlayerController {get {return playerController;}}

        protected override void InitAfterAwake()
        {
            playerController = GetComponent<PlayerController>();
        }   
    }
}
