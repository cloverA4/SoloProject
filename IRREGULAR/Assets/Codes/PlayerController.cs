using UnityEngine;

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

    //º¯¼öµé
    private Vector2 _inputVec;
    private Rigidbody2D _rigid;
    [SerializeField]private float _speed;
    private SpriteRenderer _spriter;
    private Animator _playerAni;
  
    //½Ì±ÛÅæ
    public Vector2 InputVec 
    {
        get { return _inputVec; }
        set { _inputVec = value; }
    }
   

    void Awake()
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
        _rigid = GetComponent<Rigidbody2D>();
        _spriter = GetComponent<SpriteRenderer>();
        _playerAni = GetComponent<Animator>();
    }

    void Update()
    {
        _inputVec.x = Input.GetAxisRaw("Horizontal");
        _inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = _inputVec.normalized * _speed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        _playerAni.SetFloat("Speed", _inputVec.magnitude);

        if (_inputVec.x != 0)
        {
            _spriter.flipX = _inputVec.x < 0;   
        }
    }
}
