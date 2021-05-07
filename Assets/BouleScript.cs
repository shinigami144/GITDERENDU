using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class BouleScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D myRb;
    Vector3 lastPosition;
    Spawner sp;
    int speed;
    float time;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        time = 0;
        lastPosition = this.transform.position;
    }

    public void Init(Spawner spawn,int speed)
    {
        sp = spawn;
        this.speed = speed;
    }

    public void IncreaseSpeedBoule()
    {
        if( myRb.velocity.x < speed*1500)
        {
            myRb.AddForce(new Vector2(speed*1000, 0));
        }
    }

    private void FixedUpdate()
    {
        IncreaseSpeedBoule();
        lastPosition = this.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 1 && lastPosition == this.transform.position)
        {
            //this.transform.position = this.transform.position - new Vector3(3, 0, 0);
            
        }
        if(transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
        time += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name == "PhisicalDecor")
        {
            Vector3 hitPosition = Vector3.zero;
            Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
        }
        if(collision.gameObject.GetComponent<PlateformeTraversable>() != null)
        {
            Destroy(collision.gameObject);
        }
        
    }
}
