using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuControl : MonoBehaviour
{
    public GameObject rootMenuCanvas;
    public GameObject statUpgradeCanvas;
    public GameObject characterAbilitiesCanvas;
    public GameObject craftedUpgradesCanvas;    

    // Start is called before the first frame update
    void Start()
    {
    
    }


    // Update is called once per frame
    void Update()
    {
        // Root Menu Button will set UpgradeCategoryCanvas active, others inactive
        // Other buttons will set their canvas active, others inactive

    }
    public void RootMenu()
    {
        SceneManager.LoadScene(1);
        //rootMenuCanvas.SetActive(true);
        //statUpgradeCanvas.SetActive(false);
        //characterAbilitiesCanvas.SetActive(false);
        //craftedUpgradesCanvas.SetActive(false);
    }

    public void StatUpgrade()
    {
        rootMenuCanvas.SetActive(false);
        statUpgradeCanvas.SetActive(true);
        characterAbilitiesCanvas.SetActive(false);
        craftedUpgradesCanvas.SetActive(false);
    }

    public void CharacterAbilities()
    {
        rootMenuCanvas.SetActive(false);
        statUpgradeCanvas.SetActive(false);
        characterAbilitiesCanvas.SetActive(true);
        craftedUpgradesCanvas.SetActive(false);
    }

    public void CraftedUpgrades()
    {
        rootMenuCanvas.SetActive(false);
        statUpgradeCanvas.SetActive(false);
        characterAbilitiesCanvas.SetActive(false);
        craftedUpgradesCanvas.SetActive(true);
    }
}
