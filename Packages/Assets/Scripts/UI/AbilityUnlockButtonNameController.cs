using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAbilityUnlockButtonNameController : MonoBehaviour
{
    public CharacterProfile EditingCharacter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EditingCharacter = PlayerProfile.Instance.CurrentEditingCharacter;

        //Ability2ButtonUpdater();
        //Ability3ButtonUpdater();
        //PotionButtonUpdater();
    }

    void Ability2ButtonUpdater()
    {
        // GameObject.Find("Ability2Button").GetComponentInChildren<Text>().text = "Placeholder text. Spend 500 xp to unlock ability2 for {0}!", EditingCharacter.heroName;
    }

    void Ability3ButtonUpdater()
    {
        // GameObject.Find("Ability3Button").GetComponentInChildren<Text>().text = "la di da";
    }

    void PotionButtonUpdater()
    {
        // GameObject.Find("PotionButton").GetComponentInChildren<Text>().text = "la di da";
    }
}
