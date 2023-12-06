using UnityEngine;

public class BTThrow : BTBaseNode
{
    private string BBtargetPosition;
    private GameObject prefab;

    public BTThrow(GameObject prefab, string BBtargetPosition)
    {
        this.prefab = prefab;
        this.BBtargetPosition = BBtargetPosition;
    }

    protected override void OnEnter()
    {
        Object.Instantiate(prefab).GetComponent<IThrowable>().Throw(
            blackboard.GetVariable<Agent>(VariableNames.AGENT).transform.position, 
            blackboard.GetVariable<Vector3>(BBtargetPosition
        ));
    }

    protected override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
