using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class followBody : MonoBehaviour {
    public GameObject player;
    public Camera theCamera;
    private Vector3 playerPos;
    private RectTransform rt;
    private Vector3 playerScreenPos;
    private int playerNum;

	// Use this for initialization
	void Start () {
        playerNum = player.GetComponent<move>().number;
        GetComponent<Text>().text = playerNum.ToString();
        playerPos = player.transform.position;
        rt = GetComponent<RectTransform>();
        playerScreenPos = theCamera.WorldToViewportPoint(player.transform.TransformPoint(playerPos));
        rt.anchorMax = playerScreenPos;
        rt.anchorMin = playerScreenPos;
	}
	
	// Update is called once per frame
	void Update () {
        playerScreenPos = theCamera.WorldToViewportPoint(player.transform.TransformPoint(playerPos));
        rt.anchorMax = playerScreenPos;
        rt.anchorMin = playerScreenPos;
    }
}
