using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB;
using CCB.Player;
using UnityEngine.AI;
using CCB.Gameplay;
using System;

namespace CCB.Enemy
{
    enum EnemyType
    {
        Melee,
        Range,
    }

    public class BaseEnemy : MonoBehaviour, IDamageable
    {
        [SerializeField] TimeState timeState = TimeState.Normal;

        [Header("EnemyStatus")]
        [SerializeField] EnemyType enemyType;
        [SerializeField] float healthPoint;
        [SerializeField] float damage;
        [SerializeField] float speed;

        [SerializeField] float attackRange;
        [SerializeField] float visionRange;

        [Header("EnemySkill")]
        [SerializeField] EnemySkillConfig enemySkill;

        //TODO: Implement state mechine later (AISTATE)
        NavMeshAgent navMeshAgent;

        public Action onDestroy;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();

            navMeshAgent.stoppingDistance = attackRange;
        }

        void FixedUpdate()
        {
            SetAIDistination();
        }

        void SetAIDistination()
        {
            if(IsPlayerInTriggerArea())
                navMeshAgent.SetDestination(PlayerManager.Instance.transform.position);
            else
                navMeshAgent.SetDestination(this.transform.position);
        }

        bool IsPlayerInTriggerArea()
        {
            if(timeState == TimeState.Stop)
            {
                navMeshAgent.speed = 0;
                return false;
            }

            var overlapCollider = Physics.OverlapSphere(transform.position, visionRange);

            foreach(var collider in overlapCollider)
            {
                if (collider.TryGetComponent<PlayerManager>(out var player))
                {
                    navMeshAgent.speed = speed * TimeManager.Instance.GetTime(timeState);
                    return true;
                }
            }

            navMeshAgent.speed = 0;
            return false;
            
        }

        public void ProcessDamage(float damage)
        {
            if(timeState == TimeState.Stop)
                return;

            healthPoint -= damage;
            if(healthPoint <= 0)
                OnDie();
        }

        void OnDie()
        {
            Destroy(this.gameObject);
        }

        void OnDestroy() 
        {
            onDestroy?.Invoke();    
        }

        void OnDrawGizmos() 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,visionRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,attackRange);
        }

        public void OnTimeStop(float timeTostop)
        {
            timeState = TimeState.Stop;
            StartCoroutine(OnTimeUnStop(timeTostop));
        }

        IEnumerator OnTimeUnStop(float time)
        {
            yield return new WaitForSeconds(time);
            
            timeState = TimeState.Normal;
        }
    }
}
