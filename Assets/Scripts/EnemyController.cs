using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] float HealthPoint;
    [SerializeField] float speed;

    [SerializeField] float triggerRadius;
    [SerializeField] LayerMask playerLayer;


    float currentHP;

    Vector3 direction;
    [SerializeField] PlayerController playerController;

    Rigidbody rigidbody;
    NavMeshAgent navMeshAgent;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        currentHP = HealthPoint;
    }

    void FixedUpdate()
    {
        SetAIDistination();
    }
    
    void SetAIDistination()
    {
        IsPlayerInTriggerArea();
    }

    bool IsPlayerInTriggerArea()
    {
        var overlapCollider = Physics.OverlapSphere(transform.position, triggerRadius,playerLayer);

        foreach(var collider in overlapCollider)
        {
            if (collider.TryGetComponent<PlayerController>(out var player))
            {
                navMeshAgent.speed = speed;
                navMeshAgent.SetDestination(playerController.transform.position);
                return false;
            }
        }

        navMeshAgent.speed = 0;
        return true;
        
    }

    void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(transform.position,triggerRadius);
    }
}
