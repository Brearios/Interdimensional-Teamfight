using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpriteControl : MonoBehaviour
{
    public GameObject fantasyMage;
    public GameObject fantasyPriest;
    public GameObject spaceTank;
    public GameObject postApocRogue;
    

    // Start is called before the first frame update
    void Start()
    {
    fantasyMage = GameObject.Find("/Fantasy Mage");
    fantasyPriest = GameObject.Find("/Fantasy Priest");
    spaceTank = GameObject.Find("/Space Tank");
    postApocRogue = GameObject.Find("/Post Apoc Rogue");
    }


    // Update is called once per frame
    void Update()
    {
        // Load Scene Character Sprite
        // this.gameObject.GetComponent<SpriteRenderer>().sprite = PlayerProfile.Instance.CurrentEditingCharacter.characterSprite;
        // Set prior Character Inactive

        switch (PlayerProfile.Instance.currentEditingInteger)
        {
            case 0:
                fantasyMage.SetActive(true);
                fantasyPriest.SetActive(false);
                spaceTank.SetActive(false);
                postApocRogue.SetActive(false);
                break;


            case 1:
                fantasyMage.SetActive(false);
                fantasyPriest.SetActive(false);
                spaceTank.SetActive(true);
                postApocRogue.SetActive(false); 
                break;

            case 2:
                fantasyMage.SetActive(false);
                fantasyPriest.SetActive(true);
                spaceTank.SetActive(false);
                postApocRogue.SetActive(false);
                break;

            case 3:
                fantasyMage.SetActive(false);
                fantasyPriest.SetActive(false);
                spaceTank.SetActive(false);
                postApocRogue.SetActive(true);
                break;
        } 
    }
}
