using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCB.Enemy;

namespace CCB.Gameplay
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] BaseEnemy EnemyPrefab;

        BaseEnemy enemy;

        void Start() 
        {
            SpawnEnemy();
        }

        void SpawnEnemy()
        {
            StartCoroutine(CreateEnemy());
        }

        IEnumerator CreateEnemy()
        {
            Vector3 ramdomPos = new Vector3(transform.position.x + Random.Range(0,1),1.22895f,transform.position.z+ Random.Range(0,1));
            enemy = Instantiate(EnemyPrefab,ramdomPos,Quaternion.identity);
            enemy.onDestroy += () => enemy = null;

            yield return new WaitUntil(() => enemy == null);
            StartCoroutine(CreateEnemy());
        }
    }
}
