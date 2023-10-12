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
        this._damage = damage; //this �ش�Ŭ������ ����
        this._per = per;

        if (per > -1) // ������� �ִ� �ֵ��� ���Ÿ� �������� �Ÿ���
        {
            _rigid.velocity = dir * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster") || _per == -1) // ����ó��
            return;

        _per--;

        if (_per <= -1) // ������� ���ٸ� �� �߻�ü ���ֱ�
        {
            _rigid.velocity = Vector2.zero; // ��ġ �ʱ�ȭ
            gameObject.SetActive(false);
        }
    }
}
