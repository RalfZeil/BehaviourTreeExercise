public class BTInvert : BTDecorator
{
    public BTInvert(BTBaseNode child) : base(child)
    {
    }

    protected override TaskStatus OnUpdate()
    {
        var result = child.Tick();

        switch (result)
        {
            case TaskStatus.Success: return TaskStatus.Failed;
            case TaskStatus.Failed: return TaskStatus.Success;
            case TaskStatus.Running: return TaskStatus.Running;
            default: return TaskStatus.Failed;
        }
    }
}
