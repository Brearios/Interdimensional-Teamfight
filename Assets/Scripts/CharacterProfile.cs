using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProfile : MonoBehaviour
{
    public static CharacterProfile Instance;
    public int characterTotalXP;
    public int characterAvailableXP;
    public int healthLevel;
    public int AtkLevel;
    public int AbilityLevel;
    public int nextXPCost;
    
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