using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB.Gameplay;

namespace CCB.Player
{
    public class PlayerMovement : MonoBehaviour
    {
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
            rigidbody.velocity = direction * speed * (TimeManager.Instance.GetTimeState() == TimeState.Slow ? TimeManager.Instance.GetTime() : 1);
        }

        void Dash()
        {
            Debug.Log("Dash!!!");
        }

        void OnDestroy() 
        {
            PlayerManager.Instance.PlayerController.onMove -= Move;
            PlayerManager.Instance.PlayerController.onDash -= Dash;
        }
    }

}
