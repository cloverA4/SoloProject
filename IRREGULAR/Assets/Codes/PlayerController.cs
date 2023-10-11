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


    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _inputVec.x = Input.GetAxis("Horizontal");
        _inputVec.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        _rigid.AddForce(_inputVec);

        _rigid.velocity = _inputVec;

        _rigid.MovePosition(_rigid.position + _inputVec);
    }
}
