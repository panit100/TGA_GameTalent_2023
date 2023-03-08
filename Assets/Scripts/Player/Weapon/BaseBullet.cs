using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCB.Player
{
    public enum BULLETTYPE
    {
        
    }

    public abstract class BaseBullet : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float damage;
        [SerializeField] BULLETTYPE bulletType;
        
        public float Speed {get {return speed;}}
        public float Damage {get {return damage;}}
        public BULLETTYPE BulletType {get {return bulletType;}}
        public abstract void BulletSkill();
    }

}

