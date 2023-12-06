using UnityEngine;

public class Smokecloud : MonoBehaviour
{
    float lifeTimeInSeconds = 5f;
    
    // Update is called once per frame
    void Update()
    {
        while(lifeTimeInSeconds > 0)
        {
            lifeTimeInSeconds -= Time.deltaTime;
            return;
        }

        Destroy(gameObject);
    }
}
