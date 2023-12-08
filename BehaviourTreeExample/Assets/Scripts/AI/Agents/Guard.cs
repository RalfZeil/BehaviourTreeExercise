using UnityEngine;

public class Guard : Agent
{
    public float moveSpeed = 3;
    public float runSpeed = 5;
    public float keepDistance = 2f;
    public Transform[] wayPoints;
    public GameObject weaponLocation;
    
    [SerializeField] private Player player;

    private void Start()
    {
        blackboard = new Blackboard();

        EventManager.AddListener(EventType.PlayerDied, () => blackboard.SetVariable(VariableNames.BOOL_IS_TARGET_ALIVE, true));

        blackboard.SetVariable(VariableNames.INT_ENEMY_HEALTH, 100);
        blackboard.SetVariable(VariableNames.V3_TARGET_POSITION, new Vector3(0, 0, 0));
        blackboard.SetVariable(VariableNames.INT_CURRENT_PATROL_INDEX, -1);
        blackboard.SetVariable(VariableNames.V3_WEAPON_LOCATION, weaponLocation.transform.position);
        blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_IN_SIGHT, false);
        blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_IN_RANGE, false);
        blackboard.SetVariable(VariableNames.BOOL_HAS_WEAPON, false);
        blackboard.SetVariable(VariableNames.AGENT, this);
        blackboard.SetVariable(VariableNames.V3_TARGET, player.transform.position);
        blackboard.SetVariable(VariableNames.BOOL_IS_TARGET_ALIVE, false);

        GetComponent<SightSensor>().blackboard = blackboard;

        Debug.Log(blackboard.GetVariable<Vector3>(VariableNames.V3_TARGET));

        tree =
            new BTConditional(VariableNames.BOOL_IS_BLINDED,
                new BTSequence(
                    new BTCrossfadeAnimation(animator, "Scared", 0.5f),
                    new BTWait(3f)
                ),
                new BTConditional(VariableNames.BOOL_IS_PLAYER_IN_SIGHT,
                    new BTConditional(VariableNames.BOOL_HAS_WEAPON,
                            new BTSequence(
                                new BTSequence(
                                    new BTCrossfadeAnimation(animator, "Run", 0f),
                                    new BTMoveToPosition(nmAgent, runSpeed, VariableNames.V3_TARGET, keepDistance)
                                ),
                                new BTSequence(
                                    new BTCrossfadeAnimation(animator, "Kick", 0f),
                                    new BTAttack(1f, 1f, this.transform)
                                )
                            ),
                            new BTSequence(
                                new BTCrossfadeAnimation(animator, "Run", 1f),
                                new BTMoveToPosition(nmAgent, runSpeed, VariableNames.V3_WEAPON_LOCATION, keepDistance),
                                new BTCrossfadeAnimation(animator, "Crouch Idle", 0.1f),
                                new BTPickupWeapon(2f)
                            )
                    ),
                    new BTRepeater(wayPoints.Length,
                        new BTSequence(
                            new BTCrossfadeAnimation(animator, "Rifle Walk", 1f),
                            new BTGetNextPatrolPosition(wayPoints),
                            new BTMoveToPosition(nmAgent, moveSpeed, VariableNames.V3_TARGET_POSITION, keepDistance)
                        )
                    )
                )
            );
            

        blackboard.SetVariable(VariableNames.BTBN_CURRENT_NODE, tree);


        tree.SetupBlackboard(blackboard);
    }

    private void Update()
    {
        currentStateTMP.text = blackboard.GetVariable<BTBaseNode>(VariableNames.BTBN_CURRENT_NODE).GetType().Name;
        blackboard.SetVariable(VariableNames.V3_TARGET, player.transform.position);

        if(Vector3.Distance(transform.position, player.transform.position) < keepDistance) { blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_IN_RANGE, true); }
        else { blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_IN_RANGE, false); }

        TaskStatus result = tree.Tick();
    }
}
