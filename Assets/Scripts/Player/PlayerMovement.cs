using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB.Gameplay;

namespace CCB.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public TimeState timeState = TimeState.Normal;

        [SerializeField] float speed;

        Rigidbody rigidbody;

        private float tempBoostSpeed = 1f;

        void Awake() 
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        void Start()
        {
            PlayerManager.Instance.PlayerController.onMove += Move;
            PlayerManager.Instance.PlayerController.onDash += Dash;
        }

        void Move(Vector3 direction)
        {
            rigidbody.velocity = direction * GetPlayerCurrentspeed();
        }

        void Dash()
        {
            Debug.Log("Dash!!!");
        }

        public float GetPlayerCurrentspeed()
        {
            return speed * TimeManager.Instance.GetTime(timeState) * tempBoostSpeed;
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
               // Debug.Log($"$ Clock UP !! x{GetPlayerCurrentspeed()} speed {t}sec ");
                PlayerManager.Instance.PlayerTimeDependent.BoostComponent(GetPlayerCurrentspeed());
                t += Time.deltaTime;
                yield return null;
            }
            tempBoostSpeed = 1;
            PlayerManager.Instance.PlayerTimeDependent.BoostComponent(tempBoostSpeed);
            timeState = newTimeState;
            yield return null;
        }

        void OnDestroy() 
        {
            PlayerManager.Instance.PlayerController.onMove -= Move;
            PlayerManager.Instance.PlayerController.onDash -= Dash;
        }
    }

}
