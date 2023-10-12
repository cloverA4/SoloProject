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

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
    }

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
            case 1:
                _timer += Time.deltaTime;

                if (_timer > _speed)
                {
                    _timer = 0;
                    Fire();
                }
                break;
        }
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
            case 1:
                _speed = 0.3f;
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
            // 기존 오브젝트 활용하고 부족한것은 풀링에서 가져오기

            pick.localPosition = Vector3.zero;
            pick.localRotation = Quaternion.identity;

            //한번삭제를 하고 다시 카운트만큼 생성해서 돌리기
            Vector3 rotVec = Vector3.forward * 360 * index / _count;
            pick.Rotate(rotVec);
            pick.Translate(pick.up * 1.5f, Space.World);
            pick.GetComponent<Pick>().Init(_damage, -1, Vector3.zero); // -1 는 무조껀 관통하게 만들어주는 것이다.
        }
    }

    void Fire()
    {
        // 지정된목표가 없다면 리턴
        if (!_player.Scanner.NearestTarget)
            return;

        Vector3 targetPos = _player.Scanner.NearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized; //현재 벡터의 방향은 유지하고 크기를 1로 변환된 속성
        // task 쏠때 자기자신도 돌게 만들어야한다

        Transform ore = GameManager.Instance.PoolManager.Get(_prefebId).transform;
        ore.position = transform.position;
        ore.rotation = Quaternion.FromToRotation(Vector3.up , dir);
        ore.GetComponent<Pick>().Init(_damage, _count, dir); 
    }
}
