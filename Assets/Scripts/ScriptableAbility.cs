using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class ScriptableAbility : ScriptableObject
{
    public enum TargetType { Enemy, Friendly, Self }; // Options for all enemies or all allies or 3 enemies or 3 allies?
    public TargetType targettype;
    public string abilityName;
    public int rank;
    public int HpDelta;
    public float abilityRange;
    public float abilityCooldown;
    public string description;
    public float effectDuration; 
    public int HpDeltaPerSecond; // DoT or HoT Abilities
    public float CooldownsDeltaPerSecond; // Haste or Slow Abilities
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
