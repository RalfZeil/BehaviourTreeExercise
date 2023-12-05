using UnityEngine;

public class BTCrossfadeAnimation : BTBaseNode
{
    Animator animator;
    string animationName;
    float crossfadeTime;

    public BTCrossfadeAnimation(Animator animator, string animationName, float crossfadeTime)
    {
        this.animator = animator;
        this.animationName = animationName;
        this.crossfadeTime = crossfadeTime;
    }

    protected override TaskStatus OnUpdate()
    {
        try
        {
            animator.CrossFade(animationName, crossfadeTime);
            return TaskStatus.Success;
        }
        catch
        {
            return TaskStatus.Failed;
        }
    }
}
