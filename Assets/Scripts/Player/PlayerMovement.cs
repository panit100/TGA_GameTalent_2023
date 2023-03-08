using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            rigidbody.velocity = direction * speed;
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
