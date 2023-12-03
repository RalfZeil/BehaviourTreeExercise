using UnityEngine;

public class SightSensor : MonoBehaviour
{
    [Range(0, 90f)]
    [SerializeField] private int FOV;
    [SerializeField] private float range;
    [SerializeField] LayerMask layerMask;

    [SerializeField] private bool IsInSight;
    [SerializeField] private Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(transform.position, target.transform.position - transform.position);

        float angleToTarget = Vector3.Angle(target.transform.position - transform.position, transform.forward);

        if(angleToTarget < FOV && angleToTarget > -FOV)
        {
            if (Physics.Raycast(ray, out hit, range, layerMask))
            {
                if (hit.transform.GetComponent<Player>() != null)
                {

                    Debug.Log("Player in sight!");
                }
            }
        }
    }
}
