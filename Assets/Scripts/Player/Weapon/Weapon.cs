using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB.Enemy;

namespace CCB.Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] BaseBullet baseBullet;
        [SerializeField] float range;

        Vector3 LookDirection = Vector3.zero;

        Ray aimRay;

        void Start()
        {
            PlayerManager.Instance.PlayerController.onShoot += Shoot;
        }

        void FixedUpdate() 
        {
            LookDirection = PlayerManager.Instance.PlayerController.LookAtDirection();
        }

        void Shoot()
        {
            aimRay = new Ray(transform.position,LookDirection);
            if(Physics.Raycast(aimRay,out RaycastHit hit,range))
            {
                IDamageable hitObject = hit.collider.GetComponent<IDamageable>() as IDamageable;
                hitObject.ProcessDamage(baseBullet.Damage);
                Debug.Log("Shoot!!");
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position,LookDirection * range);
        }
    }
}
