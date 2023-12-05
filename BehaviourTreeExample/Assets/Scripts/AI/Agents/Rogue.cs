using UnityEngine;

public class Rogue : Agent
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float keepDistance = 3.0f;

    [SerializeField] private Player player;

    private void Start()
    {
        //TODO: Create your Behaviour tree here
        blackboard = new Blackboard();
        blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_TARGETED, false);

        tree = new BTConditional( VariableNames.BOOL_IS_PLAYER_TARGETED,
                   new BTSequence( // Hide from guard and throw smokebomb
                   ),
                   new BTSequence( // Move to player
                       new BTCrossfadeAnimation(animator, "Walk Crouch", 0.2f),
                       new BTMoveToPosition(nmAgent, moveSpeed, VariableNames.V3_TARGET, keepDistance)
                   )
               );

        tree.SetupBlackboard(blackboard);
    }

    private void FixedUpdate()
    {
        blackboard.SetVariable(VariableNames.V3_TARGET, player.transform.position);

        tree?.Tick();
    }
}
