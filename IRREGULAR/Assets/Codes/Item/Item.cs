using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData _data;
    int _level;
    Weapon _weapon;
    Passive _passive;

    Image _icon;
    Text _textLevel; //레벨
    Text _textName; //이름
    Text _textDesc; //설명

    public int level
    {
        get { return _level; }
        set { _level = value; }
    }
    public ItemData Data
    {
        get { return _data; }
        set { _data = value; }
    }

    private void Awake()
    {
        _icon = GetComponentsInChildren<Image>()[1];
        _icon.sprite = _data.ItemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        _textLevel = texts[0];
        _textName = texts[1];
        _textDesc = texts[2];

        _textName.text = _data.ItemName;
    }

    private void OnEnable()
    {
        _textLevel.text = "Lv." + _level;

        switch (_data.Type)
        {
            case ItemData.ItemType.Pick:
            case ItemData.ItemType.Ore:
                _textDesc.text = string.Format(_data.ItemDesc, _data.Damages[_level] * 100 , _data.Counts[_level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                _textDesc.text = string.Format(_data.ItemDesc, _data.Damages[_level] * 100);
                break;
            case ItemData.ItemType.CocaLeaf:
                _textDesc.text = string.Format(_data.ItemDesc);
                break;
            case ItemData.ItemType.Magnet:
                _textDesc.text = string.Format(_data.ItemDesc, _data.Damages[_level]);
                break;

        }
    }

    public void OnClick()
    {
        switch (_data.Type)
        {
            case ItemData.ItemType.Pick:
            case ItemData.ItemType.Ore:
                if (_level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    _weapon = newWeapon.AddComponent<Weapon>(); //직접만든 컴포넌트를 반환가능
                    _weapon.Init(_data);
                }
                else {
                    float nextDamage = _data.BaseDamage;
                    int nextCount = 0;

                    nextDamage += _data.BaseDamage * _data.Damages[_level]; // 베이스 데미지에 데미지 레벨에맞는걸 곱해준다
                    nextCount += _data.Counts[_level];

                    _weapon.LevelUp(nextDamage, nextCount);
                }
                _level++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (_level == 0)
                {
                    GameObject newGear = new GameObject();
                    _passive = newGear.AddComponent<Passive>();
                    _passive.Init(_data);
                }
                else {
                    float nextRate = _data.Damages[_level];
                    _passive.LevelUp(nextRate);
                }
                _level++;
                break;
            case ItemData.ItemType.CocaLeaf:
                GameManager.Instance.playerHealth = GameManager.Instance.playerMaxHealth;
                break;
            case ItemData.ItemType.Magnet:
                PlayerController.Instance.Scanner.ExpScanRange += _data.Damages[_level];
                _level++;
                break;
        }
        if (_level == _data.Damages.Length){
            GetComponent<Button>().interactable = false;
        }
    }
}
