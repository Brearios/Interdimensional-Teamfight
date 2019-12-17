using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Effect")]
public class ScriptableEffects : ScriptableObject
{
    public string effectName;
    public float effectMagnitude;
    public float effectDuration;
    public bool canStack;
    public int numStacks;
    public int maxStacks;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
