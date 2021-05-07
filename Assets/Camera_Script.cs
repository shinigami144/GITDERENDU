using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    // Start is called before the first frame update

    float TOP;
    float LEFT;
    float RIGHT;
    float DOWN;

    Transform PlayerPosition;
    void Start()
    {
        PlayerPosition = FindObjectOfType<PLayerMouvementScipt>().gameObject.transform;
        TOP = 10;
        LEFT = -1.5f;
        DOWN = 0;
        RIGHT = 150;
    }

    private  void CentreCam()
    {
        float x = PlayerPosition.transform.position.x;
        float y = PlayerPosition.transform.position.y;
        if( x > RIGHT)
        {
            x = RIGHT;
        }
        else if (x < LEFT)
        {
            x = LEFT;
        }
        if(y > TOP)
        {
            y = TOP;
        }
        else if ( y < DOWN)
        {
            y = DOWN;
        }
        transform.position = new Vector3(x, y, transform.position.z);// tenter un effet de zoom    avec la dBoule et dPlayer 
        // essayer de tendre vers le joueur .
        // sentiment de tremblement encore présent.
    }
    private void FixedUpdate()
    {
        //CentreCam
    }
    // Update is called once per frame
    void Update()
    {
        //CentreCam();
    }
    private void LateUpdate()
    {
        CentreCam();
    }


}
