using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject pl;
    void Start()
    {
        pl = FindObjectOfType<PLayerMouvementScipt>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = pl.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.Distance(this.GetComponent<BoxCollider2D>()).pointA);
        Debug.Log(other.Distance(this.GetComponent<BoxCollider2D>()).pointB);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Stay");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("EXIT");
    }
}
