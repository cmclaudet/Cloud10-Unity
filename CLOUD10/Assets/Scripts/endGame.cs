using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class endGame : MonoBehaviour {
    /*Places all UI elements when player gets to 10
      Restarts game when play taps play again button */

    private GameObject player;
    //All UI gameobject prefabs
    public Text playerScore;
    public GameObject winText;
    public GameObject timerText;
    public GameObject tooLowText;
    public GameObject tooHighText;
    public GameObject playAgainButton;
    public GameObject Canvas;

    //if either of these values become message appears telling player if they have won or lost
    private bool won = false;
    private bool lost = false;
    
    //These values are set to maximum and minimum values player can get to before losing
    private int loseNumberNeg;
    private int loseNumberPos;

    //stopIns ensures UI elements do not instantiate more than once
    private bool stopIns = false;

    //Timer to show player how long they took to win
    private float timePassed;

	// Use this for initialization
	void Start () {
        timePassed = 0;
        player = GameObject.Find("player");
        loseNumberNeg = player.GetComponent<move>().loseNumberNeg;
        loseNumberPos = player.GetComponent<move>().loseNumberPos;
	}
	
	// Update is called once per frame
	void Update () {
        //Update timer
        timePassed += Time.deltaTime;

        //Update win/lose value
        won = player.GetComponent<move>().win;
        lost = player.GetComponent<move>().lose;
        int playerNumber = player.GetComponent<move>().number;

        if (stopIns == false)
        {
            if (won)
            //Bring up winning UI
            {
                instantiateText(winText);
                instantiateText(timerText);
                instantiateText(playAgainButton);
                instantiateScore();
                stopIns = true;
            }
            if (lost)
            //Checks if player has gone too high or too low
            {
                if (playerNumber < loseNumberNeg)
                {
                    instantiateText(tooLowText);
                }
                else
                {
                    instantiateText(tooHighText);
                }
                instantiateText(playAgainButton);
                stopIns = true;
            }
        }
	}

    //Instantiates text and sets Canvas as parent
    void instantiateText(GameObject text)
    {
        GameObject instantiatedText = Instantiate(text);
        instantiatedText.transform.SetParent(Canvas.transform, false);
    }

    //Instantiates score by rounding time passed to nearest second
    void instantiateScore()
    {
        Text instantiatedScore = Instantiate(playerScore);
        instantiatedScore.transform.SetParent(Canvas.transform, false);
        int secondsPassed = Mathf.FloorToInt(timePassed);
        instantiatedScore.GetComponent<Text>().text = secondsPassed.ToString();
    }

    public void restart()
    {
        SceneManager.LoadScene("main");
    }
}
