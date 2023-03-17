using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCB.Player.UI
{
    public class PlayerWorldCanvasUI : MonoBehaviour
    {
        [SerializeField] GameObject BulletPrefab;
        [SerializeField] Transform BulletSlotParent;

        void Start() 
        {
            PlayerManager.Instance.PlayerWeapon.addBulletToMagazine += AddBullet;   
            PlayerManager.Instance.PlayerWeapon.removeBulletToMagazine += RemoveBullet;   
        }

        void AddBullet()
        {
            var bullet = Instantiate(BulletPrefab);
            bullet.transform.SetParent(BulletSlotParent);
        }

        void RemoveBullet()
        {
            var bullet = BulletSlotParent.GetChild(0);
            Destroy(bullet.gameObject);
        }
    }
}