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
            return; // ½Ã°£¸Ø­ŸÀ»¶§ ÀÔ·Âµµ ¹ŞÁö ¾Ê±â

        timer += Time.deltaTime;
        _level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / 10f), _spawnData.Length -1); // float¸¦ int·Î ¹Ù²ãÁÖ±âÀ§ÇØ ¼Ò¼öÁ¡ ¾Æ·¡´Â ¹ö¸®´Â ÇÔ¼öÃß°¡

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
        // GetComponentsInChildrenÇÒ¶§ ÀÚ±âÀÚ½Åµµ Æ÷ÇÔÀÌ±â¿¡ ÀÚ½ÄºÎÅÍ ³Ö±âÀ§ÇØ1ºÎÅÍ½ÃÀÛ
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