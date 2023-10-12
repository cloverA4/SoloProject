using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoint;
    [SerializeField] SpawnData[] _spawnData;

    int _level;
    float timer;

    private void Awake()
    {
        _spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        _level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), _spawnData.Length -1); // float를 int로 바꿔주기위해 소수점 아래는 버리는 함수추가

        if (timer > _spawnData[_level].spawnTime) // 
        {
            Spawn();
            timer = 0f;
        }        
    }

    void Spawn()
    {
        GameObject Monster = PoolManager.instance.Get(0);
        Monster.transform.position = _spawnPoint[Random.Range(1, _spawnPoint.Length)].position;
        // GetComponentsInChildren할때 자기자신도 포함이기에 자식부터 넣기위해1부터시작
        Monster.GetComponent<Monster>().Init(_spawnData[_level]);
    }

}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public float health;
    public float speed;
}