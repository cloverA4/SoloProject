using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _health;
    [SerializeField] float _maxHealth;
    [SerializeField] Rigidbody2D _target;
    [SerializeField] RuntimeAnimatorController[] _animCon;

    bool _isLive;

    Rigidbody2D _rigid;
    Collider2D _collider;
    Animator _anim;
    SpriteRenderer _spriter;
    WaitForFixedUpdate _wait;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _anim = GetComponent<Animator>();
        _spriter = GetComponent<SpriteRenderer>();
        _wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!_isLive || _anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 DirVec = _target.position - _rigid.position;
        Vector2 NextVec = DirVec.normalized * _speed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + NextVec);
        _rigid.velocity = Vector2.zero; //¹°¸®¼Óµµ°¡ ÀÌµ¿¿¡ ¿µÇâÀ»ÁÖÁö ¾Êµµ·Ï ¼ÓµµÁ¦°Å
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return; // ½Ã°£¸Ø­ŸÀ»¶§ ÀÔ·Âµµ ¹ÞÁö ¾Ê±â
        if (!_isLive)
            return;

        _spriter.flipX = _target.position.x < _rigid.position.x;
    }

    private void OnEnable() //¿ÀºêÁ§Æ® Ç®¸µ¿¡¼­ ´Ù½Ã setactive°¡ true °¡µÉ¶§ ´Ù½Ã »óÅÂ ÃÊ±âÈ­
    {
        _target = PlayerController.Instance.GetComponent<Rigidbody2D>();
        _isLive = true;             // Á×¾ú´ÂÁö »ì¾Ò´ÂÁö È®ÀÎÇÏ´Â ºÒ°ª
        _health = _maxHealth;       // HP¸¦ ÃÖ´ë Ã¼·ÂÀ¸·Î ÃÊ±âÈ­
        _collider.enabled = true; // Ãæµ¹ È°¼ºÈ­
        _rigid.simulated = true;  // ¹°¸® È°¼ºÈ­
        _spriter.sortingOrder = 2; // Á×¾úÀ»¶§ ·¹ÀÌ¾î °ª³»·Á ´Ù¸¥ À§¿¡ ¾Èº¸ÀÌ°Ô
        _anim.SetBool("Dead", false);
    }

    public void Init(SpawnData data)
    {
        _anim.runtimeAnimatorController = _animCon[data.spriteType];
        _speed = data.speed;
        _maxHealth = data.health;
        _health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.Instance.IsLive)
            return; // ½Ã°£¸Ø­ŸÀ»¶§ ÀÔ·Âµµ ¹ÞÁö ¾Ê±â
        if (!collision.CompareTag("Weapon") || !_isLive) // ¿þÆùÀÌ ¾Æ´Ñ ´Ù¸¥°Í È¤Àº Á×¾îÀÖÀ»¶§´Â ¸®ÅÏ
            return;

        _health -= collision.GetComponent<Pick>().Damage;
        StartCoroutine("KnockBack");

        if (_health > 0){
            // »ì¾Æ ÀÖÀ»‹š ÇÇ°Ý ÀÌº¥Æ®
            _anim.SetTrigger("Hit");
        }
        else {
            // Á×¾úÀ»¶§
            GameObject Exp = GameManager.Instance.PoolManager.Get(Random.Range(3, 5)); // Ç®¸Å´ÏÀú¿¡ ÀÖ´Â 3,4 ¹ø »ý¼º
            Exp.transform.position = gameObject.transform.position;
            _isLive = false;
            _collider.enabled = false; // Ãæµ¹ ºñÈ°¼ºÈ­
            _rigid.simulated = false; // ¹°¸® ºñÈ°¼ºÈ­
            _spriter.sortingOrder = 1; // Á×¾úÀ»¶§ ·¹ÀÌ¾î °ª³»·Á ´Ù¸¥ À§¿¡ ¾Èº¸ÀÌ°Ô
            _anim.SetBool("Dead", true);
            GameManager.Instance.kill++;
            // ¾Ö´Ï¸ÞÀÌ¼ÇÀÌ ³¡³ª¸éÁ×°Ô ¾Ö´Ï¸ÞÀÌ¼Ç Å¬¸³¿¡ ÀÌº¥Æ®·Î »ý¼º
        }
    }

    IEnumerator KnockBack()
    {
        // Ãæµ¹Ã¼¿¡ ¸Â¾ÒÀ»¶§ ¾î¶² ¹°¸®·Î µÚ·Î ¹Ð·Á°¡°Ô ÇÒ²«Áö vec? rig?
        yield return _wait; // _wait¸¸Å­ ±â´Ù¸°´Ù

        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        _rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); // ¼ø°£ÀûÀ¸·Î µÚ·Î ¹Ð¸®°Ô ¸¸µé±â
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
