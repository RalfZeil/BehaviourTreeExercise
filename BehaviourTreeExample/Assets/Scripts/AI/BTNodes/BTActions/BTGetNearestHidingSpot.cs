using UnityEngine;

public class BTGetNearestHidingSpot : BTBaseNode
{
    private Transform[] transforms;
    private Transform targetTransform;

    public BTGetNearestHidingSpot(Transform[] transforms, Transform target)
    {
        this.transforms = transforms;
        this.targetTransform = target;
    }

    protected override void OnEnter()
    {
        Transform nearestTransform = null;

        foreach (Transform t in transforms)
        {
            if (nearestTransform == null) { nearestTransform = t; }
            else if( Vector3.Distance(targetTransform.position, t.position) < Vector3.Distance(targetTransform.position, nearestTransform.position))
            {
                nearestTransform = t;
                continue;
            }
        }

        blackboard.SetVariable<Vector3>(VariableNames.V3_NEAREST_HIDING_SPOT, nearestTransform.position);
    }
    protected override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
