using System.Collections;
using System.Collections.Generic;
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

    private Vector2 _inputVec;
    private Rigidbody2D _rigid;
    private float _speed;
    private SpriteRenderer _spriter;


    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _speed = 3f;
        _spriter = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _inputVec.x = Input.GetAxisRaw("Horizontal");
        _inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = _inputVec.normalized * _speed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        if (_inputVec.x != 0)
        {
            _spriter.flipX = _inputVec.x < 0;
        }
    }
}
