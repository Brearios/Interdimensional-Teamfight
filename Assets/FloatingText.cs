using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{

    public float DestroyTime = .4f;
    public Vector3 Offset = new Vector3(0, 2, 0);
    public Vector3 driftRate = new Vector3(0, .0001f, 0);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, (DestroyTime / GameManager.Instance.gameSpeed));

        transform.localPosition += Offset;
      //  transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x), 
      //  Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y), Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (driftRate / GameManager.Instance.gameSpeed);
    }
}
