using UnityEngine;
using System.Collections;

public class sort : MonoBehaviour {
    public int number;
	// Use this for initialization
	void Start () {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = "Pickups";

        number = Random.Range(1, 5);
        GameObject player = GameObject.Find("player");
        if (player.GetComponent<move>().number > 10)
        {
            number = -number;
        }
        TextMesh text = GetComponent<TextMesh>();
        text.text = number.ToString();

        transform.Translate(0, 0.3f, 0);
    }

}
