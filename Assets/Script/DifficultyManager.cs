using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DifficultyManager 
{
    
    public static float secondsToMaxDifficulty = 120f; // time pass when you want your game harder then now

    public static float GetSecondsToMaxDifficulty() {
        return Mathf.Clamp01 (Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}
