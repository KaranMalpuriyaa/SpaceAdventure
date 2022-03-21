using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {

    public event System.Action onGameOver;
   
    Rigidbody2D rb;
    Camera mainCamera;
    ParticleSystem ps;
    AudioManager audioManager;
    
    public float speedX;
    public float accSpeed;

    public GameObject destroyEffect;
    public GameObject coinEffect;
    public GameObject scoreShower;


    public static bool isMoving;
    Vector2 pos;
    float screenWidth;

    float nextTime;

    private void Start () {

        ps = GetComponentInChildren<ParticleSystem> ();
        audioManager = FindObjectOfType<AudioManager> ();
        rb = GetComponent<Rigidbody2D> ();
        mainCamera = Camera.main;
        pos.y = transform.position.y;
        screenWidth = Camera.main.aspect * Camera.main.orthographicSize -transform.localScale.x;
    }


    private void Update () {
        
        if(GameManager.firstTap == true) {

            if(isMoving) {
                float timmer = 0.05f;
                if(Time.time > nextTime) {
                    GameManager.score += 2;
                    nextTime = Time.time + timmer;
                }
            }
            else {
                float timmer = 0.5f;
                if(Time.time > nextTime) {
                    GameManager.score += 2;
                    nextTime = Time.time + timmer;
                }
            }
            
        }
        

        isMoving = Input.GetMouseButton (0);

        if(Input.GetMouseButtonDown(0)) {
            FindObjectOfType<AudioManager> ().Play ("Roket");
        }
        else if(Input.GetMouseButtonUp (0)) {
            FindObjectOfType<AudioManager> ().Stop ("Roket");
        }

        if(isMoving) {
            
            if(GameManager.touchSettings) {
                pos.x = mainCamera.ScreenToWorldPoint (Input.mousePosition).x;
            }
            if(GameManager.accSettings) {
                pos.x = Input.acceleration.x;
            }

            // partical system
            ps.Play ();

            if(pos.x > 2) {
                ps.transform.eulerAngles = new Vector3 (96f, 90f, 90f);
            }
            else if(pos.x < -2) {
                ps.transform.eulerAngles = new Vector3 (84f, 90f, 90f);
            }
            else {
                ps.transform.eulerAngles = new Vector3 (90f, 90f, 90f);
            }           
        }
        else {
            ps.Stop ();
        }

        if(this.gameObject != null) {
            if(transform.position.x > screenWidth + 1 || transform.position.x < -screenWidth - 1) {
                //print ("destroy the player");
                audioManager.Play ("Des");
                audioManager.Stop ("Roket");

                Destroy (this.gameObject);
                GameObject desParticle = Instantiate (destroyEffect, transform.position, Quaternion.identity);
                Destroy (desParticle, 2f);

                if(onGameOver != null) {
                    onGameOver ();
                }
            }
        }
    }

    private void FixedUpdate () {

        if(isMoving) {
            if(GameManager.touchSettings) {
                rb.MovePosition (Vector2.Lerp (rb.position, pos, speedX * Time.fixedDeltaTime));
            }
            else if(GameManager.accSettings) {
                Vector2 move = new Vector2 (pos.x * accSpeed * Time.fixedDeltaTime, 0);
                pos.x = Mathf.Clamp(pos.x, -screenWidth, screenWidth);
                Vector2 moveDir = move.normalized;
                rb.MovePosition (rb.position + move);                
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {

        if(other.gameObject.tag == "block") {
            audioManager.Play ("Des");
            audioManager.Stop ("Roket");

            Destroy (this.gameObject);
            GameObject desParticle = Instantiate (destroyEffect, transform.position, Quaternion.identity);
            Destroy (desParticle, 2f);
            
            if(onGameOver != null) {
                onGameOver ();
            }

        }

        if(other.gameObject.tag == "Coin") {
            audioManager.Play ("Coin");
            Destroy (other.gameObject);
            GameObject coinEf = Instantiate (coinEffect, other.transform.position + Vector3.down * 1f, Quaternion.identity);
            Destroy (coinEf, 2f);

            int amount = Random.Range (10, 50);
            GameManager.coin += amount;

            if(scoreShower != null) {
                GameObject Newscore = Instantiate (scoreShower, other.transform.position + Vector3.up * 1.5f, Quaternion.identity);
                Newscore.GetComponentInChildren<TextMeshPro> ().text = "+" + amount;
                Destroy (Newscore, 2f);               
            }
        }
    }
}
