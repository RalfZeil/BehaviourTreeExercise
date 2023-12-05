using UnityEngine;

public class BTPickupWeapon : BTBaseNode
{
    float maxTimer;
    float timer;

    public BTPickupWeapon(float timer)
    {
        this.maxTimer = timer;
    }

    protected override TaskStatus OnUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return TaskStatus.Running;
        }

        blackboard.SetVariable(VariableNames.BOOL_HAS_WEAPON, true);
        return TaskStatus.Success;
    }

    protected override void OnEnter()
    {
        blackboard.SetVariable(VariableNames.BTBN_CURRENT_NODE, this);
        timer = maxTimer;
    }
}
