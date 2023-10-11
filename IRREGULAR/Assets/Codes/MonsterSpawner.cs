using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoint;

    float timer;

    private void Awake()
    {
        _spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f)
        {
            Spawn();
            timer = 0f;
        }        
    }

    void Spawn()
    {
        GameObject enemy = PoolManager.instance.Get(Random.Range(0,2));
        enemy.transform.position = _spawnPoint[Random.Range(1, _spawnPoint.Length)].position; 
        // GetComponentsInChildren할때 자기자신도 포함이기에 자식부터 넣기위해1부터시작
    }

}
