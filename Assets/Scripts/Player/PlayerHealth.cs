using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCB.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] float hp;

        void CalculateDamage(float Damage)
        {
            hp -= Damage;
            if(hp <= 0)
                OnDie();
        }

        private void OnDie()
        {
            Destroy(this.gameObject);
        }
    }
}
