using UnityEngine;

public class fallDown : MonoBehaviour {

    Rigidbody2D rb;

    float fallSpeed = 10f;
    float screenWidth;

    public Vector2 fallSpeedMinMax;
    

    private void Start () {

        rb = GetComponent<Rigidbody2D> ();

        screenWidth = -Camera.main.orthographicSize - 2; // calculate the screen width
        // incresing fall speed according to the difficulty manager
        fallSpeed = Mathf.Lerp (fallSpeedMinMax.y, fallSpeedMinMax.x, DifficultyManager.GetSecondsToMaxDifficulty ());
    }


    private void Update () {

        // contineusly moving down the block object
        transform.Translate (Vector2.down * fallSpeed * Time.deltaTime, Space.Self);

        // destroy the block when block y position is smaller then screen width
        if(transform.position.y < screenWidth) {         
            Destroy (this.gameObject);
        }
        
    }
}
