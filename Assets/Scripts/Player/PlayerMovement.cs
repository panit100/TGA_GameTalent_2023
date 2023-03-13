using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB.Gameplay;

namespace CCB.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] TimeState timeState = TimeState.Normal;

        [SerializeField] float speed;

        Rigidbody rigidbody;

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
            rigidbody.velocity = direction * speed * TimeManager.Instance.GetTime(timeState);
        }

        void Dash()
        {
            Debug.Log("Dash!!!");
        }

        public void OnFastForwardActivated(float duration) 
        {
            timeState = TimeState.Accelerate;
            StartCoroutine(OnTimeToState(duration, TimeState.Normal));
        }

        IEnumerator OnTimeToState(float time, TimeState newTimeState)
        {
            yield return new WaitForSeconds(time);

            timeState = newTimeState;
        }

        void OnDestroy() 
        {
            PlayerManager.Instance.PlayerController.onMove -= Move;
            PlayerManager.Instance.PlayerController.onDash -= Dash;
        }
    }

}
