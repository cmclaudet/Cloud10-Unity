using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class move : MonoBehaviour
{
    public AudioSource droplet1;
    public AudioSource droplet2;
    public AudioSource droplet3;
    public AudioSource droplet4;
    public AudioSource droplet5;
    public AudioSource clearSkies;
    private AudioSource[] droplets;

    public float forceMag;
    public int number = 0;
    private bool control = true;
    public int loseNumberNeg = -20;
    public int loseNumberPos = 30;
    public bool win = false;
    public bool lose = false;

    public bool canSpawnStorm = true;

    void Start()
    {
        droplets = new AudioSource[5]{ droplet1, droplet2, droplet3, droplet4, droplet5 };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount > 0 && control)
        {
            Vector2 touch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector3 fingerPos = new Vector3(touch.x, touch.y, 0.0f);
            Vector3 forceDir = (fingerPos - transform.position).normalized;
            Vector3 force = forceDir * forceMag;
            GetComponent<Rigidbody2D>().AddForce(force);
        }
        if (control == false)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (control)
        {
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
                deleteStorms();
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
        int bodySortingOrder = body.GetComponent<SpriteRenderer>().sortingOrder;
        GameObject[] allText = GameObject.FindGameObjectsWithTag(textTag);
        for (int i = 0; i < allText.Length; i++)
        {
            if (allText[i].GetComponent<Renderer>().sortingOrder == bodySortingOrder + 1)
            {
                if (body.gameObject.CompareTag("Pickup"))
                {
                    int pickupNumber = allText[i].GetComponent<sort>().number;
                    checkSwitch(number, pickupNumber);
                    number += pickupNumber;
                    if (number == 10)
                    {
                        control = false;
                        win = true;
                    }
                }
                else if (body.gameObject.CompareTag("Evil"))
                {
                    number += allText[i].GetComponent<sortevil>().number;
                    if (number < loseNumberNeg || number > loseNumberPos)
                    {
                        control = false;
                        lose = true;
                    }
                }
                allText[i].gameObject.SetActive(false);
                break;
            }
        }
        body.gameObject.SetActive(false);
    }

    void checkSwitch(int playerNumber, int pickupNumber)
    {
        if (playerNumber < 10 && 10 < (playerNumber + pickupNumber))
        {
            changeSign("+");
        }
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
            allEvilText[i].GetComponent<sortevil>().number *= -1;
            allEvilText[i].GetComponent<TextMesh>().text = sign;
        }
    }
}
