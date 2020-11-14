using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enhancement", menuName = "Enhancement")]

public class ScriptableEnhancement : ScriptableObject
{
    public string enhancementName;
    public int enhancementLevel;
    public enum effectType { addHealth, addDamage, addPower };

    // Some kind of swappable/moveable upgrade item
    // public PowerCore? currentPowerCore;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
