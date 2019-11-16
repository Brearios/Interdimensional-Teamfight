using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    public static DisplayText Instance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsRunning == true)
        {
            gameObject.SetActive(false); // Can't re-activate since this script is disabled
        }
    }
}
