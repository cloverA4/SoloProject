using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoint;
    [SerializeField] SpawnData[] _spawnData;
    float _levelTime;

    int _level;
    float timer;

    private void Awake()
    {
        _spawnPoint = GetComponentsInChildren<Transform>();
        _levelTime = GameManager.Instance.maxGameTime / _spawnData.Length;
    }

    void Update()
    {
        if (!GameManager.Instance.IsLive)
            return; // Ω√∞£∏ÿ≠ü¿ª∂ß ¿‘∑¬µµ πﬁ¡ˆ æ ±‚

        timer += Time.deltaTime;
        _level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / _levelTime), _spawnData.Length -1); // float∏¶ int∑Œ πŸ≤„¡÷±‚¿ß«ÿ º“ºˆ¡° æ∆∑°¥¬ πˆ∏Æ¥¬ «‘ºˆ√ﬂ∞°

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
        // GetComponentsInChildren«“∂ß ¿⁄±‚¿⁄Ω≈µµ ∆˜«‘¿Ã±‚ø° ¿⁄Ωƒ∫Œ≈Õ ≥÷±‚¿ß«ÿ1∫Œ≈ÕΩ√¿€
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