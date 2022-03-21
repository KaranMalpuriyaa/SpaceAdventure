using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Panals")]
    public GameObject gameOverPannal;
    public GameObject startPannal;
    public GameObject pausePannal;

    [Space(2)]
    [Header ("Avoid scene")]
    public TMP_Text scoreText;
    public TMP_Text gameOverscoreText;
    public TMP_Text bestScoreText;

    [Space (2)]
    [Header ("Collecting start scene")]
    public TMP_Text coinText;
    public TMP_Text gameOvercoinText;
    public TMP_Text bestcoinText;

    [Space (2)]
    [Header ("Diffend roket scene")]
    public TMP_Text surviveTime;
    public TMP_Text gameOverSurviveTime;
    public TMP_Text bestSurviveTime;

    [Space (2)]
    [Header ("Shoot meteor scene")]
    public TMP_Text meteorScore;
    public TMP_Text gameOverMeteorScore;
    public TMP_Text bestMeteorScore;


    public static int score;
    public static int coin;
    public static int surviveTimmer;
    public static int meteorKillAmount;


    [Space (20)]
    public Button pausePannalButton;
    public Button resumeButton;
    public Button exitButton;

    public static bool gameOver { get; private set; }
    public static bool firstTap { get; private set; }
    

    // settings bool
    public static bool touchSettings;
    public static bool accSettings;

    public static bool settings;

    private Button[] btns;
    private AudioManager audioManager;
    

    

    private void Awake () {
        InniBtn ();     
    }

    private void Start () {

        AdManager.instance.RequestInterstitial ();

        if(bestScoreText != null) {
            bestScoreText.text = PlayerPrefs.GetInt ("BestScore").ToString ();
        }

        if(bestcoinText != null) {
            bestcoinText.text = PlayerPrefs.GetInt ("BestCoinScore").ToString ();
        }

        if(bestSurviveTime != null) {
            bestSurviveTime.text = PlayerPrefs.GetInt ("BestTimeScore").ToString ();
        }

        if(bestMeteorScore != null) {
            bestMeteorScore.text = PlayerPrefs.GetInt ("BestMeteorScore").ToString ();
        }

        pausePannal.SetActive (false);
        gameOverPannal.SetActive (false);
       
        FindObjectOfType<Player> ().onGameOver += GameOver;
         
        pausePannalButton.onClick.AddListener (() => PausePannalOpen ());
        resumeButton.onClick.AddListener (() => Resume ());
        exitButton.onClick.AddListener (() => BacktoMenu ());      
    }

    private void Update () {

        
        getStartPannal ();

        if(gameOver) {

            if(Input.GetMouseButtonDown (0)) {

                FindObjectOfType<AudioManager> ().Play ("btn");
                SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
                gameOver = false;

            }
        }

        if(Input.GetMouseButtonDown (0)) {
            FindObjectOfType<AudioManager> ().Play ("btn");
            firstTap = true;
        }

        if(gameOver == false) {

            if(scoreText != null)
                scoreText.text = score.ToString ();

            if(coinText != null)
                coinText.text = coin.ToString ();

            if(surviveTime != null)
                surviveTime.text = surviveTimmer.ToString ();

            if(meteorScore != null)
                meteorScore.text = meteorKillAmount.ToString ();
        }

    }

    public void GameOver() {
        
        gameOver = true;

        if(gameOverscoreText != null)
            gameOverscoreText.text = score.ToString ();

        if(gameOvercoinText != null)
            gameOvercoinText.text = coin.ToString ();

        if(gameOverSurviveTime != null)
            gameOverSurviveTime.text = surviveTimmer.ToString ();

        if(gameOverMeteorScore != null)
            gameOverMeteorScore.text = meteorKillAmount.ToString ();

        if(bestScoreText != null) {
            if(score > PlayerPrefs.GetInt ("BestScore", 0)) {
                PlayerPrefs.SetInt ("BestScore", score);
                bestScoreText.text = score.ToString ();
            }
        }

        if(bestcoinText != null) {
            if(coin > PlayerPrefs.GetInt ("BestCoinScore", 0)) {
                PlayerPrefs.SetInt ("BestCoinScore", coin);
                bestcoinText.text = coin.ToString ();
            }
        }

        if(bestSurviveTime != null) {
            if(surviveTimmer > PlayerPrefs.GetInt ("BestTimeScore", 0)) {
                PlayerPrefs.SetInt ("BestTimeScore", surviveTimmer);
                bestSurviveTime.text = surviveTimmer.ToString ();
            }
        }

        if(bestMeteorScore != null) {
            if(meteorKillAmount > PlayerPrefs.GetInt ("BestMeteorScore", 0)) {
                PlayerPrefs.SetInt ("BestMeteorScore", meteorKillAmount);
                bestMeteorScore.text = meteorKillAmount.ToString ();
            }
        }

        gameOverPannal.SetActive (true);
        

        score = 0;
        coin = 0;
        surviveTimmer = 0;
        meteorKillAmount = 0;

        int random = Random.Range (0, 6);
        if(random == 0) {
            AdManager.instance.ShowInterstitial ();
            //print ("show intersitial");
        }
        
        if(random == 3) {
            AdManager.instance.ShowRewardBasedVideo ();
        }

    }

    public void BacktoMenu() {

        Time.timeScale = 1;
        audioManager.Stop ("Game");
        audioManager.Play ("Theme");
        UIManager.selectModes = true;
        UIManager.mainMenu = false;
        UIManager.isAbout = false;
        UIManager.settings = false;
        SceneManager.LoadScene (0);
    }

    public void PausePannalOpen() {
        pausePannal.SetActive (true);
        Time.timeScale = 0;
    }

    public void Resume() {
        Time.timeScale = 1;
        pausePannal.SetActive (false);
    }

    public GameObject getStartPannal() {
        if(firstTap == false) {
            startPannal.SetActive (true);
        }
        else {
            startPannal.SetActive (false);
        }
        return startPannal;
    }

    public void InniBtn() {

        btns = FindObjectsOfType<Button> ();
        audioManager = FindObjectOfType<AudioManager> ();

        foreach(Button btn in btns) {
            btn.onClick.AddListener (() => audioManager.Play ("btn"));    
        }   
    }
}
