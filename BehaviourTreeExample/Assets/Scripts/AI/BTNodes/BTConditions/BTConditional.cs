public class BTConditional : BTComposite
{
    string condition;

    public BTConditional(string condition, BTBaseNode childTrue, BTBaseNode childFalse) : base(childTrue, childFalse)
    {
        this.condition = condition;
    }

    protected override TaskStatus OnUpdate()
    {
        if (blackboard.GetVariable<bool>(condition))
        {
            return children[0].Tick();
        }
        else
        {
            return children[1].Tick();
        }
    }
}
