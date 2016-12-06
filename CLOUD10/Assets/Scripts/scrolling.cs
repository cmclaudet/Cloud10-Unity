using UnityEngine;
using System.Collections;

public class scrolling : MonoBehaviour {

    public float speed = 0.4f;

    // Use this for initialization
    void Start()
    {
 //       GetComponent<Renderer>().sortingLayerName = "Default";
//        GetComponent<Renderer>().sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
