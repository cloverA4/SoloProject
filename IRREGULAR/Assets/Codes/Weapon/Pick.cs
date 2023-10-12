using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    [SerializeField] float _damage;
    [SerializeField] float _per;

    Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this._damage = damage; //this 해당클래스에 접근
        this._per = per;

        if (per > -1) // 관통력이 있는 애들은 원거리 공격으로 거르기
        {
            _rigid.velocity = dir * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster") || _per == -1) // 예외처리
            return;

        _per--;

        if (_per <= -1) // 관통력이 없다면 쏜 발사체 꺼주기
        {
            _rigid.velocity = Vector2.zero; // 위치 초기화
            gameObject.SetActive(false);
        }
    }
}
