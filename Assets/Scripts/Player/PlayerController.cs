using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CCB.Player
{
    public class PlayerController : MonoBehaviour
    {
        public Action<Vector3> onMove;
        public Action onDash;
        public Action onShoot;
        public Action onActiveSkill;

        void FixedUpdate()
        {
            LookAtMouse();
            
            OnMove();
            OnDash();
            OnShoot();
            OnActiveSkill();
        }

        void LookAtMouse()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit hit))
            {
                float angle = Mathf.Atan2(hit.point.z,hit.point.x) * Mathf.Rad2Deg - 90f;
                Debug.Log("angle " + angle);
                transform.rotation = new Quaternion(0,angle,0,0);
            }
        }

        void OnMove()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            onMove?.Invoke(new Vector3(horizontal,0,vertical));
        }

        void OnDash()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                onDash?.Invoke();
        }

        void OnShoot()
        {
            if(Input.GetMouseButton(0))
                onShoot?.Invoke();
        }

        void OnActiveSkill()
        {
            if(Input.GetKeyDown(KeyCode.Q))
                onActiveSkill?.Invoke();
        }
    }
}
