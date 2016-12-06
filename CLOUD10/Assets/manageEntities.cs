using UnityEngine;
using System.Collections;

public class manageEntities : MonoBehaviour {
    public int maxPickupNumber = 10;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        GameObject[] allPickups = GameObject.FindGameObjectsWithTag("Pickup");
        GameObject[] allPickupText = GameObject.FindGameObjectsWithTag("PickupText");
        if (allPickups.Length > maxPickupNumber)
        {
            allPickups[0].gameObject.SetActive(false);
            allPickupText[0].gameObject.SetActive(false);
        }
	}
}
