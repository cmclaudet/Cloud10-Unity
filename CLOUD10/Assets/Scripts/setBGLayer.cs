using UnityEngine;
using System.Collections;

public class setBGLayer : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GetComponent<MeshRenderer>().sortingLayerName = "Background";
	}
}
