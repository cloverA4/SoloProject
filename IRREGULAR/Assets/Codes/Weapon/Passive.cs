using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive : MonoBehaviour
{
    ItemData.ItemType _type;
    float _rate;

    public void Init(ItemData data)
    {
        name = "Passive" + data.ItemId;
        transform.parent = GameManager.Instance.Player.transform;
        transform.localScale = Vector3.zero;

        _type = data.Type; //아이템 타입
        _rate = data.Damages[0];

        ApplyPassive();
    }

    public void LevelUp(float rate)
    {
        this._rate = rate;
        ApplyPassive(); //레벨업했을때 적용
    }

    void ApplyPassive() //타입에따라 적절하게 로직을 적용시켜주는 함수
    {
        switch (_type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    { //무기 공격속도
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            switch (weapon.Id)
            {
                case 0://근거리 공격속도
                    weapon.Speed = 150 + (150 * _rate);
                    break;
                case 1://원거리공격속도
                    weapon.Speed = 0.5f * (1f - _rate);
                    break;
            }
        }
    }

    void SpeedUp() //속도업
    {
        float speed = 3;
        GameManager.Instance.Player.Speed = speed + speed * _rate;
    }
}
