using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] float _scanRange;
    float _expScanRange;
    LayerMask _targetLayerMon;
    LayerMask _targetLayerExp;
    RaycastHit2D[] _targets;
    RaycastHit2D[] _targetsExp;
    Transform _nearestTarget = null;

    //ΩÃ±€≈ÊµÈ
    public Transform NearestTarget
    {
        get { return _nearestTarget; }
        set { _nearestTarget = value; }
    }
    public float ExpScanRange
    {
        get { return _expScanRange; }
        set { _expScanRange = value; }
    }

    private void Awake()
    {
        _targetLayerMon = 1 << LayerMask.NameToLayer("Monster");
        _targetLayerExp = 1 << LayerMask.NameToLayer("Exp");
        _expScanRange = 1f;
    }


    private void FixedUpdate()
    {
        _targets = Physics2D.CircleCastAll(transform.position, _scanRange, Vector2.zero, 0, _targetLayerMon);
        _targetsExp = Physics2D.CircleCastAll(transform.position, _expScanRange, Vector2.zero, 0, _targetLayerExp);
        _nearestTarget = GetNearest();
        MagnetingExp();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in _targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }

    void MagnetingExp()
    {
        foreach (RaycastHit2D target in _targetsExp)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            
            float distance = Vector3.Distance(myPos, targetPos);
            Debug.Log(target);
            if (distance < _expScanRange)
            {
                target.transform.position = Vector3.MoveTowards(targetPos, myPos, 4f * Time.deltaTime);
            }
        }
    }

    
}

