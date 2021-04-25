using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound", menuName = "Sound Data")]
public class ScriptableSoundData : ScriptableObject
{
    public enum SpeakerType { LevelScripting, BossSound, HeroSound, EnemySound, AmbianceSound };
    public enum SoundType { PlotSound, VoiceLine, DeathSound, AbilitySound, AutoAttackSound, LowOther };
    public AudioClip SoundFile;
}
