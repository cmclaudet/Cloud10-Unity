using UnityEngine;
using System.Collections;

public class manageEntities : MonoBehaviour {
    public int maxPickupNumber = 10;
    public int maxEvilNumber = 20;
    public int maxGoldNumber = 2;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        inactivate("Pickup", "PickupText", maxPickupNumber);
        inactivate("Evil", "EvilText", maxEvilNumber);
        inactivateGold();
	}

    void inactivate(string bodyTag, string textTag, int maxNum)
    {
        GameObject[] allBodies = GameObject.FindGameObjectsWithTag(bodyTag);
        GameObject[] allBodyText = GameObject.FindGameObjectsWithTag(textTag);
        if (allBodies.Length > maxNum)
        {
            allBodies[0].gameObject.SetActive(false);
            allBodyText[0].gameObject.SetActive(false);
        }
    }

    void inactivateGold()
    {
        GameObject[] allGold = GameObject.FindGameObjectsWithTag("Gold");
        if (allGold.Length > maxGoldNumber)
        {
            allGold[0].gameObject.SetActive(false);
        }
    }
}
