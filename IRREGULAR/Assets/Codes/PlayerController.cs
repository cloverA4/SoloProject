using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    public static PlayerController Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    //변수들
    private Vector2 _inputVec;
    private Rigidbody2D _rigid;
    [SerializeField]private float _speed;
    private SpriteRenderer _spriter;
    private Animator _playerAni;
    private Scanner _scanner;
    [SerializeField] Equip[] _equipWeapon; // 
    [SerializeField] RuntimeAnimatorController[] _animCon;

    //싱글톤
    public Vector2 InputVec 
    {
        get { return _inputVec; }
        set { _inputVec = value; }
    }
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public Scanner Scanner
    {
        get { return _scanner; }
        set { _scanner = value; }
    }
    public Equip[] EquipWeapon
    {
        get { return _equipWeapon; }
        set { _equipWeapon = value; }
    }


    void Awake()
    {
        instance = this;
        //if (instance == null)
        //{
        //    instance = this; 
        //    DontDestroyOnLoad(gameObject); 
        //}
        //else
        //{
        //    Destroy(this.gameObject);
        //}
        _rigid = GetComponent<Rigidbody2D>();
        _spriter = GetComponent<SpriteRenderer>();
        _playerAni = GetComponent<Animator>();
        _scanner = GetComponent<Scanner>();
        _equipWeapon = GetComponentsInChildren<Equip>(true); // 인자값에 true를 넣으면 활성화가 안된 오브젝트도 GetComponent를 할수있다
    }

    private void OnEnable()
    {
        _speed *= CharacterStat.Speed;
        _playerAni.runtimeAnimatorController = _animCon[GameManager.Instance.PlayerId];
    }

    void Update()
    {
        if (!GameManager.Instance.IsLive)
            return; // 시간멈췃을때 입력도 받지 않기
        //_inputVec.x = Input.GetAxisRaw("Horizontal");
        //_inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return; // 시간멈췃을때 입력도 받지 않기
        Vector2 nextVec = _inputVec * _speed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return; // 시간멈췃을때 입력도 받지 않기

        _playerAni.SetFloat("Speed", _inputVec.magnitude);

        if (_inputVec.x != 0)
        {
            _spriter.flipX = _inputVec.x < 0;   
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!GameManager.Instance.IsLive)
            return;

        GameManager.Instance.playerHealth -= Time.deltaTime * 10;

        if (GameManager.Instance.playerHealth < 0)
        {
            for (int index = 2; index < transform.childCount; index++){
                transform.GetChild(index).gameObject.SetActive(false);
                //GetChild 주어진 인덱스의 자식 오브젝트를 반환하는 함수
            }

            _playerAni.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        }
    }

    void OnMove(InputValue value)
    {
        _inputVec = value.Get<Vector2>();
    }
}
