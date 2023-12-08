using UnityEngine;

public class SightSensor : MonoBehaviour
{
    [SerializeField, Range(0,90f)] private int FOV;
    [SerializeField] private float range;
    [SerializeField] LayerMask layerMask;
    [SerializeField] private Transform target;

    public Blackboard blackboard;

    public bool isBlinded;
    private float maxBlindTimer = 5f;
    private float currentBlindTimer;

    private void Update()
    {
        if (blackboard == null) return;
        if (target == null) return;

        if (isBlinded)
        {
            currentBlindTimer -= Time.deltaTime;

            if(currentBlindTimer < 0 ) { isBlinded = false; }

            blackboard.SetVariable(VariableNames.BOOL_IS_BLINDED, true);
            EventManager.InvokeEvent(EventType.UnspottedPlayer);

            return;
        }


        blackboard.SetVariable(VariableNames.BOOL_IS_BLINDED, false);

        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(transform.position, target.transform.position - transform.position);

        float angleToTarget = Vector3.Angle(target.transform.position - transform.position, transform.forward);

        if(angleToTarget < FOV && angleToTarget > -FOV)
        {
            if (Physics.Raycast(ray, out hit, range, layerMask))
            {
                Player player = hit.transform.GetComponent<Player>();
                if (player != null && player?.IsDead == false)
                {
                    blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_IN_SIGHT, true);
                    EventManager.InvokeEvent(EventType.SpottedPlayer);
                }
                else
                {
                    blackboard.SetVariable(VariableNames.BOOL_IS_PLAYER_IN_SIGHT, false);
                    EventManager.InvokeEvent(EventType.UnspottedPlayer);
                }
            }
        }
    }

    public void BlindSight()
    {
        currentBlindTimer = maxBlindTimer;
        isBlinded = true;
    }
}
