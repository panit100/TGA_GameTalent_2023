using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB.Gameplay;

namespace CCB.Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] BaseBullet baseBullet;
        [SerializeField] float range;
        [SerializeField] int maxBullet;
        [SerializeField] List<BaseBullet> bulletList;
        [SerializeField] float fireRate;
        bool canShoot = true;

        Vector3 LookDirection = Vector3.zero;

        Ray aimRay;

        void Start()
        {
            Reload();
            PlayerManager.Instance.PlayerController.onShoot += Shoot;
        }

        void FixedUpdate() 
        {
            LookDirection = PlayerManager.Instance.PlayerController.LookAtDirection();
        }

        bool CheckReload()
        {
            if (bulletList.Count <= 0)
            {
                print("Reload");
                return true;
            }
            return false;
        }

        void Reload()
        {
            for (var i = 0; i < maxBullet; i++)
            {
                bulletList.Add(baseBullet);
            }
        }

        IEnumerator WaitForNextShoot(float time)
        {
            yield return new WaitForSeconds(time);
            canShoot = true;
        }

        void Shoot()
        {
            if (CheckReload())
            {
                Reload();
            }
            else if (!CheckReload() && canShoot == true)
            {
                canShoot = false;
                StartCoroutine(WaitForNextShoot(fireRate));

                aimRay = new Ray(transform.position,LookDirection);
                if(Physics.Raycast(aimRay,out RaycastHit hit,range))
                {
                    IDamageable hitObject = hit.collider.GetComponent<IDamageable>() as IDamageable;
                    hitObject.ProcessDamage(baseBullet.Damage);
                    Debug.Log("Shoot!!");
                }
                bulletList.RemoveAt(0);
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position,LookDirection * range);
        }
    }
}
