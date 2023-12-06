using UnityEngine;

public class BTWait : BTBaseNode
{
    float maxTime;
    float time;

    public BTWait(float waitTime)
    {
        this.maxTime = waitTime;

    }
    protected override TaskStatus OnUpdate()
    {
        while(time > 0)
        {
            time -= Time.deltaTime; 
            return TaskStatus.Running;
        }

        return TaskStatus.Success;
    }

    protected override void OnEnter()
    {
        time = maxTime;
    }
}
