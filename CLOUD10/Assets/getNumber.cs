using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class getNumber : MonoBehaviour {
    public Text numberText;
//    public Camera theCamera;
    private Vector3 playerPos;
    private RectTransform rt;
    private Vector3 playerScreenPos;
    private int playerNum;

	// Use this for initialization
	void Start () {
        playerNum = GetComponent<move>().number;
        numberText.GetComponent<Text>().text = playerNum.ToString();
        playerPos = transform.position;
        rt = numberText.GetComponent<RectTransform>();
        playerScreenPos = Camera.main.WorldToViewportPoint(transform.TransformPoint(playerPos));
        rt.anchorMax = playerScreenPos;
        rt.anchorMin = playerScreenPos;
	}
	
	// Update is called once per frame
	void Update () {
        playerScreenPos = Camera.main.WorldToViewportPoint(transform.TransformPoint(playerPos));
        rt.anchorMax = playerScreenPos;
        rt.anchorMin = playerScreenPos;
    }
}
