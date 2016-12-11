using UnityEngine;
using System.Collections;

public class getMesh : MonoBehaviour {

    public GameObject numberMesh;
    // Use this for initialization
    void Start()
    {
        numberMesh.GetComponent<Renderer>().sortingLayerName = "Player";
        numberMesh.GetComponent<Renderer>().sortingOrder = 1;
        numberMesh.GetComponent<TextMesh>().text = GetComponent<move>().number.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        numberMesh.GetComponent<TextMesh>().text = GetComponent<move>().number.ToString();
        numberMesh.transform.position = transform.position;
    }
}
