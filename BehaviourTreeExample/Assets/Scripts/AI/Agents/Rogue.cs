using UnityEngine;

public class Rogue : Agent
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float keepDistance = 3.0f;

    [SerializeField] private Player player;

    [SerializeField] private GameObject smokebombPrefab;

    private void Start()
    {
        EventManager.AddListener(EventType.SpottedPlayer, () => blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_TARGETED, true));
        EventManager.AddListener(EventType.UnspottedPlayer, () => blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_TARGETED, false));

        blackboard = new Blackboard();
        blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_TARGETED, false);
        blackboard.SetVariable(VariableNames.AGENT, this);

        tree = new BTConditional( VariableNames.BOOL_IS_PLAYER_TARGETED,
                   new BTSequence( // Hide from guard and throw smokebomb
                       new BTGetNearestHidingSpot(HidingSpots.instance.hidingObjects.ToArray(), transform),
                       new BTMoveToPosition(nmAgent, moveSpeed, VariableNames.V3_NEAREST_HIDING_SPOT, 1f),
                       new BTCrossfadeAnimation(animator, "Crouch Idle", 0.5f),
                       new BTThrow(smokebombPrefab, VariableNames.V3_TARGET),
                       new BTWait(5f)
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
