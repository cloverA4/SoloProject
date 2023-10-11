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

    Rigidbody2D _rigid;
    Animator _anim;
    SpriteRenderer _spriter;
    bool _isLive;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!_isLive)
            return;

        Vector2 DirVec = _target.position - _rigid.position;
        Vector2 NextVec = DirVec.normalized * _speed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + NextVec);
        _rigid.velocity = Vector2.zero; //�����ӵ��� �̵��� ���������� �ʵ��� �ӵ�����
    }

    private void LateUpdate()
    {
        if (!_isLive)
            return;

        _spriter.flipX = _target.position.x < _rigid.position.x;
    }

    private void OnEnable() //������Ʈ Ǯ������ �ٽ� setactive�� true ���ɶ� �ٽ� ���� �ʱ�ȭ
    {
        _target = PlayerController.Instance.GetComponent<Rigidbody2D>();
        _isLive = true;
        _health = _maxHealth;
    }

    public void Init(SpawnData data)
    {
        _anim.runtimeAnimatorController = _animCon[data.spriteType];
        _speed = data.speed;
        _maxHealth = data.health;
        _health = data.health;
    }
}
