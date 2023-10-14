using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] PlayerController _player;
    [SerializeField] PoolManager _poolManager;
    [SerializeField] LevelUp _uiLevelUp;
    [SerializeField] GameResult _uiResult; // ���Ӱ�� Ui ������Ʈ�� ������ ���� ���� �� �ʱ�ȭ
    [SerializeField] GameObject _enemyCleaner; //���� �¸��� �� ���� �����ϴ� Ŭ���� ����

    //���� �ð�
    bool _isLive; //�ϴ� true
    float _gameTime;
    float _maxGameTime = 10;

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
        get { return _playerMaxHealth; }
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

    public void GameStart()
    {
        _playerHealth = _playerMaxHealth;
        _uiLevelUp.Select(0);
        Resume();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverDelay());
    }

    IEnumerator GameOverDelay()
    {
        _isLive = false;

        yield return new WaitForSeconds(0.5f);

        _uiResult.gameObject.SetActive(true);
        _uiResult.Lose();
        Stop();
    }

    public void GameClear()
    {
        StartCoroutine(GameClearDelay());
    }

    IEnumerator GameClearDelay()
    {
        _enemyCleaner.SetActive(true);
        _isLive = false;

        yield return new WaitForSeconds(0.5f);

        _uiResult.gameObject.SetActive(true);
        _uiResult.Win();
        Stop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (!_isLive)
            return;

        _gameTime += Time.deltaTime;

        if (_gameTime > _maxGameTime){
            _gameTime = _maxGameTime;
            GameClear();
        }
    }

    public void GetExp(int expNum)
    {
        if (!_isLive)
            return;

        _exp += expNum;

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
