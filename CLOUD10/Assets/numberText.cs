using UnityEngine;
using System.Collections;

public class numberText : MonoBehaviour {
    private Transform parent;
    private int number;
    // Use this for initialization
    void Start()
    {
        number = Random.Range(1, 5);
        parent = transform.parent;

        Renderer parentRenderer = parent.GetComponent<Renderer>();
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingLayerID = parentRenderer.sortingLayerID;
        renderer.sortingOrder = parentRenderer.sortingOrder;

        Transform spriteTransform = parent.transform;
        TextMesh text = GetComponent<TextMesh>();
        Vector3 pos = new Vector3(spriteTransform.position.x, spriteTransform.position.y + 20f, spriteTransform.position.z);
        transform.position = pos;

        text.text = number.ToString();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
