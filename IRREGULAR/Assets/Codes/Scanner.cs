using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] float _scanRange;
    [SerializeField] LayerMask _targetLayer;
    [SerializeField] RaycastHit2D[] _targets;
    [SerializeField] Transform _nearestTarget = null;

    private void FixedUpdate()
    {
        _targets = Physics2D.CircleCastAll(transform.position, _scanRange, Vector2.zero, 0,_targetLayer);
        _nearestTarget = GetNearest();
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

    public Transform NearestTarget()
    {
        return _nearestTarget;
    }
}

