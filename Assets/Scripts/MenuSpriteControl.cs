using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpriteControl : MonoBehaviour
{
    public GameObject fantasyMage;
    public GameObject fantasyPriest;
    public GameObject spaceTank;
    public GameObject postApocRogue;
    public GameObject steamBot;
    public GameObject plantHero;
    public GameObject spaceSoldier;
    public List<GameObject> heroSprites;
    

    // Start is called before the first frame update
    void Start()
    {
    fantasyMage = GameObject.Find("/Fantasy Mage");
    fantasyPriest = GameObject.Find("/Fantasy Priest");
    spaceTank = GameObject.Find("/Space Tank");
    postApocRogue = GameObject.Find("/Post Apoc Rogue");
    steamBot = GameObject.Find("SteamBot");
    plantHero = GameObject.Find("Plantbassador");
    spaceSoldier = GameObject.Find("Space Soldier");


    heroSprites.Add(fantasyMage);
    heroSprites.Add(fantasyPriest);
    heroSprites.Add(spaceTank);
    heroSprites.Add(postApocRogue);
    heroSprites.Add(steamBot);
    heroSprites.Add(plantHero);
    heroSprites.Add(spaceSoldier);

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
                steamBot.SetActive(false);
                plantHero.SetActive(false);
                spaceSoldier.SetActive(false);
                break;


            case 1:
                fantasyMage.SetActive(false);
                fantasyPriest.SetActive(true);
                spaceTank.SetActive(false);
                postApocRogue.SetActive(false);
                steamBot.SetActive(false);
                plantHero.SetActive(false);
                spaceSoldier.SetActive(false);
                break;

            case 2:
                fantasyMage.SetActive(false);
                fantasyPriest.SetActive(false);
                spaceTank.SetActive(true);
                postApocRogue.SetActive(false);
                steamBot.SetActive(false);
                plantHero.SetActive(false);
                spaceSoldier.SetActive(false);
                break;

            case 3:
                fantasyMage.SetActive(false);
                fantasyPriest.SetActive(false);
                spaceTank.SetActive(false);
                postApocRogue.SetActive(true);
                steamBot.SetActive(false);
                plantHero.SetActive(false);
                spaceSoldier.SetActive(false);
                break;

            case 4:
                fantasyMage.SetActive(false);
                fantasyPriest.SetActive(false);
                spaceTank.SetActive(false);
                postApocRogue.SetActive(false);
                steamBot.SetActive(true);
                plantHero.SetActive(false);
                spaceSoldier.SetActive(false);
                break;

            case 5:
                fantasyMage.SetActive(false);
                fantasyPriest.SetActive(false);
                spaceTank.SetActive(false);
                postApocRogue.SetActive(false);
                steamBot.SetActive(false);
                plantHero.SetActive(true);
                spaceSoldier.SetActive(false);
                break;

            case 6:
                fantasyMage.SetActive(false);
                fantasyPriest.SetActive(false);
                spaceTank.SetActive(false);
                postApocRogue.SetActive(false);
                steamBot.SetActive(false);
                plantHero.SetActive(false);
                spaceSoldier.SetActive(true);
                break;
        } 
    }
}


//            case 0:
//                foreach (GameObject hero in heroSprites)
//                { 
//                    hero.SetActive(false);
//                }
//                fantasyMage.SetActive(true);
//                break;


//            case 1:
//                foreach (GameObject hero in heroSprites)
//                {
//                    hero.SetActive(false);
//                }
//                fantasyPriest.SetActive(true);
//                break;

//            case 2:
//                foreach (GameObject hero in heroSprites)
//                {
//                    hero.SetActive(false);
//                }
//                spaceTank.SetActive(true);
//                break;

//            case 3:
//                foreach (GameObject hero in heroSprites)
//                {
//                    hero.SetActive(false);
//                }
//                postApocRogue.SetActive(true);
//                break;

//            case 4:
//                foreach (GameObject hero in heroSprites)
//                {
//                    hero.SetActive(false);
//                }
//                steamBot.SetActive(true);
//                break;

//            case 5:
//                foreach (GameObject hero in heroSprites)
//                {
//                    hero.SetActive(false);
//                }
//                plantHero.SetActive(true);
//                break;

//            case 6:
//                foreach (GameObject hero in heroSprites)
//                {
//                    hero.SetActive(false);
//                }
//                spaceSoldier.SetActive(true);
//                break;