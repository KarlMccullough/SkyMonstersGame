using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Monetization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    private string adid = "3455593";
    private string videoad = "video";

    public static MenuManager instance;

    public bool gameOver = false;

    [SerializeField]
    private Text scoreText , coinText, goScoreText, goCoinText, highscoreText;
    [SerializeField]
    private GameObject gameMenu, gameOverMenu;

    private int currentCoin, currentScore, highScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Advertisement.Initialize(adid, true);

        scoreText.text = "" + 0;
        coinText.text = "" + 0;
     
        
    }

    public void Update()
    {
        

        highScore = currentScore;
       
        if (PlayerPrefs.GetInt("score") <= highScore) 
            PlayerPrefs.SetInt("score", highScore);
        
    }

    public void IncreaseCoin()
    {
        currentCoin++;
        coinText.text = "" + currentCoin;
    }
    public void IncreaseScore()
    {
        currentScore++;
        
        scoreText.text = "" + currentScore;
       
    }

    public void GameOver()
    {
        SoundManager.instance.PlayDeath();
        gameOver = true;
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
       
        goCoinText.text = "Coins: " + currentCoin;
        
        goScoreText.text = "Score: " + currentScore;
        
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("score").ToString();

        ADlauncher();
       

    }

    public void HomeBtn()
    {
        SoundManager.instance.PlayClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene(). name);
    }
    public void ResetBtn()
    {
        PlayerPrefs.DeleteKey("score");
    }

    public void ContinueProgress()
    {
        SoundManager.instance.PlayClick();
       
        playON();
    }

    public void playON()
    {
        gameOver = false;
        gameMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        Player.instance.gameObject.SetActive(true);
    }

    void ADlauncher()
    {
        
        if (Advertisement.IsReady("rewardedVideo"))
        {
            
            Advertisement.Show("rewardedVideo");
        }
    }
}
