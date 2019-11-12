using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardRed : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start() { }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsRunning == false)
        {
            return;
        }
        else
        {
            float distanceToClosestRed = Mathf.Infinity;
            Red closestRed = null;
            Red[] allReds = GameObject.FindObjectsOfType<Red>();

            foreach (Red currentRed in allReds)
            {
                float distanceToRed = (currentRed.transform.position - transform.position).sqrMagnitude;
                if (distanceToRed < distanceToClosestRed)
                {
                    distanceToClosestRed = distanceToRed;
                    closestRed = currentRed;
                }
            }

            // while (Vector3.Distance(closestRed.transform.position - transform.position) > gameObject.GetComponent.<Actor>.range
            /* float dist = (closestRed.transform.position - transform.position).magnitude;
                while (dist > 1) */
            {
                transform.position = Vector2.MoveTowards(transform.position, closestRed.transform.position, speed * Time.deltaTime);
            }
        }
    }
}
