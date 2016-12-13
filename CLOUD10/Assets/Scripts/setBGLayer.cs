using UnityEngine;
using System.Collections;

public class setBGLayer : MonoBehaviour {
    
    //Set sorting layer of background object
	void Awake () {
        GetComponent<MeshRenderer>().sortingLayerName = "Background";
	}
}
