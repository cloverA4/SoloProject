using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] PlayerController _player;
    [SerializeField] PoolManager _poolManager;

    float _gameTime;
    float _maxGameTime = 2 * 10;

    //플레이어 정보들
    float _playerHealth;
    float _playerMaxHealth = 10;
    int _playerlevel = 0;
    int _kill;
    int _exp;
    int[] _nextExp = { 1, 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 }; // 다음 경험치 필요 요구치

    //싱글톤
    public PlayerController Player
    {
        get { return _player; }
        set { _player = value; }
    }
    public PoolManager PoolManager
    {
        get { return _poolManager; }
        set { _poolManager = value; }
    }
    public float gameTime
    {
        get { return _gameTime; }
        set { _gameTime = value; }
    }
    public float maxGameTime
    {
        get { return _maxGameTime; }
        set { _maxGameTime = value; }
    }
    public float playerHealth
    {
        get { return _playerHealth; }
        set { _playerHealth = value; }
    }
    public float playerMaxHealth
    {
        get { return _playerMaxHealth = 10; }
        set { _playerMaxHealth = value; }
    }
    public int playerlevel
    {
        get { return _playerlevel; }
        set { _playerlevel = value; }
    }
    public int kill
    {
        get { return _kill; }
        set { _kill = value; }
    }
    public int exp
    {
        get { return _exp; }
        set { _exp = value; }
    }
    public int[] nextExp
    {
        get { return _nextExp; }
        set { _nextExp = value; }
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        _gameTime += Time.deltaTime;

        if (_gameTime > _maxGameTime)
        {
            _maxGameTime = _maxGameTime;
        }
    }

}
