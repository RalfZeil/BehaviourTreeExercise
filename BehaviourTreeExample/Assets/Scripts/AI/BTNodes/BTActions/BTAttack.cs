using UnityEngine;

public class BTAttack : BTBaseNode
{
    float maxTimer;
    float timer;
    float radius;
    Transform attacker;

    public BTAttack(float timer, float radius, Transform attackOrigin)
    {
        this.maxTimer = timer;
        this.radius = radius;
        this.attacker = attackOrigin;
    }
    protected override TaskStatus OnUpdate()
    {
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            return TaskStatus.Running;
        }

        // TODO: Improve hit detection (Not a deliverable)
        Vector3 position = attacker.position;
        Collider[] colliders = Physics.OverlapSphere(new Vector3(position.x, position.y, position.z), radius);

        foreach(Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable var))
            {
                var.TakeDamage(blackboard.GetVariable<Agent>(VariableNames.AGENT).gameObject);
            }
        }

        return TaskStatus.Success;
    }

    protected override void OnEnter()
    {
        timer = maxTimer;
    }
}
