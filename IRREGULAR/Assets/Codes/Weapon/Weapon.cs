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

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (_id)
        {
            case 0:
                transform.Rotate(Vector3.back * _speed * Time.deltaTime);
                break;
            default:
                _timer += Time.deltaTime;

                if (_timer > _speed)
                {
                    Fire();
                }
                break;
        }
        //test
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }

    public void Init()
    {
        switch (_id)
        {
            case 0:
                _speed = 150;
                Batch();
                break;
            default:
                break;
        }

        
        
    }

    public void LevelUp(float damage, int count)
    {
        this._damage = damage;
        this._count += count;

        if(_id == 0)
            Batch();
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
            pick.GetComponent<Pick>().Init(_damage, -1); // -1 �� ������ �����ϰ� ������ִ� ���̴�.
        }
    }

    void Fire()
    {
        
    }
}
