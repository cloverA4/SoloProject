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
        _rigid.velocity = Vector2.zero; //�����ӵ��� �̵��� ���������� �ʵ��� �ӵ�����
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return; // �ð��ح����� �Էµ� ���� �ʱ�
        if (!_isLive)
            return;

        _spriter.flipX = _target.position.x < _rigid.position.x;
    }

    private void OnEnable() //������Ʈ Ǯ������ �ٽ� setactive�� true ���ɶ� �ٽ� ���� �ʱ�ȭ
    {
        _target = PlayerController.Instance.GetComponent<Rigidbody2D>();
        _isLive = true;             // �׾����� ��Ҵ��� Ȯ���ϴ� �Ұ�
        _health = _maxHealth;       // HP�� �ִ� ü������ �ʱ�ȭ
        _collider.enabled = true; // �浹 Ȱ��ȭ
        _rigid.simulated = true;  // ���� Ȱ��ȭ
        _spriter.sortingOrder = 2; // �׾����� ���̾� ������ �ٸ� ���� �Ⱥ��̰�
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
            return; // �ð��ح����� �Էµ� ���� �ʱ�
        if (!collision.CompareTag("Weapon") || !_isLive) // ������ �ƴ� �ٸ��� Ȥ�� �׾��������� ����
            return;

        _health -= collision.GetComponent<Pick>().Damage;
        StartCoroutine("KnockBack");

        if (_health > 0){
            // ��� ������ �ǰ� �̺�Ʈ
            _anim.SetTrigger("Hit");
        }
        else {
            // �׾�����
            GameObject Exp = GameManager.Instance.PoolManager.Get(Random.Range(3, 5)); // Ǯ�Ŵ����� �ִ� 3,4 �� ����
            Exp.transform.position = gameObject.transform.position;
            _isLive = false;
            _collider.enabled = false; // �浹 ��Ȱ��ȭ
            _rigid.simulated = false; // ���� ��Ȱ��ȭ
            _spriter.sortingOrder = 1; // �׾����� ���̾� ������ �ٸ� ���� �Ⱥ��̰�
            _anim.SetBool("Dead", true);
            GameManager.Instance.kill++;
            // �ִϸ��̼��� �������װ� �ִϸ��̼� Ŭ���� �̺�Ʈ�� ����
        }
    }

    IEnumerator KnockBack()
    {
        // �浹ü�� �¾����� � ������ �ڷ� �з����� �Ҳ��� vec? rig?
        yield return _wait; // _wait��ŭ ��ٸ���

        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        _rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); // ���������� �ڷ� �и��� �����
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
