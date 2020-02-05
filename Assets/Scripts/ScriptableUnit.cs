 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class ScriptableUnit : ScriptableObject
{
    public string unitName;
    public int maxHealth;
    public int attackDamage;
    // Later make this a random amount within a range, probably min/max fields
    public int abilityPower;
    public int globalCooldown;
    public int xpWhenKilled;
    public string role;
    public string team;
    public float atkRange;
    public float moveSpeed;
    public float targetCheckFrequency;
}
