using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class move : MonoBehaviour
{
    /*Moves player when touch is applied
      Manages collisions with other clouds and their effects
      Triggers win/lose states when conditions are met*/

    //Sound effects when player collides with pickups and storms
    public AudioSource droplet1;
    public AudioSource droplet2;
    public AudioSource droplet3;
    public AudioSource droplet4;
    public AudioSource droplet5;
    //sound effect for when player collides with gold cloud
    public AudioSource clearSkies;
    private AudioSource[] droplets;

    public float forceMag;          //force applied when touch is applied
    public int number = 0;          //player number
    private bool control = true;    //toggle for when player can and cannot control
    public int loseNumberNeg = -20; //minimum score player can have before losing
    public int loseNumberPos = 30;  //max score player can have before losing
    public bool win = false;        //triggers win state
    public bool lose = false;       //triggers lose state

    public bool canSpawnStorm = true;   //when true storms are able to spawn

    void Start()
    {
        //five different sounds for when player collides with clouds to add randomization
        droplets = new AudioSource[5]{ droplet1, droplet2, droplet3, droplet4, droplet5 };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If screen is touched and player can be controlled, force is applied in direction of touch
        if (Input.GetMouseButton(0) && control)
        {
            //find touch position in world co-ordinates
            Vector2 touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 fingerPos = new Vector3(touch.x, touch.y, 0.0f);
            Vector3 forceDir = (fingerPos - transform.position).normalized;
            Vector3 force = forceDir * forceMag;
            GetComponent<Rigidbody2D>().AddForce(force);
        }
        if (control == false)   //if control == false player must have won or lost
        {
            //set player velocity to 0 to stop player moving instantaneously after winning/losing
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (control)
        {
            //if player collides with cloud random droplet sound is played and cloud is set inactive
            int soundNum = Random.Range(0,4);
            if (col.gameObject.CompareTag("Pickup"))
            {
                droplets[soundNum].Play();
                setInactive(col, "PickupText");
            }
            if (col.gameObject.CompareTag("Evil"))
            {
                droplets[soundNum].Play();
                setInactive(col, "EvilText");
            }
            if (col.gameObject.CompareTag("Gold"))
            {
                clearSkies.Play();
                col.gameObject.SetActive(false);
                deleteStorms();     //removes all storm clouds and sets storm spawn to false
            }
        }
    }

    void deleteStorms()
    {
        GameObject[] allStorms = GameObject.FindGameObjectsWithTag("Evil");
        GameObject[] allStormText = GameObject.FindGameObjectsWithTag("EvilText");

        for (int i = 0; i < allStorms.Length; i++)
        {
            allStorms[i].gameObject.SetActive(false);
            allStormText[i].gameObject.SetActive(false);
            canSpawnStorm = false;
        }
    }

    void setInactive(Collider2D body, string textTag)
    {
        /*To set cloud inactive need to set its text object inactive as well
            text object sorting order is always 1 value higher than its cloud
            thus sorting order of cloud is found to find its text and set both inactive
          Function ALSO checks for when player switches from a number above 10 to below 10, or vice versa
            this is to change the sign of numbers on storm cloud to ensure it is always a disadvantage for the player to get a storm cloud*/

        int bodySortingOrder = body.GetComponent<SpriteRenderer>().sortingOrder;    //cloud sorting order
        GameObject[] allText = GameObject.FindGameObjectsWithTag(textTag);          //all text objects for this type of cloud
        for (int i = 0; i < allText.Length; i++)
        {
            if (allText[i].GetComponent<Renderer>().sortingOrder == bodySortingOrder + 1)   //if text object has sorting order 1 value higher than the cloud's, it must be set inactive
            {
                if (body.gameObject.CompareTag("Pickup"))
                {
                    int pickupNumber = allText[i].GetComponent<sort>().number;
                    checkSwitch(number, pickupNumber);  //check for switch. only possible with pickup
                    number += pickupNumber; //find new number of player
                    if (number == 10)
                    {
                        control = false;
                        win = true;     //only check for win with pickup
                    }
                }
                else if (body.gameObject.CompareTag("Evil"))
                {
                    number += allText[i].GetComponent<sortevil>().number;
                    //only check for lose with storm
                    if (number < loseNumberNeg || number > loseNumberPos)
                    {
                        control = false;
                        lose = true;
                    }
                }
                allText[i].gameObject.SetActive(false);     //set text inactive
                break;
            }
        }
        body.gameObject.SetActive(false);       //set cloud inactive
    }

    void checkSwitch(int playerNumber, int pickupNumber)
    {
        //check for player number going from below 10 to above 10
        if (playerNumber < 10 && 10 < (playerNumber + pickupNumber))
        {
            changeSign("+");
        }
        //check for player number going from above 10 to below 10
        if ((playerNumber + pickupNumber) < 10 && 10 < playerNumber)
        {
            changeSign("-");
        }
    }

    void changeSign(string sign)
    {
        GameObject[] allEvilText = GameObject.FindGameObjectsWithTag("EvilText");
        for (int i = 0; i < allEvilText.Length; i++)
        {
            //change sign of storm clouds to ensure they are disadvantageous to player
            allEvilText[i].GetComponent<sortevil>().number *= -1;
            allEvilText[i].GetComponent<TextMesh>().text = sign;
        }
    }
}
