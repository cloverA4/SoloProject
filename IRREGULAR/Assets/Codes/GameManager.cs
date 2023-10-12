using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] PlayerController _player;
    [SerializeField] PoolManager _poolManager;
    [SerializeField] LevelUp _uiLevelUp;

    //���� �ð�
    bool _isLive = true; //�ϴ� true
    float _gameTime;
    float _maxGameTime = 2 * 10;

    //�÷��̾� ������
    float _playerHealth;
    float _playerMaxHealth = 10;
    int _playerlevel = 0;
    int _kill;
    int _exp;
    int[] _nextExp = { 1, 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 }; // ���� ����ġ �ʿ� �䱸ġ

    //�̱���
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
    public bool IsLive
    {
        get { return _isLive; }
        set { _isLive = value; }
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

    private void Start()
    {
        _playerHealth = _playerMaxHealth;

        _uiLevelUp.Select(0);
    }

    void Update()
    {
        if (!_isLive)
            return;

        _gameTime += Time.deltaTime;

        if (_gameTime > _maxGameTime){
            _gameTime = _maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if (_exp == _nextExp[Mathf.Min(playerlevel,nextExp.Length-1)]){ //���������� ������ �����̵����� ���ؼ� �����Ÿ� �߰� ����ġ��
            _playerlevel++;
            _exp = 0;
            _uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        _isLive = false;
        Time.timeScale = 0; //����Ƽ �ð� �ӵ� 0
    }

    public void Resume()
    {
        _isLive = true;
        Time.timeScale = 1;
    }
}
