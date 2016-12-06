using UnityEngine;
using System.Collections;

public class createWholepickup : MonoBehaviour {

    public Rigidbody2D justPickup;
    public Rigidbody2D justEvil;
    public GameObject pickupText;
    public GameObject evilText;
    public float spawnFrequencyPickup = 2;
    public float spawnFrequencyEvil = 1;
    public int layerOrderPickups;
    public int layerOrderEvils;
    private float timeSinceSpawnPickup = 0;
    private float timeSinceSpawnEvil = 0;

    // Use this for initialization
    void Start()
    {
        layerOrderPickups = 1;
        layerOrderEvils = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawnPickup += Time.deltaTime;
        timeSinceSpawnEvil += Time.deltaTime;

        //spawn new pickup every 2 seconds
        if (timeSinceSpawnPickup > spawnFrequencyPickup)
        {
            Rigidbody2D newPickup = Instantiate(justPickup);
            newPickup.GetComponent<SpriteRenderer>().sortingOrder = layerOrderPickups; //set draw order for pickup

            GameObject newText = Instantiate(pickupText);
            Renderer textRenderer = newText.GetComponent<Renderer>();     //find text mesh renderer
            textRenderer.sortingOrder = layerOrderPickups + 1;    //set draw order for text on pickup. Must be 1 above so it can be seen over the pickup.

            setTextToCloud(newPickup, newText);


            layerOrderPickups += 2;
            timeSinceSpawnPickup = 0;
        }

        if (timeSinceSpawnEvil > spawnFrequencyEvil)
        {
            Rigidbody2D newEvil = Instantiate(justEvil);
            newEvil.GetComponent<SpriteRenderer>().sortingOrder = layerOrderEvils;

            GameObject newTextEvil = Instantiate(evilText);
            Renderer evilTextRenderer = newTextEvil.GetComponent<Renderer>();
            evilTextRenderer.sortingOrder = layerOrderEvils + 1;

            setTextToCloud(newEvil, newTextEvil);

            layerOrderEvils += 2;
            timeSinceSpawnEvil = 0;
        }
    }

    void setTextToCloud(Rigidbody2D cloud, GameObject cloudText)
    {
        cloudText.transform.position = cloud.transform.position;
        cloudText.transform.Translate(0.4f, 0.4f, 0);
        cloudText.GetComponent<Rigidbody2D>().velocity = cloud.GetComponent<Rigidbody2D>().velocity;
    }
}
