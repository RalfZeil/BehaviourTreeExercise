using UnityEngine;

public class SightSensor : MonoBehaviour
{
    [SerializeField, Range(0,90f)] private int FOV;
    [SerializeField] private float range;
    [SerializeField] LayerMask layerMask;

    public Blackboard blackboard;
    [SerializeField] private Transform target;

    void Start()
    {
    }

    void Update()
    {
        if (blackboard == null) return;
        if (target == null) return;

        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(transform.position, target.transform.position - transform.position);

        float angleToTarget = Vector3.Angle(target.transform.position - transform.position, transform.forward);

        if(angleToTarget < FOV && angleToTarget > -FOV)
        {
            if (Physics.Raycast(ray, out hit, range, layerMask))
            {
                if (hit.transform.GetComponent<Player>() != null)
                {
                    blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_IN_SIGHT, true);
                    EventManager.InvokeEvent(EventType.SpottedPlayer);
                }
                else
                {
                    blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_IN_SIGHT, false);
                }
            }
        }
    }
}
