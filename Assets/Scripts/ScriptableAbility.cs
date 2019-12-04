using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class ScriptableAbility : ScriptableObject
{
    public string abilityName;
    public int rank;
    public float abilityRange;
    public float abilityCooldown;
    public bool usedOnFriends; // Can use on friendly targets
    public bool usedOnSelf; // Can only use on self

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
