using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB.Gameplay;

namespace CCB.Player
{
    public class Weapon : MonoBehaviour
    {
        //[SerializeField] BaseBullet baseBullet;
        [SerializeField] float range;
        public float Range => range;
        [SerializeField] List<BaseBullet> bulletListForRandom;
        [SerializeField] int maxBullet;
        [SerializeField] List<BaseBullet> bulletList;
        [SerializeField] float fireRate;
        bool canShoot = true;
        [SerializeField]
        private WeaponVFX _weaponVFX;
        Vector3 LookDirection = Vector3.zero;

        Ray aimRay;

        void Start()
        {
            SetUpInputAction();

            Reload();
        }

        void SetUpInputAction()
        {
            PlayerManager.Instance.PlayerController.onShoot += Shoot;
            PlayerManager.Instance.PlayerController.onReload += Reload;
            PlayerManager.Instance.PlayerController.onDiscardBullet += DiscardBullet;
        }

        void RemoveInputAction()
        {
            PlayerManager.Instance.PlayerController.onShoot -= Shoot;
            PlayerManager.Instance.PlayerController.onReload -= Reload;
            PlayerManager.Instance.PlayerController.onDiscardBullet -= DiscardBullet;
        }

        void FixedUpdate() 
        {
            LookDirection = PlayerManager.Instance.PlayerController.LookAtDirection();
        }

        bool CheckReload()
        {
            if (bulletList.Count <= 0)
            {
                Reload();
                return true;
            }
            return false;
        }

        void Reload()
        {
            PlayerManager.Instance.playerAnimator.SetTrigger("Reload");

            for (var i = bulletList.Count; i < maxBullet; i++)
            {
                BaseBullet addBullet = bulletListForRandom[Random.Range(0,bulletListForRandom.Count)];
                bulletList.Add(addBullet);
            }
        }

        IEnumerator WaitForNextShoot(float time)
        {
            _weaponVFX.WaitDestroyVFX(0.1f);
            yield return new WaitForSeconds(time);
            
            canShoot = true;
        }

        void Shoot()
        {
            if (!CheckReload() && canShoot == true)
            {
                
                canShoot = false;
                PlayerManager.Instance.playerAnimator.SetTrigger("Shoot");
                
                //SetFireRate();
                StartCoroutine(WaitForNextShoot(fireRate));
               
                ShootTarget();
                bulletList.RemoveAt(0);
                return;
            }
           
           
            
            CheckReload();
        }

        void ShootTarget()
        {
            aimRay = new Ray(transform.position,LookDirection);
            _weaponVFX.CreateShootVFX(transform.position,transform.rotation);
            if(Physics.Raycast(aimRay,out RaycastHit hit,range))
            {
                IDamageable hitObject = hit.collider.GetComponent<IDamageable>() as IDamageable;
                hitObject.ProcessDamage(bulletList[0].Damage);
            }
           
        }

        void SetFireRate()
        {
            fireRate = fireRate / TimeManager.Instance.GetTime(PlayerManager.Instance.PlayerMovement.TimeState);
            // Set If FireRate OverCapLevel
        }

        void DiscardBullet()
        {
            bulletList.RemoveAt(0);
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position,LookDirection * range);
        }

        void OnDestroy() 
        {
            RemoveInputAction();
        }
    }
}