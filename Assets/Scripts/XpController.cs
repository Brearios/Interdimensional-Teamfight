using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public int MageHeroXP;
    public int HealerHeroXP;
    public int TankHeroXP;
    public int RogueHeroXP;

    void Start()
    {
        // Find or Count Heroes in Scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
