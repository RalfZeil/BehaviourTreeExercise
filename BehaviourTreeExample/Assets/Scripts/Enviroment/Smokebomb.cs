using UnityEngine;

public class Smokebomb : MonoBehaviour, IThrowable
{
    [SerializeField] private GameObject smokecloudPrefab;

    private float radius = 4f;
    private float speed = 0.1f;
    private Vector3 endPoint;
    private bool throwing = false;

    public void Detonate()
    {
        Instantiate(smokecloudPrefab, transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            collider.transform.GetComponent<SightSensor>()?.BlindSight();
        }

        throwing = false;
    }

    public void Throw(Vector3 startPoint, Vector3 endPoint)
    {
        transform.position = startPoint;
        this.endPoint = endPoint;
        throwing = true;
    }

    public void FixedUpdate()
    {
        if(throwing)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed);

            if (Vector3.Distance(transform.position, endPoint) < 0.001f)
            {
                endPoint = Vector3.zero;
                Detonate();
            }
        }
    }
}
