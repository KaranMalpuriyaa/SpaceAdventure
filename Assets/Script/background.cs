using UnityEngine;

public class background : MonoBehaviour {

    float backgroundMoveSpeed;
    public Vector2 speedMinMax; // difficulty manager take the min speen to max speed when  difficulty time is over time is done
    Vector2 offset;
    public float slowSpeed = 0.5f; // background move speed when player not tap the screen

    
    public void Update () {

        if(GameManager.firstTap) {
            if(Player.isMoving) {
                backgroundMoveSpeed = Mathf.Lerp (speedMinMax.y, speedMinMax.x, DifficultyManager.GetSecondsToMaxDifficulty ());
                offset = new Vector2 (0, Time.time * backgroundMoveSpeed);
                GetComponent<Renderer> ().material.mainTextureOffset = offset;
            }
            else {
                offset = new Vector2 (0, Time.time * slowSpeed);
                GetComponent<Renderer> ().material.mainTextureOffset = offset;
            }
            
        }
        
    }
}
