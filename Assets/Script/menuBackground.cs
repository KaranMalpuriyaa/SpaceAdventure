using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuBackground : MonoBehaviour {

    float speed;
    public Vector2 speedMinMax;
    Vector2 offset;


    public void Update () {
        speed = Mathf.Lerp (speedMinMax.y, speedMinMax.x, DifficultyManager.GetSecondsToMaxDifficulty ());
        offset = new Vector2 (0, Time.time * speed);
        GetComponent<Renderer> ().material.mainTextureOffset = offset;
    }

        
            

        

    
}