using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int _id;
    [SerializeField] int _prefebId;
    [SerializeField] float _damage;
    [SerializeField] int _count;
    [SerializeField] float _speed;

    float _timer;
    PlayerController _player;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    private void Awake()
    {
        _player = GameManager.Instance.Player;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsLive)
            return; // ½Ã°£¸Ø­ŸÀ»¶§ ÀÔ·Âµµ ¹ŞÁö ¾Ê±â

        switch (_id)
        {
            case 0:
                transform.Rotate(Vector3.back * _speed * Time.deltaTime);
                break;
            case 1:
                _timer += Time.deltaTime;

                if (_timer > _speed)
                {
                    _timer = 0;
                    Fire();
                }
                break;
        }
        if (Input.GetButtonDown("Jump")) //Å×½ºÆ®¿ë ·¹º§¾÷
        {
            LevelUp(10, 1);
        }
    }

    public void Init(ItemData data)
    {
        //±âº»¼¼ÆÃ
        name = "Weapon " + data.ItemId;
        transform.parent = _player.transform; // ºÎ¸ğ¸¦ ÇÃ·¹ÀÌ¾î·Î
        transform.localPosition = Vector3.zero; // ÇÃ·¹ÀÌ¾î Áö¿ªÀ§Ä¡ÀÇ ¿øÁ¡À¸·Î º¯°æ

        //¾ÆÀÌÅÛ ³»ºÎ °ªµé
        _id = data.ItemId;
        _damage = data.BaseDamage;
        _count = data.BaseCount;

        for (int index = 0; index < GameManager.Instance.PoolManager.Prefabs.Length; index++) {
            if (data.Projectile == GameManager.Instance.PoolManager.Prefabs[index])
            {
                _prefebId = index;
                break;
            }
        }

        switch (_id)
        {
            case 0:
                _speed = 150;
                Batch();
                break;
            case 1:
                _speed = 0.4f;
                break;
        }
        //Àåºñ Âø¿ë Àû¿ë
        Equip equip = _player.EquipWeapon[(int)data.Type];
        equip.Spriter.sprite = data.Equip;
        equip.gameObject.SetActive(true);

        _player.BroadcastMessage("ApplyPassive", SendMessageOptions.DontRequireReceiver);
        //Æ¯Á¤ ÇÔ¼ö È£ÃâÀ» ¸ğµç ÀÚ½Ä¿¡°Ô ¹æ¼ÛÇÏ´Â ÇÔ¼ö ´ÜÇÏ³ªÀÇ ¿ÀºêÁ§Æ®°¡¾Æ´Ï¶ó ÇÃ·¹ÀÌ¾î°¡°¡Áø ¸ğµç ÀÚ½Ä¿¡°Ô ApplyPassive ¸¦ ½ÇÇàÇÏ¶ó°í È£ÁÙÇÔ
        //¿Ö³ÄÇÏ¸é Àåºñ¸¦ ¾÷±×·¹ÀÌµå ÇÒ¶§¿¡µµ °ø°İ¼Óµµ °è»ê½ÄÀÌ »õ·Î µé¾î°¡¾ß ÇÏ±â ‹š¹®ÀÌ´Ù
    }

    void Batch()
    {
        for (int index = 0; index < _count; index++)
        {
            Transform pick; 

            if (index < transform.childCount) { 
                pick = transform.GetChild(index);
            }
            else{
                pick = GameManager.Instance.PoolManager.Get(_prefebId).transform;
                pick.parent = transform;
            }
            // ±âÁ¸ ¿ÀºêÁ§Æ® È°¿ëÇÏ°í ºÎÁ·ÇÑ°ÍÀº Ç®¸µ¿¡¼­ °¡Á®¿À±â

            pick.localPosition = Vector3.zero;
            pick.localRotation = Quaternion.identity;

            //ÇÑ¹ø»èÁ¦¸¦ ÇÏ°í ´Ù½Ã Ä«¿îÆ®¸¸Å­ »ı¼ºÇØ¼­ µ¹¸®±â
            Vector3 rotVec = Vector3.forward * 360 * index / _count;
            pick.Rotate(rotVec);
            pick.Translate(pick.up * 1.5f, Space.World);
            pick.GetComponent<Pick>().Init(_damage, -1, Vector3.zero); // -1 ´Â ¹«Á¶²« °üÅëÇÏ°Ô ¸¸µé¾îÁÖ´Â °ÍÀÌ´Ù.
        }
    }

    public void LevelUp(float damage, int count)
    {
        this._damage = damage;
        this._count += count;

        if(_id == 0)
            Batch();

        _player.BroadcastMessage("ApplyPassive",SendMessageOptions.DontRequireReceiver);
    }


    void Fire()
    {
        // ÁöÁ¤µÈ¸ñÇ¥°¡ ¾ø´Ù¸é ¸®ÅÏ
        if (!_player.Scanner.NearestTarget)
            return;

        Vector3 targetPos = _player.Scanner.NearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized; //ÇöÀç º¤ÅÍÀÇ ¹æÇâÀº À¯ÁöÇÏ°í Å©±â¸¦ 1·Î º¯È¯µÈ ¼Ó¼º
        // task ½ò¶§ ÀÚ±âÀÚ½Åµµ µ¹°Ô ¸¸µé¾î¾ßÇÑ´Ù

        Transform ore = GameManager.Instance.PoolManager.Get(_prefebId).transform;
        ore.position = transform.position;
        ore.rotation = Quaternion.FromToRotation(Vector3.up , dir);
        ore.GetComponent<Pick>().Init(_damage, _count, dir); 
    }
}
