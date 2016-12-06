using UnityEngine;
using System.Collections;

public class createPickups : MonoBehaviour
{
    public Rigidbody2D pickup;
    public float spawnFrequency = 2;
    public int layerOrder;
    private float timeSinceSpawn = 0;

    // Use this for initialization
    void Start()
    {
        layerOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;

        //spawn new pickup every 2 seconds
        if (timeSinceSpawn > spawnFrequency)
        {
            Rigidbody2D newPickup = Instantiate(pickup);
            newPickup.GetComponent<SpriteRenderer>().sortingOrder = 20; //set draw order for pickup
            Transform gameObjChild = newPickup.GetComponentInChildren<Transform>(); //find text mesh component on child
            Renderer childRenderer = gameObjChild.GetComponent<Renderer>();     //find text mesh renderer
            childRenderer.sortingOrder = 1;    //set draw order for text on pickup. Must be 1 above so it can be seen over the pickup.

            Debug.Log("Text sorting order: ");
            Debug.Log(childRenderer.sortingOrder);

            layerOrder += 2;
            timeSinceSpawn = 0;
        }
    }
}
