using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatdesPied : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject parent;
    void Start()
    {
        parent = this.GetComponentInParent<Transform>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = parent.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touche Sol");
    }
}
