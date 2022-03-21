using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject bulletPerfab;
    public Transform firePoint;

    public float bulletForce;
    public float timmer = 0.01f;

    private float nextSpawnTime;
    float nextTime;

    private void Update () {

        if(GameManager.firstTap) {
            float sur = 1f;
            if(Time.time > nextTime) {
                GameManager.surviveTimmer += 1;
                nextTime = Time.time + sur;
            }

            if(Player.isMoving) {
                if(Time.time > nextSpawnTime) {
                    nextSpawnTime = Time.time + timmer;
                    GameObject newBullet = Instantiate (bulletPerfab, firePoint.position, Quaternion.identity);
                    Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D> ();
                    bulletRb.AddForce (Vector2.up * bulletForce, ForceMode2D.Impulse);
                    Destroy (newBullet, 4f);
                }
            }
        }

    }
}