using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB;
using CCB.Player;
using UnityEngine.AI;
using CCB.Gameplay;

namespace CCB.Enemy
{
    enum EnemyType
    {
        Melee,
        Range,
    }

    public class BaseEnemy : MonoBehaviour, IDamageable
    {
        [SerializeField] EnemyType enemyType;
        [SerializeField] float healthPoint;
        [SerializeField] float damage;
        [SerializeField] float speed;

        [SerializeField] float attackRange;
        [SerializeField] float visionRange;

        //TODO: Implement state mechine later (AISTATE)

        [SerializeField] EnemySkillConfig enemySkill;

        NavMeshAgent navMeshAgent;
        

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
        }

        bool IsPlayerInTriggerArea()
        {
            var overlapCollider = Physics.OverlapSphere(transform.position, visionRange);

            foreach(var collider in overlapCollider)
            {
                if (collider.TryGetComponent<PlayerManager>(out var player))
                {
                    navMeshAgent.speed = speed * (TimeManager.Instance.GetTimeState() == TimeState.Slow ? TimeManager.Instance.GetTime() : 1);
                    return true;
                }
            }

            navMeshAgent.speed = 0;
            return false;
            
        }

        public void ProcessDamage(float damage)
        {
            healthPoint -= damage;
            if(healthPoint <= 0)
                OnDie();
        }

        void OnDie()
        {

        }

        void OnDrawGizmos() 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,visionRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,attackRange);
        }
    }
}
