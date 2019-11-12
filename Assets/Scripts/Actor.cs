﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int maxHealth;
    public int attackDamage;
    // Later make this a random amount within a range
    public int globalCooldown;
    public string role;
    public string team;
    public float range;
}
