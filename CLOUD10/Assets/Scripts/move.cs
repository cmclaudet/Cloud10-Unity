﻿using UnityEngine;
using System.Collections;

public class move : MonoBehaviour
{
    public float forceMag;
    public int number = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 forceDir = (mousePos - transform.position).normalized;
            Vector3 force = forceDir * forceMag;
            GetComponent<Rigidbody2D>().AddForce(force);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Pickup"))
        {
            setInactive(col, "PickupText");
        }
        if (col.gameObject.CompareTag("Evil"))
        {
            setInactive(col, "EvilText");
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
                    number += allText[i].GetComponent<sort>().number;
                }
                else if (body.gameObject.CompareTag("Evil"))
                {
                    number += allText[i].GetComponent<sortevil>().number;
                }
                allText[i].gameObject.SetActive(false);
                break;
            }
        }
        body.gameObject.SetActive(false);
    }
}
