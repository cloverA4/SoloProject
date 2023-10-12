using UnityEngine;

public class Equip : MonoBehaviour
{
    [SerializeField] bool _isLeftHand;
    [SerializeField] SpriteRenderer _spriter;

    SpriteRenderer _player;

    Vector3 _rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 _rightPosReverse = new Vector3(-0.35f, -0.15f, 0);
    Quaternion _leftRot = Quaternion.Euler(0,0,-35f);
    Quaternion _leftPosReverse = Quaternion.Euler(0, 0,-135f);

    public SpriteRenderer Spriter
    {
        get { return _spriter; }
        set { _spriter = value; }
    }

    private void Awake()
    {
        _player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool isReverse = _player.flipX;
        if (_isLeftHand)// 근접무기
        {
            transform.localRotation = isReverse ? _leftPosReverse : _leftRot;
            _spriter.flipY = isReverse;
            _spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else//원거리무기
        {
            transform.localPosition = isReverse ? _rightPosReverse : _rightPos;
            _spriter.flipX = isReverse;
            _spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
