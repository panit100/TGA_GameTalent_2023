using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCB.Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] BaseBullet baseBullet;

        void Start()
        {
            PlayerManager.Instance.PlayerController.onShoot += Shoot;
        }

        void Shoot()
        {
            Debug.Log("Shoot!!");
        }
    }
}
