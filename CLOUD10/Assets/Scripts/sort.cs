using UnityEngine;
using System.Collections;

public class sort : MonoBehaviour {
    //generates number of pickup based on player number
    //sets sorting layer to "Pickups"
    public int number;
	// Use this for initialization
	void Start () {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = "Pickups";

        //pickups will have random number between 1 and 5, positive if player is below 10 and negative if not
        number = Random.Range(1, 5);
        GameObject player = GameObject.Find("player");
        if (player.GetComponent<move>().number > 10)
        {
            number = -number;
        }
        TextMesh text = GetComponent<TextMesh>();
        text.text = number.ToString();
        
        //offset to place text in centre of pickup
        transform.Translate(0, 0.3f, 0);
    }

}
