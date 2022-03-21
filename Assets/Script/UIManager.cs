using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {


    [SerializeField] private GameObject mainMenuPannal;
    [SerializeField] private GameObject selectModesPannal;
    [SerializeField] private GameObject aboutPannal;
    [SerializeField] private GameObject settingsPannal;

    [SerializeField] public static bool mainMenu, selectModes, isAbout, settings;

    public Image touchImage, accImage;

    private Button[] btns;
    private AudioManager audioManager;

    public static UIManager instance;
    
    
    private void Awake () {

        mainMenuPannal.SetActive (true);
        selectModesPannal.SetActive (true);
        aboutPannal.SetActive (true);
        settingsPannal.SetActive (true);

        InniBtn ();

        if(instance == null) {
            instance = this;
        }  
    }

    public void Start () {

        FindObjectOfType<AudioManager>().Play("Theme");

        if(!PlayerPrefs.HasKey("Acc")) {
            PlayerPrefs.SetInt ("Acc", 0);
            GameManager.accSettings = PlayerPrefs.GetInt ("Acc") == 1;
            SaveControllSettings ();
        }
    
        SaveControllSettings ();
        
        mainMenu = true;
        selectModes = true;
        isAbout = false;
        settings = false;
        Initialize ();        
    }

    private void Update () {

        getReturnTouchImage ();
        getReturnAccImage ();
        Initialize ();
    }

    public void Initialize() {

        if(mainMenu) {
            mainMenuPannal.SetActive (true);
            
            selectModes = false;
            isAbout = false;
            settings = false;
        }
        else {
            mainMenuPannal.SetActive (false);
        }

        if(selectModes) {
            selectModesPannal.SetActive (true);
            mainMenu = false;
            isAbout = false;
            settings = false;
        }
        else {
            selectModesPannal.SetActive (false);
        }

        if(isAbout) {
            aboutPannal.SetActive (true);
            mainMenu = false;
            selectModes = false;
            settings = false;
        }
        else {
            aboutPannal.SetActive (false);
        }

        if(settings) {
            settingsPannal.SetActive (true);
            mainMenu = false;
            selectModes = false;
            isAbout = false;
        }
        else {
            settingsPannal.SetActive (false);
        }
    }

    public void PlayBtn() {

        selectModes = true;
        mainMenu = false;
        isAbout = false;
        settings = false;
    }
    
    public void About () {
        isAbout = true;
        selectModes = false;
        mainMenu = false;
        settings = false;
    }

    public void Settings () {
        settings = true;
        isAbout = false;
        selectModes = false;
        mainMenu = false;
    }

    public void LoadScene (string name) {

        FindObjectOfType<AudioManager> ().Stop ("Theme");
        FindObjectOfType<AudioManager> ().Play ("Game");
        SceneManager.LoadScene (name);
    }
    
    public void SelectModeBack() {

        selectModes = false;
        mainMenu = true;
    }

    public void AboutBack () {

        isAbout = false;
        mainMenu = true;
    }

    public void SettingsBack () {

        settings = false;
        mainMenu = true;
    }

    public void TouchSetting () {

        GameManager.accSettings = false;
        GameManager.touchSettings = true;

        PlayerPrefs.SetInt ("Touch", 1);
        PlayerPrefs.SetInt ("Acc", 0);
    }
    public void AccSetting () {

        GameManager.touchSettings = false;
        GameManager.accSettings = true;

        PlayerPrefs.SetInt ("Touch", 0);
        PlayerPrefs.SetInt ("Acc", 1);
    }

    // return image when you select the special image
    public Image getReturnTouchImage() {
        if(GameManager.touchSettings) {
            GameManager.accSettings = false;
            touchImage.enabled = true;
        }
        else {
            touchImage.enabled = false;
        }
        return touchImage;
    }
    public Image getReturnAccImage () {

        if(GameManager.accSettings) {
            GameManager.touchSettings = false;
            accImage.enabled = true;
        }
        else {
            accImage.enabled = false;
        }
        return accImage;
    }

    public void InniBtn () {

        btns = FindObjectsOfType<Button> ();
        audioManager = FindObjectOfType<AudioManager> ();

        foreach(Button btn in btns) {
            btn.onClick.AddListener (() => audioManager.Play ("btn"));            
        }       
    }

    public void SaveControllSettings() {

        if(PlayerPrefs.GetInt ("Touch") == 0) {
            GameManager.touchSettings = false;
        }
        else if(PlayerPrefs.GetInt ("Touch") == 1) {
            GameManager.touchSettings = true;
        }

        if(PlayerPrefs.GetInt ("Acc") == 0) {
            GameManager.accSettings = false;
        }
        else if(PlayerPrefs.GetInt ("Acc") == 1) {
            GameManager.accSettings = true;
        }

        if(GameManager.touchSettings) {
            PlayerPrefs.SetInt ("Touch", 1);
            PlayerPrefs.SetInt ("Acc", 0);
        }
        else {
            PlayerPrefs.SetInt ("Touch", 0);
            PlayerPrefs.SetInt ("Acc", 1);
        }
        if(GameManager.accSettings) {
            PlayerPrefs.SetInt ("Acc", 1);
            PlayerPrefs.SetInt ("Touch", 0);
        }
        else {
            PlayerPrefs.SetInt ("Acc", 0);
            PlayerPrefs.SetInt ("Touch", 1);
        }
    }

    public void Youtube() {
        // Youtube url
        Application.OpenURL ("https://www.youtube.com/channel/UCg3IrNKR_hdObOlNBxH8Xfg");
    }
    public void Instagram () {
        // Instagram url 
        Application.OpenURL ("https://www.instagram.com/lookdevorignal/");
    }

    public void RateUs() {
        #if UNITY_ANDROID
        Application.OpenURL ("market://details?id=com.LookDev.SpaceAdventures");
        #endif
    }

    public void DeletePlayerPrefs() {
        PlayerPrefs.DeleteAll ();
    }










}
