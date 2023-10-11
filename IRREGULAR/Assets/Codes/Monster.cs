using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]float _speed;
    [SerializeField]Rigidbody2D _target;
    Rigidbody2D _rigid;
    SpriteRenderer _spriter;
    bool _isLive = true;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
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
}
