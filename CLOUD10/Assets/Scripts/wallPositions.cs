using UnityEngine;
using System.Collections;

public class wallPositions : MonoBehaviour {
    //sets wall size and position based on screen size
    private BoxCollider2D[] walls;
    private Vector3 worldScreenDim;

	// Use this for initialization
	void Start () {
        walls = GetComponents<BoxCollider2D>();
        Vector3 pixelScreenDim = new Vector3(Screen.width, Screen.height, 0.0f);
        worldScreenDim = Camera.main.ScreenToWorldPoint(pixelScreenDim);

        walls[0].size = new Vector2(2*worldScreenDim.x, 1);
        walls[0].offset = new Vector2(0, worldScreenDim.y);

        walls[1].size = new Vector2(2 * worldScreenDim.x, 1);
        walls[1].offset = new Vector2(0, -worldScreenDim.y);

        walls[2].size = new Vector2(1, 2 * worldScreenDim.y);
        walls[2].offset = new Vector2(worldScreenDim.x, 0);

        walls[3].size = new Vector2(1, 2 * worldScreenDim.y);
        walls[3].offset = new Vector2(-worldScreenDim.x, 0);
    }
	
}
