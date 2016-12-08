using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class endGame : MonoBehaviour {
    private GameObject player;
    public Text playerScore;
    public GameObject winText;
    public GameObject timerText;
    public GameObject tooLowText;
    public GameObject tooHighText;
    public GameObject playAgainButton;
    public GameObject Canvas;
    private bool won = false;
    private bool lost = false;

    private int loseNumberNeg;
    private int loseNumberPos;

    private bool stopIns = false;

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
        timePassed += Time.deltaTime;

        won = player.GetComponent<move>().win;
        lost = player.GetComponent<move>().lose;
        int playerNumber = player.GetComponent<move>().number;

        if (stopIns == false)
        {
            if (won)
            {
                instantiateText(winText);
                instantiateText(timerText);
                instantiateText(playAgainButton);
                instantiateScore();
                stopIns = true;
            }
            if (lost)
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

    void instantiateText(GameObject text)
    {
        GameObject instantiatedText = Instantiate(text);
        instantiatedText.transform.SetParent(Canvas.transform, false);
    }

    void instantiateScore()
    {
        Text instantiatedScore = Instantiate(playerScore);
        instantiatedScore.transform.SetParent(Canvas.transform, false);
        int secondsPassed = Mathf.FloorToInt(timePassed);
        instantiatedScore.GetComponent<Text>().text = secondsPassed.ToString();
    }

    public void restart()
    {
        SceneManager.LoadScene("title");
    }
}
