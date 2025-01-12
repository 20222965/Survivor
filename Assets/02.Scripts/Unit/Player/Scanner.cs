using UnityEngine;
public class Scanner : MonoBehaviour
{
    [SerializeField] float scanRange;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] RaycastHit2D[] targets;
    
    public Transform NearestTarget { get; private set; }


    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        NearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float minDistance = 100;
        foreach (var target in targets)
        {
            Vector3 pos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDistance = Vector3.Distance(pos, targetPos);

            if (curDistance < minDistance)
            {
                minDistance = curDistance;
                result = target.transform;
            }
        }

        return result;
    }
}
