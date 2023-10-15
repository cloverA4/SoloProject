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
        if (!GameManager.Instance.IsLive)
            return; // �ð��ح����� �Էµ� ���� �ʱ�

        timer += Time.deltaTime;
        _level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / 10f), _spawnData.Length -1); // float�� int�� �ٲ��ֱ����� �Ҽ��� �Ʒ��� ������ �Լ��߰�

        if (timer > _spawnData[_level].spawnTime) // 
        {
            timer = 0f;
            Spawn();
        }        
    }

    void Spawn()
    {
        GameObject Monster = GameManager.Instance.PoolManager.Get(0);
        Monster.transform.position = _spawnPoint[Random.Range(1, _spawnPoint.Length)].position;
        // GetComponentsInChildren�Ҷ� �ڱ��ڽŵ� �����̱⿡ �ڽĺ��� �ֱ�����1���ͽ���
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