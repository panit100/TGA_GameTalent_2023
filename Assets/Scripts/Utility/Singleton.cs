using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCB.Utility
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance {get; protected set;}

        protected virtual void Awake() 
        {
            InitAfterAwake();
        }

        protected abstract void InitAfterAwake();

        protected Singleton()
        {
            // if(Instance != null && Instance != this)
            // {
            //     Instance = this as T;
            //     throw new System.Exception($"There are 2 Singleton in Scene. Plase remove one of them.");
            // }
            // else
                Instance = this as T;
        }
    }
}


