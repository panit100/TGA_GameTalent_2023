using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB.Gameplay;
using System;

namespace CCB.Player
{
    public class Weapon : MonoBehaviour
    {
        //[SerializeField] BaseBullet baseBullet;
        [SerializeField] float range;
        [SerializeField] List<BaseBullet> bulletListForRandom;
        [SerializeField] int maxBullet;
        [SerializeField] List<BaseBullet> bulletList;
        [SerializeField] float fireRate;
        bool canShoot = true;

        Vector3 LookDirection = Vector3.zero;

        Ray aimRay;

        public Action addBulletToMagazine;
        public Action removeBulletToMagazine;

        void Awake() 
        {
            PlayerManager.Instance.PlayerWeapon = this;
        }

        void Start()
        {
            SetUpInputAction();
            StartCoroutine(ReloadWhenStart());
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

        IEnumerator ReloadWhenStart()
        {
            yield return new WaitUntil(() => addBulletToMagazine != null);
            Reload();
        }

        void Reload()
        {
            PlayerManager.Instance.playerAnimator.SetTrigger("Reload");

            for (var i = bulletList.Count; i < maxBullet; i++)
            {
                BaseBullet addBullet = bulletListForRandom[UnityEngine.Random.Range(0,bulletListForRandom.Count)];
                bulletList.Add(addBullet);
                addBulletToMagazine?.Invoke();
            }
        }

        IEnumerator WaitForNextShoot(float time)
        {
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
                DiscardBullet();
                return;
            }

            CheckReload();
        }

        void ShootTarget()
        {
            aimRay = new Ray(transform.position,LookDirection);
            
            if(Physics.Raycast(aimRay,out RaycastHit hit,range))
            {
                IDamageable hitObject = hit.collider.GetComponent<IDamageable>() as IDamageable;
                hitObject.ProcessDamage(bulletList[0].Damage);
            }
        }

        void SetFireRate()
        {
            fireRate = fireRate / TimeManager.Instance.GetTime(PlayerManager.Instance.PlayerMovement.timeState);
            // Set If FireRate OverCapLevel
        }

        void DiscardBullet()
        {
            removeBulletToMagazine();
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