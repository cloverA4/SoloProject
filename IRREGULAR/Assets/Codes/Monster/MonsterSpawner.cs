using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoint;
    [SerializeField] SpawnData[] _spawnData;
    float _levelTime;

    int _level;
    float _timer;
    float _eliteTimer;
    float _initEliteTimer;

    private void Awake()
    {
        _spawnPoint = GetComponentsInChildren<Transform>();
        _levelTime = (GameManager.Instance.maxGameTime - 60) / _spawnData.Length;
        _eliteTimer = GameManager.Instance.maxGameTime - 60;
        
    }

    void Update()
    {
        if (!GameManager.Instance.IsLive)
            return; // �ð��ح����� �Էµ� ���� �ʱ�

        _timer += Time.deltaTime;
        _initEliteTimer += Time.deltaTime;

        _level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / _levelTime), _spawnData.Length -1); // float�� int�� �ٲ��ֱ����� �Ҽ��� �Ʒ��� ������ �Լ��߰�

        if (_timer > _spawnData[_level].spawnTime) // 
        {
            _timer = 0f;
            if (_eliteTimer > GameManager.Instance.gameTime)
                Spawn();
        }
        if (_eliteTimer < _initEliteTimer) //
        {
            EliteMonsterSpawn();
            _initEliteTimer = 0f;
        }
    }

    void Spawn()
    {
        GameObject Monster = GameManager.Instance.PoolManager.Get(0);
        Monster.transform.position = _spawnPoint[Random.Range(1, _spawnPoint.Length)].position;
        // GetComponentsInChildren�Ҷ� �ڱ��ڽŵ� �����̱⿡ �ڽĺ��� �ֱ�����1���ͽ���
        Monster.GetComponent<Monster>().Init(_spawnData[_level]);
    }

    void EliteMonsterSpawn()
    {
        GameObject Monster = GameManager.Instance.PoolManager.Get(5);
        Monster.transform.position = _spawnPoint[Random.Range(1, _spawnPoint.Length)].position;
        Monster.GetComponent<Monster>().EliteInit();
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