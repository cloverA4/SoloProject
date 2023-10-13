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

    //������
    private Vector2 _inputVec;
    private Rigidbody2D _rigid;
    [SerializeField]private float _speed;
    private SpriteRenderer _spriter;
    private Animator _playerAni;
    private Scanner _scanner;
    [SerializeField] Equip[] _equipWeapon; // 

    //�̱���
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
        _scanner = GetComponent<Scanner>();
        _equipWeapon = GetComponentsInChildren<Equip>(true); // ���ڰ��� true�� ������ Ȱ��ȭ�� �ȵ� ������Ʈ�� GetComponent�� �Ҽ��ִ�
    }

    void Update()
    {
        if (!GameManager.Instance.IsLive)
            return; // �ð��ح����� �Էµ� ���� �ʱ�
        _inputVec.x = Input.GetAxisRaw("Horizontal");
        _inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return; // �ð��ح����� �Էµ� ���� �ʱ�
        Vector2 nextVec = _inputVec.normalized * _speed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return; // �ð��ح����� �Էµ� ���� �ʱ�

        _playerAni.SetFloat("Speed", _inputVec.magnitude);

        if (_inputVec.x != 0)
        {
            _spriter.flipX = _inputVec.x < 0;   
        }
    }
}
