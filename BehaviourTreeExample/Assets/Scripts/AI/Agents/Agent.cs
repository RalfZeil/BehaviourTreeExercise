using TMPro;
using UnityEngine;
using UnityEngine.AI;

public abstract class Agent : MonoBehaviour
{
    [SerializeField] protected TextMeshPro currentStateTMP;

    protected BTBaseNode tree;
    protected NavMeshAgent nmAgent;
    protected Animator animator;
    protected Blackboard blackboard;

    protected void Awake()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        currentStateTMP.text = blackboard.GetVariable<BTBaseNode>(VariableNames.BTBN_CURRENT_NODE).GetType().Name;
    }
}
