///
/// The Parallel node runs all its children at the same time
///
public class BTParallel : BTComposite
{
    public BTParallel(params BTBaseNode[] children) : base(children) { }

    protected override TaskStatus OnUpdate()
    {
        foreach(var child in children)
        {
            var result = child.Tick();

            switch(result)
            {
                case TaskStatus.Success: continue;
                case TaskStatus.Failed: return TaskStatus.Failed;
                case TaskStatus.Running: return TaskStatus.Running;
            }
        }

        return TaskStatus.Success;
    }

    protected override void OnEnter()
    {
    }

    protected override void OnExit()
    {
    }

    public override void OnReset()
    {
        foreach (var c in children)
        {
            c.OnReset();
        }
    }
}

