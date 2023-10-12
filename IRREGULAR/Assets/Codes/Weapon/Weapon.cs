using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int _id;
    [SerializeField] int _prefebId;
    [SerializeField] float _damage;
    [SerializeField] int _count;
    [SerializeField] float _speed;

    float _timer;
    PlayerController _player;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    private void Awake()
    {
        _player = GameManager.Instance.Player;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsLive)
            return; // �ð��ح����� �Էµ� ���� �ʱ�

        switch (_id)
        {
            case 0:
                transform.Rotate(Vector3.back * _speed * Time.deltaTime);
                break;
            case 1:
                _timer += Time.deltaTime;

                if (_timer > _speed)
                {
                    _timer = 0;
                    Fire();
                }
                break;
        }
        if (Input.GetButtonDown("Jump")) //�׽�Ʈ�� ������
        {
            LevelUp(10, 1);
        }
    }

    public void Init(ItemData data)
    {
        //�⺻����
        name = "Weapon " + data.ItemId;
        transform.parent = _player.transform; // �θ� �÷��̾��
        transform.localPosition = Vector3.zero; // �÷��̾� ������ġ�� �������� ����

        //������ ���� ����
        _id = data.ItemId;
        _damage = data.BaseDamage;
        _count = data.BaseCount;

        for (int index = 0; index < GameManager.Instance.PoolManager.Prefabs.Length; index++) {
            if (data.Projectile == GameManager.Instance.PoolManager.Prefabs[index])
            {
                _prefebId = index;
                break;
            }
        }

        switch (_id)
        {
            case 0:
                _speed = 150;
                Batch();
                break;
            case 1:
                _speed = 0.4f;
                break;
        }
        //��� ���� ����
        Equip equip = _player.EquipWeapon[(int)data.Type];
        equip.Spriter.sprite = data.Equip;
        equip.gameObject.SetActive(true);

        _player.BroadcastMessage("ApplyPassive", SendMessageOptions.DontRequireReceiver);
        //Ư�� �Լ� ȣ���� ��� �ڽĿ��� ����ϴ� �Լ� ���ϳ��� ������Ʈ���ƴ϶� �÷��̾���� ��� �ڽĿ��� ApplyPassive �� �����϶�� ȣ����
        //�ֳ��ϸ� ��� ���׷��̵� �Ҷ����� ���ݼӵ� ������ ���� ���� �ϱ� �����̴�
    }

    void Batch()
    {
        for (int index = 0; index < _count; index++)
        {
            Transform pick; 

            if (index < transform.childCount) { 
                pick = transform.GetChild(index);
            }
            else{
                pick = GameManager.Instance.PoolManager.Get(_prefebId).transform;
                pick.parent = transform;
            }
            // ���� ������Ʈ Ȱ���ϰ� �����Ѱ��� Ǯ������ ��������

            pick.localPosition = Vector3.zero;
            pick.localRotation = Quaternion.identity;

            //�ѹ������� �ϰ� �ٽ� ī��Ʈ��ŭ �����ؼ� ������
            Vector3 rotVec = Vector3.forward * 360 * index / _count;
            pick.Rotate(rotVec);
            pick.Translate(pick.up * 1.5f, Space.World);
            pick.GetComponent<Pick>().Init(_damage, -1, Vector3.zero); // -1 �� ������ �����ϰ� ������ִ� ���̴�.
        }
    }

    public void LevelUp(float damage, int count)
    {
        this._damage = damage;
        this._count += count;

        if(_id == 0)
            Batch();

        _player.BroadcastMessage("ApplyPassive",SendMessageOptions.DontRequireReceiver);
    }


    void Fire()
    {
        // �����ȸ�ǥ�� ���ٸ� ����
        if (!_player.Scanner.NearestTarget)
            return;

        Vector3 targetPos = _player.Scanner.NearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized; //���� ������ ������ �����ϰ� ũ�⸦ 1�� ��ȯ�� �Ӽ�
        // task �� �ڱ��ڽŵ� ���� �������Ѵ�

        Transform ore = GameManager.Instance.PoolManager.Get(_prefebId).transform;
        ore.position = transform.position;
        ore.rotation = Quaternion.FromToRotation(Vector3.up , dir);
        ore.GetComponent<Pick>().Init(_damage, _count, dir); 
    }
}
