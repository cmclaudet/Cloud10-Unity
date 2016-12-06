using UnityEngine;
using System.Collections;

public class sortevil : MonoBehaviour {
    public int number;
    // Use this for initialization
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = "Evils";

        number = Random.Range(-1, -5);
        TextMesh text = GetComponent<TextMesh>();
        text.text = "-";

        transform.Translate(0, 0.3f, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
