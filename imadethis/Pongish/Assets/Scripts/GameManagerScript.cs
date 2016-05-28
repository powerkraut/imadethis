using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

    int playerOneScore, playerTwoScore;
    [SerializeField]
    BallScript gameBall;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    AudioClip goalScored;
    [SerializeField]
    AudioClip endGame;
    AudioSource audSource;
    [SerializeField]
    GameObject endGameScreen;
    CameraShakeScript camShake;

    public void StartNewGame()
    {
        playerOneScore = 0;
        playerTwoScore = 0;
        UpdateScoreText();
        endGameScreen.SetActive(false);
        gameBall.Reset();
    }
	// Use this for initialization
	void Start () {
        audSource = GetComponent<AudioSource>();
        camShake = GetComponent<CameraShakeScript>();
        StartNewGame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void UpdateScoreText()
    {
        scoreText.text = "Player One " + playerOneScore.ToString() + " _ " + playerTwoScore.ToString() + " Player Two";
    }

    public void GoalScored(int playerNumber)
    {
        camShake.Shake();
        PlaySound(goalScored);

        if(playerNumber == 1)
        {
            playerOneScore += 1;
        }
        else if(playerNumber == 2)
        {
            playerTwoScore += 1;
        }

        if(playerOneScore == 3)
        {
            GameOver(1);
        }
        else if(playerTwoScore == 3)
        {
            GameOver(2);
        }
        UpdateScoreText();
    }

    void GameOver(int winner)
    {
        gameBall.Stop();
        endGameScreen.SetActive(true);
        PlaySound(endGame);
    }

    void PlaySound(AudioClip soundClip)
    {
        audSource.clip = soundClip;
        audSource.Play();
    }
}
