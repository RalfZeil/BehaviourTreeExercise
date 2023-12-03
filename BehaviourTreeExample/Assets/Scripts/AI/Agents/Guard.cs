using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public float moveSpeed = 3;
    public float keepDistance = 1f;
    public Transform[] wayPoints;
    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        //Create your Behaviour Tree here!
        Blackboard blackboard = new Blackboard();
        blackboard.SetVariable(VariableNames.INT_ENEMY_HEALTH, 100);
        blackboard.SetVariable(VariableNames.V3_TARGET_POSITION, new Vector3(0, 0, 0));
        blackboard.SetVariable(VariableNames.INT_CURRENT_PATROL_INDEX, -1);
        blackboard.SetVariable(VariableNames.V3_DEFAULT_VECTOR, new Vector3(0, 0, 0));
        blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_IN_SIGHT, false);
        blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_IN_RANGE, false);
        blackboard.SetVariable(VariableNames.BOOL_HAS_WEAPON, false);

        tree =
            new BTConditional(VariableNames.BOOL_IS_PLAYER_IN_SIGHT, 
                new BTConditional(VariableNames.BOOL_HAS_WEAPON,
                        new BTConditional(VariableNames.BOOL_IS_PLAYER_IN_RANGE,
                            new BTMoveToPosition(agent, moveSpeed, VariableNames.V3_DEFAULT_VECTOR, keepDistance), // TODO: Move towards player position
                            new BTAttack()
                            ),
                        new BTSequence(
                            // TODO: Search for weapon
                            // TODO: Pickup weapon
                            )
                    ),
                new BTRepeater(wayPoints.Length,
                    new BTSequence(
                        new BTGetNextPatrolPosition(wayPoints),
                        new BTMoveToPosition(agent, moveSpeed, VariableNames.V3_TARGET_POSITION, keepDistance)
                    )
                )
            );
        

        tree.SetupBlackboard(blackboard);
    }

    private void FixedUpdate()
    {
        TaskStatus result = tree.Tick();
    }
}
