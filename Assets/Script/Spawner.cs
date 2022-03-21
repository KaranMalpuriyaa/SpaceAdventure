using UnityEngine;

public class Spawner : MonoBehaviour {
  
    public GameObject[] blockPerfab;
    Camera mainCamera;
    Player player;

    Vector2 screenWidthHeight;
    float timeBtwSpawn;
    float nextSpawnTime;

    public Vector2 blockSizeMinMax;
    public Vector2 timeBtwSpawnMinMax;
    public float blockRotMax;


    private void Start () {      
        mainCamera = Camera.main;
        screenWidthHeight = new Vector2 (mainCamera.aspect * mainCamera.orthographicSize, mainCamera.orthographicSize);
    }

    private void Update () {

        if(GameManager.firstTap) {
            if(Time.time > nextSpawnTime) {
                timeBtwSpawn = Mathf.Lerp (timeBtwSpawnMinMax.y, timeBtwSpawnMinMax.x, DifficultyManager.GetSecondsToMaxDifficulty ());
                nextSpawnTime = Time.time + timeBtwSpawn;

                // random block size
                float blockSize = Random.Range (blockSizeMinMax.x, blockSizeMinMax.y);
                // random x spawn pos
                Vector2 screenWidthHeightMinMax = new Vector2 (Random.Range (-screenWidthHeight.x, screenWidthHeight.x), screenWidthHeight.y + blockSize);
                float blockAngle = Random.Range (-blockRotMax, blockRotMax);
                int blockIndex = Random.Range (0, blockPerfab.Length);
                GameObject newBlock = Instantiate (blockPerfab[blockIndex], screenWidthHeightMinMax, Quaternion.Euler (Vector3.forward * blockAngle));
                newBlock.transform.localScale = Vector3.one * blockSize;
                Destroy (newBlock, 10f);
                // new time 
            }
        }         
    }
}