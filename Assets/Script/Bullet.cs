using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject blockDestroyEffect; // destroy partical effect
    private AudioManager audioManager;

    private void Start () {
        audioManager = FindObjectOfType<AudioManager> ();
    }

    private void OnTriggerEnter2D (Collider2D other) {

        if(other.gameObject.tag == "block") {
            Destroy (other.gameObject, 0.01f);
            Destroy (this.gameObject, 0.01f);
            GameManager.meteorKillAmount += 10;
            audioManager.Play ("Des");
            GameObject blockEf = Instantiate (blockDestroyEffect, other.transform.position, Quaternion.identity);
            Destroy (blockEf, 2f);
        }
    }
}
