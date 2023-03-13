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

        PlayerMovement playerMovement;

        public PlayerController PlayerController {get {return playerController;}}

        public PlayerMovement PlayerMovement { get { return playerMovement; } }

        protected override void InitAfterAwake()
        {
            playerController = GetComponent<PlayerController>();
            playerMovement = GetComponent<PlayerMovement>();
        }   
    }
}
