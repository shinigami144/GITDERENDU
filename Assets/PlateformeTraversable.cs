using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeTraversable : MonoBehaviour
{

    GameObject Player;
    public bool ContinueVerifPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PLayerMouvementScipt>().gameObject;
        ContinueVerifPlayer = true;
    }


    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player_Want_TraversPlateforme();
        if (Player_UpMe() && ContinueVerifPlayer)
        {
            
            this.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else if (!Player_UpMe())
        {
            
            this.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
          
            this.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void Player_Want_TraversPlateforme()
    {
        if( Player.GetComponent<PLayerMouvementScipt>().getDoubleInput() == 2)
        {
            ContinueVerifPlayer = false;
        }
        else if(Player.GetComponent<PLayerMouvementScipt>().EtatPerso == PLayerMouvementScipt.ETAT.Intengible)
        {
            ContinueVerifPlayer = false;
        }
        else
        {
            ContinueVerifPlayer = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.GetComponent<PLayerMouvementScipt>() != null)
        {
            ContinueVerifPlayer = false;
        }
        else if (collision.gameObject.GetComponent<BouleScript>() != null)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PLayerMouvementScipt>() != null)
        {
            ContinueVerifPlayer = true;
        }
    }

    private bool Player_UpMe()
    {
        if(Player.transform.position.y-Player.GetComponent<BoxCollider2D>().size.y/3 >= this.transform.position.y)
        {
            return true;
        }
        return false;
    }



}
