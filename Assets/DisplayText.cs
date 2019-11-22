using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public static DisplayText Instance;
    Text text;
<<<<<<< HEAD

=======
    
>>>>>>> b900fd467c413560abe60215bc6c62f400f287ba
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // By setting the entire game object inactive, yes you
        // prevent it's lifecycle from calling. You can enable/disable
        // individual components on a game object like this.
        text.enabled = !GameManager.Instance.IsRunning;
    }
}