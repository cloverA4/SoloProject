using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    float _gameTime;
    float _maxGameTime = 2 * 10;

    //½Ì±ÛÅæ
    public float GameTime
    {
        get { return _gameTime; }
        set { _gameTime = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
