using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] PlayerController _player;
    [SerializeField] PoolManager _poolManager;
    [SerializeField] LevelUp _uiLevelUp;
    [SerializeField] GameResult _uiResult; // 게임결과 Ui 오브젝트를 저장할 변수 선언 및 초기화
    [SerializeField] GameObject _enemyCleaner; //게임 승리할 때 적을 정리하는 클리너 변수
    [SerializeField] Transform _uiJoy;
    [SerializeField] GameObject _gameExitUi;
    [SerializeField] GameObject _OptionUiBtn;
    [SerializeField] GameObject _OptionUi;

    //게임 시간
    bool _isLive; 
    float _gameTime;
    float _maxGameTime = 180;

    //플레이어 정보들
    int _playerId;
    float _playerHealth;
    float _playerMaxHealth = 10;
    int _playerlevel = 0;
    int _kill;
    int _exp;
    int[] _nextExp = { 1, 3, 7, 10, 15, 25, 30, 35, 40, 45, 50, 55 }; // 다음 경험치 필요 요구치

    //싱글톤
    public GameObject OptionUiBtn
    {
        get { return _OptionUiBtn; }
        set { _OptionUiBtn = value; }
    }
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
    public int PlayerId
    {
        get { return _playerId; }
        set { _playerId = value; }  
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
        Instance = this;
        Application.targetFrameRate = 60;
    }

    public void GameStart(int id)
    {
        _playerId = id;
        _playerHealth = _playerMaxHealth;

        _player.gameObject.SetActive(true);
        _uiLevelUp.Select(_playerId % 2); // 캐릭터선택시 아이디 확인
        _gameExitUi.SetActive(false);

        Resume();
        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
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

        if (_gameTime > _maxGameTime-0.1f)
        {
            _enemyCleaner.SetActive(true);
        }

        
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

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameClear()
    {
        
        StartCoroutine(GameClearDelay());
    }

    IEnumerator GameClearDelay()
    {
        _isLive = false;
        _enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        _uiResult.gameObject.SetActive(true);
        _uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }


    public void GetExp(int expNum)
    {
        if (!_isLive)
            return;

        _exp += expNum;

        if (_exp == _nextExp[Mathf.Min(_playerlevel,_nextExp.Length-1)]){ //레벨오르면 오류뜸 만랩이됫을때 비교해서 낮은거만 뜨게 경험치량
            _playerlevel++;
            _exp = 0;
            _uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        _isLive = false;
        Time.timeScale = 0; //유니티 시간 속도 0
        _uiJoy.localScale = Vector3.zero;
    }

    public void Resume()
    {
        _isLive = true;
        Time.timeScale = 1;
        _uiJoy.localScale = Vector3.one;
    }

    public void GameExit()
    {
        Application.Quit(); 
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        _OptionUi.SetActive(false);
        Time.timeScale = 1;
    }

    public void OptUI()
    {
        _OptionUi.SetActive(true);
        Time.timeScale = 0;
    }
}
