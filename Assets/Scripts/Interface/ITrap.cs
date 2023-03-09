using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCB.Enviroment
{
    public interface ITrap
    {
        void ProcessDamage(IDamageable target,float damage);
    }
}
