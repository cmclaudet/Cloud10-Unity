using UnityEngine;
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
            int colSortingOrder = col.GetComponent<SpriteRenderer>().sortingOrder;
            GameObject[] allText = GameObject.FindGameObjectsWithTag("PickupText");
            for (int i = 0; i < allText.Length; i++)
            {
                if (allText[i].GetComponent<Renderer>().sortingOrder == colSortingOrder + 1)
                {
                    allText[i].gameObject.SetActive(false);
                    break;
                }
            }
            col.gameObject.SetActive(false);
        }
    }
}
