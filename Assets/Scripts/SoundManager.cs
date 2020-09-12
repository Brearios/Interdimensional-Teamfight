using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public int simultaneousSounds;
    public List <ScriptableSoundData> scriptableSounds;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        // Sort sounds by priority



        // Play top simultaneousSounds # of Sounds


        // Play next tier of sounds at lower volume

        
        // Open up slots as sounds complete


       
    }
}
