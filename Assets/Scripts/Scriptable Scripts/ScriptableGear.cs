using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gear Item", menuName = "Gear")]

public class ScriptableGear : ScriptableObject
{
    public int currentLevel;
    public string itemName;
    public string[] rankNames;
    public int enhancementSlots;
    public ScriptableEnhancement[] currentEnhancements;
}
