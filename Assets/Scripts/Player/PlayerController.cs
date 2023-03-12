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
            Vector3 tempdirection = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit hit))
            {
                //float angle = Mathf.Atan2(hit.point.z,hit.point.x) * Mathf.Rad2Deg - 90f;
                 tempdirection = hit.point- transform.position;
            }
            else
            {
                tempdirection = Vector3.zero;
            }
            tempdirection.y = 0f;
            transform.forward = tempdirection;
        }

        private void OnDrawGizmos()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Gizmos.DrawRay(ray);
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
