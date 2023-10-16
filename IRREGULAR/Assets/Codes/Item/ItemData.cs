using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")] // Ŀ���� �޴��� �����ϴ� �Ӽ�
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
    [SerializeField] int _itemId;      //�����۾��̵�
    [SerializeField] string _itemName; //�������̸�

    [TextArea] //�ν����Ϳ� �ؽ�Ʈ�� ������ ������ �ְ� ���ִ� TextArea�Ӽ� �ο�
    [SerializeField] string _itemDesc; //�����ۼ���
    [SerializeField] Sprite _itemIcon; //�������̹���

    [Header("# Level Data")]
    [SerializeField] float _baseDamage; //�⺻������
    [SerializeField] int _baseCount; //�⺻����

    [SerializeField] float[] _Damages; //������
    [SerializeField] int[] _counts; //����

    [Header("# Weapon")]
    [SerializeField] GameObject _projectile;
    [SerializeField] Sprite _equip;

    //�̱����
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


