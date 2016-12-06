using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class genNumber : MonoBehaviour {
    public int number;
    private Text pickupText;

    private RectTransform rt;
    private Vector3 pickupPos;
    private Vector3 pickupScreenPos;

	// Use this for initialization
	void Start () {
        number = Random.Range(1, 5);
        pickupText = GameObject.Find("pickupText").GetComponent<Text>();

        pickupText.text = number.ToString();
        pickupPos = transform.position;
        rt = pickupText.GetComponent<RectTransform>();
        pickupScreenPos = Camera.main.WorldToViewportPoint(transform.TransformPoint(pickupPos));
        rt.anchorMax = pickupScreenPos;
        rt.anchorMin = pickupScreenPos;
	}
	
	// Update is called once per frame
	void Update () {
        pickupScreenPos = Camera.main.WorldToViewportPoint(transform.TransformPoint(pickupPos));
        rt.anchorMax = pickupScreenPos;
        rt.anchorMin = pickupScreenPos;
    }
}
