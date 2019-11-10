using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardBlue : MonoBehaviour
{
    public float speed;
    
    // Start is called before the first frame update
    void Start() { }
    // Update is called once per frame
    void Update()
    {
        float distanceToClosestBlue = Mathf.Infinity;
        Blue closestBlue = null;
        Blue[] allBlues = GameObject.FindObjectsOfType<Blue>();

        foreach (Blue currentBlue in allBlues)
        {
            float distanceToBlue = (currentBlue.transform.position - transform.position).sqrMagnitude;
            if (distanceToBlue < distanceToClosestBlue)
            {
                distanceToClosestBlue = distanceToBlue;
                closestBlue = currentBlue;
            }
        }

        // while (Vector3.Distance(closestBlue.transform.position - transform.position) > gameObject.GetComponent.<Actor>.range
        {
            transform.position = Vector2.MoveTowards(transform.position, closestBlue.transform.position, speed * Time.deltaTime);
        }
    }    
}
