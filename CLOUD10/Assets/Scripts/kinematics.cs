using UnityEngine;
using System.Collections;

public class kinematics : MonoBehaviour {
    public float maxSpeed = 5.0f;
    public float minSpeed = 2.0f;

    private int seed = (int)System.DateTime.Now.Ticks;
    private float bodyRadius;
    private Vector3 worldScreenDim;

	// Use this for initialization
	void Awake () {
        //Find radius of body
        bodyRadius = 0;
        Random.InitState(seed);

        //Get screen dimensions with respect to world co-ordinates
        Vector3 pixelScreenDim = new Vector3(Screen.width, Screen.height, 0.0f);
        worldScreenDim = Camera.main.ScreenToWorldPoint(pixelScreenDim);

        //random number to generate which side pickup will spawn
        //1: left, 2: right, 3: top, 4: bottom
        int whichSide = Random.Range(0, 4);

        //Set position
        Vector3 position = genPos(whichSide);
        transform.position = position;

        //Set velocity
        Vector3 direction = genDir(whichSide, position);
        float speed = Random.Range(minSpeed, maxSpeed);
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
 
    /*Function generates a random position for body outside one of the four possible edges.
    Random number between 1 and 4 to represent the four edges is generated.
    For each edge random position along the edge is generated in posList.*/
    Vector3 genPos(int side)
    {
        //offsets defined as the distance between screen centre ([0,0,0] in world space) and edges
        float yOffset = worldScreenDim.y + bodyRadius;
        float xOffset = worldScreenDim.x + bodyRadius;

        float[,] posList = 
            new float[4, 2] { {-xOffset, Random.Range(-yOffset, yOffset) },   //left
                              {xOffset, Random.Range(-yOffset, yOffset) },    //right
                              {Random.Range(-xOffset, xOffset), -yOffset },   //bottom
                              {Random.Range(-xOffset, xOffset), yOffset } };  //top

        float xpos = posList[side, 0];
        float ypos = posList[side, 1];
        Vector3 position = new Vector3(xpos, ypos, 0.0f);
        return position;
    }
    
    /*Generates direction of movement based on which side cloud comes from and where along the side it spawns.
    Written such that clouds will always move inwards: upper and lower bounds for direction change with cloud's position*/
    Vector2 genDir(int side, Vector3 position)
    {   
        //First bound for angle of movement for each side
        float[] angleBounds =
            new float[4] { 45 + 45*position.y/(worldScreenDim.y),   //left 
                           -135 - 45*position.y/worldScreenDim.y,   //right
                           -45 - 45*position.x/worldScreenDim.x,    //bottom
                           135 + 45*position.x/worldScreenDim.x};   //top

        float bound1 = angleBounds[side];
        float bound2 = bound1 + 90; //second bound always 90 degrees more

        float angDeg = Random.Range(bound1, bound2); //Actual movement angle must lie somewhere between bounds
        float angRad = angDeg * Mathf.PI / 180.0f;

        //Get x,y direction from cloud's movement angle
        float dirx = Mathf.Sin(angRad);
        float diry = Mathf.Cos(angRad);
        Vector3 direction = new Vector3(dirx, diry);

        return direction;
    }
}
