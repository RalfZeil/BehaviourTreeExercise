using UnityEngine;

public class BTAttack : BTBaseNode
{
    protected override TaskStatus OnUpdate()
    {
        return TaskStatus.Running;
    }

    protected override void OnEnter()
    {
        Debug.Log("Attacked");
    }
}
