using UnityEngine;
using System.Collections;

public class endGame : MonoBehaviour {
    private GameObject player;
    public GameObject winText;
    public GameObject timerText;
    public GameObject tooLowText;
    public GameObject tooHighText;
    public GameObject Canvas;
    private bool won = false;
    private bool lost = false;

    private int loseNumberNeg;
    private int loseNumberPos;

    private bool stopIns = false;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
        loseNumberNeg = player.GetComponent<move>().loseNumberNeg;
        loseNumberPos = player.GetComponent<move>().loseNumberPos;
	}
	
	// Update is called once per frame
	void Update () {
        won = player.GetComponent<move>().win;
        lost = player.GetComponent<move>().lose;
        int playerNumber = player.GetComponent<move>().number;

        if (stopIns == false)
        {
            if (won)
            {
                instantiateText(winText);
                instantiateText(timerText);
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
                stopIns = true;
            }
        }
	}

    void instantiateText(GameObject text)
    {
        GameObject instantiatedText = Instantiate(text);
        instantiatedText.transform.SetParent(Canvas.transform, false);
    }
}
