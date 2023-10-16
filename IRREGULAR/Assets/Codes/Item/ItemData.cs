using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")] // 커스텀 메뉴를 생성하는 속성
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Pick,
        Ore,
        Glove,
        Shoe,
        CocaLeaf,
        Magnet,
        Bible,
        Cross,
    }

    [Header("# Main Info")]
    [SerializeField] ItemType _itemType;
    [SerializeField] int _itemId;      //아이템아이디
    [SerializeField] string _itemName; //아이템이름

    [TextArea] //인스펙터에 텍스트를 여러줄 넣을수 있게 해주는 TextArea속성 부여
    [SerializeField] string _itemDesc; //아이템설명
    [SerializeField] Sprite _itemIcon; //아이템이미지

    [Header("# Level Data")]
    [SerializeField] float _baseDamage; //기본데미지
    [SerializeField] int _baseCount; //기본갯수

    [SerializeField] float[] _Damages; //데미지
    [SerializeField] int[] _counts; //갯수

    [Header("# Weapon")]
    [SerializeField] GameObject _projectile;
    [SerializeField] Sprite _equip;

    //싱글톤들
    public ItemType Type
    {
        set { _itemType = value; }
        get { return _itemType; }
    }
    public int ItemId
    {
        set { _itemId = value; }
        get { return _itemId; }
    }
    public string ItemName
    {
        set { _itemName = value; }
        get { return _itemName; }
    }
    public string ItemDesc
    {
        set { _itemDesc = value; }
        get { return _itemDesc; }
    }
    public Sprite ItemIcon
    {
        set { _itemIcon = value; }
        get { return _itemIcon; }
    }
    public float BaseDamage
    {
        set { _baseDamage = value; }
        get { return _baseDamage; }
    }
    public int BaseCount
    {
        set { _baseCount = value; }
        get { return _baseCount; }
    }
    public int[] Counts
    {
        set { _counts = value; }
        get { return _counts; }
    }

    public float[] Damages
    {
        set { _Damages = value; }
        get { return _Damages; }
    }

    public GameObject Projectile
    {
        set { _projectile = value; }
        get { return _projectile; }
    }

    public Sprite Equip
    {
        set { _equip = value; }
        get { return _equip; }
    }
}


