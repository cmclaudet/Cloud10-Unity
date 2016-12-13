using UnityEngine;
using System.Collections;

public class sortevil : MonoBehaviour {
    //generates number for storm cloud
    //sets sorting layer for storm cloud
    //displays sign number to ensure player does not know exact storm cloud number
    public int number;
    // Use this for initialization
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = "Evils";

        number = Random.Range(-1, -5);
        
        TextMesh text = GetComponent<TextMesh>();
        text.text = "-";

        GameObject player = GameObject.Find("player");
        if (player.GetComponent<move>().number > 10)
        {
            number = -number;
            text.text = "+";
        }

        transform.Translate(-0.1f, 0.2f, 0);
    }
    
}
