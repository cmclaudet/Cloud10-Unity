using UnityEngine;
using System.Collections;

public class createWholepickup : MonoBehaviour {

    public Rigidbody2D justPickup;
    public GameObject pickupText;
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
            Rigidbody2D newPickup = Instantiate(justPickup);
            newPickup.GetComponent<SpriteRenderer>().sortingOrder = layerOrder; //set draw order for pickup

            GameObject newText = Instantiate(pickupText);
            Renderer textRenderer = newText.GetComponent<Renderer>();     //find text mesh renderer
            textRenderer.sortingOrder = layerOrder + 1;    //set draw order for text on pickup. Must be 1 above so it can be seen over the pickup.

            newText.transform.position = newPickup.transform.position;
            newText.transform.Translate(0.4f, 0.4f, 0);
            newText.GetComponent<Rigidbody2D>().velocity = newPickup.GetComponent<Rigidbody2D>().velocity;

            layerOrder += 2;
            timeSinceSpawn = 0;
        }
    }
}
