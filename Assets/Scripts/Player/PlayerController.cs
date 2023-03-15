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

        Vector3 lookDirection = Vector3.zero;

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
            lookDirection = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit hit))
            {
                lookDirection = hit.point- transform.position;
            }
            else
            {
                lookDirection = Vector3.zero;
            }
            lookDirection.y = 0f;
            transform.forward = lookDirection;
        }

        public Vector3 LookAtDirection()
        {
            return lookDirection.normalized;
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
            if(Input.GetMouseButtonDown(0))
                onShoot?.Invoke();
        }

        void OnActiveSkill()
        {
            if(Input.GetKeyDown(KeyCode.Q))
                onActiveSkill?.Invoke();
        }
    }
}
