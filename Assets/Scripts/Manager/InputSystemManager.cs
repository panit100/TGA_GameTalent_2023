using System;
using System.Collections;
using System.Collections.Generic;
using CCB.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace CCB.Utility
{
    public class InputSystemManager : Singleton<InputSystemManager>
    {
        const string PLAYER_ACTIONMAP = "Player";
        const string UI_ACTIONMAP = "UI";
        
        [SerializeField] InputActionAsset playerInputAction;

    #region UnityAction
        public UnityAction<Vector2> onMove;
        public UnityAction<bool> onPressMove;
        public UnityAction<Vector2> onLook;
        public UnityAction onShoot;
        public UnityAction onDash;
        public UnityAction onActiveSkill;
        public UnityAction onInteract;
        public UnityAction onReload;
        public UnityAction onPause;
        public UnityAction onChangeBullet;
    #endregion

        InputActionMap playerControlMap;
        InputActionMap uiControlMap;

        bool globalInputEnable = false;
        bool playerControlEnable = true;
        bool uiControlEnable = true;

        protected override void InitAfterAwake()
        {
            playerControlMap = playerInputAction.FindActionMap(PLAYER_ACTIONMAP);
            uiControlMap = playerInputAction.FindActionMap(UI_ACTIONMAP);
        }

        void Start()
        {
            ToggleGlobalInput(true);
        }

        private void UpdateInputState()
        {
            if(globalInputEnable && playerControlEnable) playerControlMap.Enable();
            else playerControlMap.Disable();

            if(globalInputEnable && uiControlEnable) uiControlMap.Enable();
            else uiControlMap.Disable();
        }

        private void ToggleGlobalInput(bool toggle)
        {
            globalInputEnable = toggle;
            UpdateInputState();
        }

        public void TogglePlayerControl(bool toggle)
        {
            playerControlEnable = toggle;
            UpdateInputState();
        }

        public void ToggleUIControl(bool toggle)
        {
            uiControlEnable = toggle;
            UpdateInputState();
        }

    #region  ControlFunction
        #region Player Function
            void OnMove(InputValue value)
            {
                if(value.Get<Vector2>() != Vector2.zero)
                    onMove?.Invoke(value.Get<Vector2>());
            }

            
            void OnPressMove(InputValue value)
            {
                if(value.isPressed)
                    onPressMove?.Invoke(true);
                else
                    onPressMove?.Invoke(false);
            }

            void OnLook(InputValue value)
            {
                if(value.Get<Vector2>() != Vector2.zero)
                    onLook?.Invoke(value.Get<Vector2>());
            }

            void OnShoot(InputValue value)
            {
                if(value.isPressed)
                    onShoot?.Invoke();
            }

            void OnDash(InputValue value)
            {
                if(value.isPressed)
                    onDash?.Invoke();
            }

            void OnActiveSkill(InputValue value)
            {
                if(value.isPressed)
                    onActiveSkill?.Invoke();
            }

            void OnInteract(InputValue value)
            {
                if(value.isPressed)
                    onInteract?.Invoke();
            }

            void OnReload(InputValue value)
            {
                if(value.isPressed)
                    onReload?.Invoke();
            }

            void OnPause(InputValue value)
            {
                if(value.isPressed)
                    onPause?.Invoke();
            }

            void OnChangeBullet(InputValue value)
            {
                if(value.isPressed)
                    onChangeBullet?.Invoke();
            }

        #endregion

        #region  UI Function

        #endregion
    #endregion
    }
}
