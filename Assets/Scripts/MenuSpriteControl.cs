using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpriteControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = PlayerProfile.Instance.CurrentEditingCharacter.characterSprite;
    }
}
