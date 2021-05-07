using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestructibleScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Sprite FullLife;
    [SerializeField]
    Sprite MidLife;
    private int Hp;

    void Start()
    {
        Hp = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(1/Time.deltaTime);
    }

    private void TakeDamage()
    {
        Hp -= 1;
        if(Hp == 1)
        {
            GetComponent<SpriteRenderer>().sprite = MidLife;
        }
        else if( Hp == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PLayerMouvementScipt>() != null)
        {
            if(collision.gameObject.GetComponent<PLayerMouvementScipt>().DealDamage() )
            {
                TakeDamage();
            }
        }
        if( collision.gameObject.GetComponent<BouleScript>() != null)
        {
            Destroy(this.gameObject);
        }
    }
}
