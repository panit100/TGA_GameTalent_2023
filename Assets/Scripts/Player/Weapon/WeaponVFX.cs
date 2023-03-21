using System;
using System.Collections;
using System.Collections.Generic;
using CCB.Player;
using Unity.Mathematics;
using UnityEngine;

public class WeaponVFX : MonoBehaviour
{
   [SerializeField] private GameObject VfxPrefab;
   
   private GameObject Instance;
   private Hovl_Laser LaserScript;
   private Hovl_Laser2 LaserScript2;

   private Weapon _weapon;
   private void Start()
   {
      _weapon = GetComponent<Weapon>();
   }


   public void CreateShootVFX(Vector3 _position ,quaternion direction)
   {
      Destroy(Instance);
      Instance = Instantiate(VfxPrefab, _position, direction);
      Instance.transform.parent = transform;
      LaserScript = Instance.GetComponent<Hovl_Laser>();
      LaserScript2 = Instance.GetComponent<Hovl_Laser2>();
      LaserScript.MaxLength = _weapon.Range;

   }

   public void WaitDestroyVFX(float Sec)
   {
      Invoke("DestroyshootVFX",Sec);
   }

   public void DestroyshootVFX()
   {
      if(LaserScript) LaserScript.DisablePrepare();
      if(LaserScript2) LaserScript2.DisablePrepare();
      Destroy(Instance,1);
   }
}
