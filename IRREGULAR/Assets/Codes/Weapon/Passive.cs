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

        _type = data.Type; //������ Ÿ��
        _rate = data.Damages[0];

        ApplyPassive();
    }

    public void LevelUp(float rate)
    {
        this._rate = rate;
        ApplyPassive(); //������������ ����
    }

    void ApplyPassive() //Ÿ�Կ����� �����ϰ� ������ ��������ִ� �Լ�
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
    { //���� ���ݼӵ�
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            switch (weapon.Id)
            {
                case 0://�ٰŸ� ���ݼӵ�
                    weapon.Speed = 150 + (150 * _rate);
                    break;
                case 1://���Ÿ����ݼӵ�
                    weapon.Speed = 0.5f * (1f - _rate);
                    break;
            }
        }
    }

    void SpeedUp() //�ӵ���
    {
        float speed = 3;
        GameManager.Instance.Player.Speed = speed + speed * _rate;
    }
}
