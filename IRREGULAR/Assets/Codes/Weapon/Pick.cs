using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    [SerializeField] float _damage;
    [SerializeField] float _per;

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public void Init(float damage, int per)
    {
        this._damage = damage; //this 해당클래스에 접근
        this._per = per;
    }
}
