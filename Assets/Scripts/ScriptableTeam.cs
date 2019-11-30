using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Team", menuName = "Team")]
public class ScriptableTeam : MonoBehaviour
{
    
    public enum Team { Neutral, Blue, Red }; // Green, Purple, Orange
    public Color color;
}
