using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCB.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] float healthPoint;

        public void ProcessDamage(float damage)
        {
            healthPoint -= damage;
            if(healthPoint <= 0)
                OnDie();
        }

        void OnDie()
        {

        }
    }
}
