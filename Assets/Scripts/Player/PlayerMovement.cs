using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using CCB.Gameplay;
using CCB.Utility;
using Vector3 = UnityEngine.Vector3;

namespace CCB.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] TimeState timeState = TimeState.Normal;
        public TimeState TimeState => timeState;


        [SerializeField] float speed;
        [SerializeField] float dashSpeed;
        [SerializeField] float dashDuration;
        [SerializeField] float dashCooldown;

        float currentSpeed;

        Rigidbody rigidbody;

        private float tempBoostSpeed = 1f;
        bool isMove;
        bool isDashing;
        Vector3 direction;

        void Awake() 
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        void Start()
        {
            SetUpInputAction();
        
            currentSpeed = speed;
        }

        void SetUpInputAction()
        {
            PlayerManager.Instance.PlayerController.onMove += OnMove;
            PlayerManager.Instance.PlayerController.onPressMove += OnPressMove;
            PlayerManager.Instance.PlayerController.onDash += Dash;
        }

        void RemoveInputAction()
        {
            PlayerManager.Instance.PlayerController.onMove -= OnMove;
            PlayerManager.Instance.PlayerController.onPressMove -= OnPressMove;
            PlayerManager.Instance.PlayerController.onDash -= Dash;
        }

        void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            if (!isMove)
            {
                direction = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                rigidbody.velocity = Vector3.zero;
            }
            else
            {
                rigidbody.velocity = direction * GetPlayerCurrentspeed();
            }
        }

        void OnMove(Vector3 value)
        {
            direction = value;
        }

        void OnPressMove(bool isPressed)
        {
            isMove = isPressed;
            PlayerManager.Instance.playerAnimator.SetBool("isRun",isPressed);
        }

        void Dash()
        {
            if(isDashing || !isMove)
                return;

            currentSpeed = dashSpeed;
            isDashing = true;
            PlayerManager.Instance.playerAnimator.SetBool("isDash",true);
            InputSystemManager.Instance.TogglePlayerControl(false);
            StartCoroutine(Dashing(dashDuration,dashCooldown));
        }

        public float GetPlayerCurrentspeed()
        {
            return currentSpeed * TimeManager.Instance.GetTime(timeState) * tempBoostSpeed;
        }
     
        public void OnFastForwardActivated(float duration) 
        {
            timeState = TimeState.Accelerate;
            StartCoroutine(OnTimeToState(duration, TimeState.Normal));
        }

        public void OnAccelTimeActivated(float duration,float multiplier)
        {
            tempBoostSpeed = multiplier;
            timeState = TimeState.Accelerate;
            StartCoroutine(SpeedBurst(duration, TimeState.Normal));
        }

        IEnumerator OnTimeToState(float time, TimeState newTimeState)
        {
            yield return new WaitForSeconds(time);

            timeState = newTimeState;
        }

        IEnumerator SpeedBurst(float duration,TimeState newTimeState)
        {
            var t = 0f;
            
            while(t<duration)
            {
                PlayerManager.Instance.PlayerTimeDependent.BoostComponent(GetPlayerCurrentspeed());
                PostProManager.Instance.TransitionIn(duration);
                t += Time.deltaTime;
                yield return null;
            }
            PostProManager.Instance.TransitionOut(duration);

            tempBoostSpeed = 1;
            PlayerManager.Instance.PlayerTimeDependent.BoostComponent(tempBoostSpeed);
            timeState = newTimeState;
            yield return null;
        }

        IEnumerator Dashing(float duration,float dashCooldown)
        {
            yield return new WaitForSeconds(duration);
            currentSpeed = speed;
            InputSystemManager.Instance.TogglePlayerControl(true);

            yield return new WaitForSeconds(dashCooldown);
            isDashing = false;
            PlayerManager.Instance.playerAnimator.SetBool("isDash",false);
        }

        void OnDestroy() 
        {
            RemoveInputAction();
        }
    }

}
