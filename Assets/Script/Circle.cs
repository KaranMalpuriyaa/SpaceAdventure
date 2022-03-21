using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

    Camera mainCam;
    Rigidbody2D rb;

    float nextTime;

    private void Start () {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D> ();
    }

    private void Update () {

        Vector3 targetPos = mainCam.ScreenToWorldPoint (Input.mousePosition);
        targetPos.z = 0;
        rb.position = targetPos;

        if(GameManager.firstTap == true) {

            float sur = 0.1f;
            if(Time.time > nextTime) {
                GameManager.surviveTimmer += 2;
                nextTime = Time.time + sur;
            }
        }
            

    }

    private void OnCollisionEnter2D (Collision2D other) {
        if(other.gameObject.tag == "block") {
            other.gameObject.GetComponent<fallDown> ().enabled = false;
            Destroy (other.gameObject, 5f);
        }
    }
}