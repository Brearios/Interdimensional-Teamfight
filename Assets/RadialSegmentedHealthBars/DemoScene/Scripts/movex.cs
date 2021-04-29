using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movex : MonoBehaviour {

    public float speed = 1;
    public float yspeed = 1;
    private float oldY = 0;

    private void Start() {
        oldY = transform.position.y;
    }
    void Update() {
        var pos = transform.position;
        pos.x += speed * Time.deltaTime;
        pos.y = oldY + Mathf.Sin(Time.timeSinceLevelLoad * yspeed);
        transform.position = pos;
    }
}