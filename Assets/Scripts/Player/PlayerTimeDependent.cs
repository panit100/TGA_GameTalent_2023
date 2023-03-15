using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeDependent : MonoBehaviour
{
   [SerializeField] private Animator PlayerAnim;

   public void BoostComponent(float multiplier)
   {
      PlayerAnim.speed = Mathf.Clamp(multiplier, 1, 3f);
   }
}
