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
        _rigid.velocity = Vector2.zero; //물리속도가 이동에 영향을주지 않도록 속도제거
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return; // 시간멈췃을때 입력도 받지 않기
        if (!_isLive)
            return;

        _spriter.flipX = _target.position.x < _rigid.position.x;
    }

    private void OnEnable() //오브젝트 풀링에서 다시 setactive가 true 가될때 다시 상태 초기화
    {
        _target = PlayerController.Instance.GetComponent<Rigidbody2D>();
        _isLive = true;             // 죽었는지 살았는지 확인하는 불값
        _health = _maxHealth;       // HP를 최대 체력으로 초기화
        _collider.enabled = true; // 충돌 활성화
        _rigid.simulated = true;  // 물리 활성화
        _spriter.sortingOrder = 2; // 죽었을때 레이어 값내려 다른 위에 안보이게
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
            return; // 시간멈췃을때 입력도 받지 않기
        if (!collision.CompareTag("Weapon") || !_isLive) // 웨폰이 아닌 다른것 혹은 죽어있을때는 리턴
            return;

        _health -= collision.GetComponent<Pick>().Damage;
        StartCoroutine("KnockBack");

        if (_health > 0){
            // 살아 있을떄 피격 이벤트
            _anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else {
            // 죽었을때
            GameObject Exp = GameManager.Instance.PoolManager.Get(Random.Range(3, 6)); // 풀매니저에 있는 3,4,5 번 생성
            Exp.transform.position = gameObject.transform.position;
            _isLive = false;
            _collider.enabled = false; // 충돌 비활성화
            _rigid.simulated = false; // 물리 비활성화
            _spriter.sortingOrder = 1; // 죽었을때 레이어 값내려 다른 위에 안보이게
            _anim.SetBool("Dead", true);
            GameManager.Instance.kill++;

            if(GameManager.Instance.IsLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
            // 애니메이션이 끝나면죽게 애니메이션 클립에 이벤트로 생성
        }
    }

    IEnumerator KnockBack()
    {
        // 충돌체에 맞았을때 어떤 물리로 뒤로 밀려가게 할껀지 vec? rig?
        yield return _wait; // _wait만큼 기다린다

        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        _rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); // 순간적으로 뒤로 밀리게 만들기
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
