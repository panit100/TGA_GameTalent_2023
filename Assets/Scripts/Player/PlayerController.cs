using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CCB.Utility;

namespace CCB.Player
{
    public class PlayerController : MonoBehaviour
    {
        public Action<Vector3> onMove;
        public Action<bool> onPressMove;
        public Action onDash;
        public Action onShoot;
        public Action onActiveSkill;
        public Action<float> onSwapSkill;

        Vector3 lookDirection = Vector3.zero;
        Vector3 moveDirection = Vector3.zero;

        void Start() 
        {
            SetUpInputAction();
        }

        void SetUpInputAction()
        {
            InputSystemManager.Instance.onMove += OnMove; 
            InputSystemManager.Instance.onPressMove += OnPressMove;    
            InputSystemManager.Instance.onLook += LookAtMouse;
            InputSystemManager.Instance.onShoot += OnShoot;  
            InputSystemManager.Instance.onDash += OnDash;  
            InputSystemManager.Instance.onActiveSkill += OnActiveSkill;
            InputSystemManager.Instance.onSwapSkill += OnSwapSkill;
            // InputSystemManager.Instance.onInteract += LookAtMouse;   
            // InputSystemManager.Instance.onReload += LookAtMouse;
            // InputSystemManager.Instance.onChangeBullet += LookAtMouse;
        }

        void RemoveInputAction()
        {
            InputSystemManager.Instance.onMove -= OnMove; 
            InputSystemManager.Instance.onPressMove -= OnPressMove;    
            InputSystemManager.Instance.onLook -= LookAtMouse;
            InputSystemManager.Instance.onShoot -= OnShoot;  
            InputSystemManager.Instance.onDash -= OnDash;  
            InputSystemManager.Instance.onActiveSkill -= OnActiveSkill;
            InputSystemManager.Instance.onSwapSkill -= OnSwapSkill;
            // InputSystemManager.Instance.onInteract += LookAtMouse;   
            // InputSystemManager.Instance.onReload += LookAtMouse;
            // InputSystemManager.Instance.onChangeBullet += LookAtMouse;
        }

        void LookAtMouse(Vector2 mousePosition)
        {
            lookDirection = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePosition.x,mousePosition.y,0));
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

        void OnMove(Vector2 direction)
        {
            onMove?.Invoke(new Vector3(direction.x,0,direction.y));
        }

        void OnPressMove(bool isPressed)
        {
            onPressMove?.Invoke(isPressed);
        }

        void OnDash()
        {
            onDash?.Invoke();
        }

        void OnShoot()
        {
            onShoot?.Invoke();
        }

        void OnActiveSkill()
        {
            onActiveSkill?.Invoke();
        }

        void OnSwapSkill(float value)
        {
            onSwapSkill?.Invoke(value);
        }

        private void OnDrawGizmos()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Gizmos.DrawRay(ray);
        }

        void OnDestroy()
        {
            RemoveInputAction();
        }
    }
}
