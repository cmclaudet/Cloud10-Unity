using UnityEngine;
using System.Collections;

/*Creates all cloud gameobjects and text gameobjects from prefabs
  Assigns their sorting layer and sorting order */

public class createWholepickup : MonoBehaviour {
    //All prefabs
    public Rigidbody2D justPickup;
    public Rigidbody2D justEvil;
    public GameObject pickupText;
    public GameObject evilText;
    public Rigidbody2D goldCloud;

    private GameObject player;

    //Defines amount of time between each cloud spawn
    public float spawnFrequencyPickup = 2;
    public float spawnFrequencyEvil = 1;
    public float spawnFrequencyGold = 5;
    public int spawnGoldProb = 3;   //Gold cloud has a 1/spawnGoldProb chance of spawning

    //Define timer for evil storm clouds to start spawning after disappearing
    public float respawnStormTimer = 5;
    private float currentStormTimer;

    //Define sorting order for Pickups and Evil clouds. 
    //These clouds are on separate sorting layers so can have the same sorting order numbers without overlapping
    public int layerOrderPickups;
    public int layerOrderEvils;

    //Timer for spawning clouds
    private float timeSinceSpawnPickup = 0;
    private float timeSinceSpawnEvil = 0;
    private float timeSinceSpawnGold = 0;

    // Use this for initialization
    void Start()
    {
        layerOrderPickups = 1;
        layerOrderEvils = 1;
        player = GameObject.Find("player");
        currentStormTimer = respawnStormTimer;
    }

    // Update is called once per frame
    void Update()
    {
        //Update timers
        timeSinceSpawnPickup += Time.deltaTime;
        timeSinceSpawnEvil += Time.deltaTime;
        timeSinceSpawnGold += Time.deltaTime;

        //spawn new pickup every 2 seconds
        if (timeSinceSpawnPickup > spawnFrequencyPickup)
        {
            spawnCloud(justPickup, pickupText, layerOrderPickups);
            //Add 2 to sorting order so next pickup spawns over both the last pickup and its text
            layerOrderPickups += 2;
            timeSinceSpawnPickup = 0;
        }

        if (timeSinceSpawnEvil > spawnFrequencyEvil)
        {
            if (player.GetComponent<move>().canSpawnStorm)
            {
                spawnCloud(justEvil, evilText, layerOrderEvils);
                layerOrderEvils += 2;
                timeSinceSpawnEvil = 0;
            }
        }

        //If storm clouds have disappeared start adding to respawn timer
        if (player.GetComponent<move>().canSpawnStorm == false)
        {
            updateStormTimer();  
        }

        //Randomize gold cloud spawn so chance of 1/spawnGoldProb of gold cloud spawning
        if (timeSinceSpawnGold > spawnFrequencyGold)
        {
            int roll = Random.Range(1, spawnGoldProb);
            if (roll == 1)
            {
                Instantiate(goldCloud);
            }
            timeSinceSpawnGold = 0;
        }
    }

    void spawnCloud(Rigidbody2D cloud, GameObject cloudText, int layerOrder)
    {
        Rigidbody2D newCloud = Instantiate(cloud);
        newCloud.GetComponent<SpriteRenderer>().sortingOrder = layerOrder; //set draw order for pickup

        //Instantiate text for pickup
        GameObject newText = Instantiate(cloudText);
        Renderer textRenderer = newText.GetComponent<Renderer>();     //find text mesh renderer
        textRenderer.sortingOrder = layerOrder + 1;    //set draw order for text on pickup. Must be 1 above so it can be seen over the pickup.

        //Set text to have same position and velocity as cloud
        setTextToCloud(newCloud, newText);
    }

    void setTextToCloud(Rigidbody2D cloud, GameObject cloudText)
    {
        cloudText.transform.position = cloud.transform.position;
        cloudText.transform.Translate(0.4f, 0.4f, 0);
        cloudText.GetComponent<Rigidbody2D>().velocity = cloud.GetComponent<Rigidbody2D>().velocity;
    }

    void updateStormTimer()
    {
        currentStormTimer -= Time.deltaTime;
        if (currentStormTimer < 0)
        {
            //Once timer is up storm clouds start respawning
            player.GetComponent<move>().canSpawnStorm = true;
            currentStormTimer = respawnStormTimer;
        }
    }
}
