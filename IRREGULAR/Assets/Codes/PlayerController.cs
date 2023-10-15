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
    private Scanner _scanner;
    [SerializeField] Equip[] _equipWeapon; // 
    [SerializeField] RuntimeAnimatorController[] _animCon;

    //½Ì±ÛÅæ
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
        _equipWeapon = GetComponentsInChildren<Equip>(true); // ÀÎÀÚ°ª¿¡ true¸¦ ³ÖÀ¸¸é È°¼ºÈ­°¡ ¾ÈµÈ ¿ÀºêÁ§Æ®µµ GetComponent¸¦ ÇÒ¼öÀÖ´Ù
    }

    private void OnEnable()
    {
        _speed *= CharacterStat.Speed;
        _playerAni.runtimeAnimatorController = _animCon[GameManager.Instance.PlayerId];
    }

    void Update()
    {
        if (!GameManager.Instance.IsLive)
            return; // ½Ã°£¸Ø­ŸÀ»¶§ ÀÔ·Âµµ ¹ÞÁö ¾Ê±â
        _inputVec.x = Input.GetAxisRaw("Horizontal");
        _inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return; // ½Ã°£¸Ø­ŸÀ»¶§ ÀÔ·Âµµ ¹ÞÁö ¾Ê±â
        Vector2 nextVec = _inputVec.normalized * _speed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return; // ½Ã°£¸Ø­ŸÀ»¶§ ÀÔ·Âµµ ¹ÞÁö ¾Ê±â

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
                //GetChild ÁÖ¾îÁø ÀÎµ¦½ºÀÇ ÀÚ½Ä ¿ÀºêÁ§Æ®¸¦ ¹ÝÈ¯ÇÏ´Â ÇÔ¼ö
            }

            _playerAni.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        }
    }
}
