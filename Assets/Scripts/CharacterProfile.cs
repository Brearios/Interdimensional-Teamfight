using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProfile : MonoBehaviour
{
    public static CharacterProfile Instance;
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

    

    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {

    }


}