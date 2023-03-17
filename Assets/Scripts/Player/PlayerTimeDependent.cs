using System.Collections;
using System.Collections.Generic;
using CCB.Player;
using UnityEngine;

namespace CCB.Player
{
   public class PlayerTimeDependent : MonoBehaviour
   {
      public void BoostComponent(float multiplier)
      {
         PlayerManager.Instance.playerAnimator.speed = Mathf.Clamp(multiplier, 1, 3f);
      }
   }
}

