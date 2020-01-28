using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProfile : MonoBehaviour
{
    // public static CharacterProfile Instance;
    public int characterTotalXP;
    public int characterAvailableXP;
    // _ArrayLevel variables are to count XP cost increments
    public int healthArrayLevel;
    public int atkArrayLevel;
    public int abilityArrayLevel;
    public int nextXPCost;
    public int health;
    public int atk;
    public int abilityPower;
    public bool created = false;
        
    private void Awake()
    {
        /*
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(gameObject);
        }
        */
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {

    }


}