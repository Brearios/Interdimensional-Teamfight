using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xpAvailableText : MonoBehaviour
{
    public static DisplayText Instance;
    public CharacterProfile SceneCharacter;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        SceneCharacter = GameObject.FindObjectOfType<CharacterProfile>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"{SceneCharacter.characterAvailableXP} XP Remaining";
    }
}