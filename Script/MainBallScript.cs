using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBallScript : MonoBehaviour
{
    [SerializeField]
    Camera theCam;
    private Vector3 TouchPos;
    private PLayerManager manager;
    private Vector2 forceInput;
    // player game object manager 
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(0, 1, 0);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Touch()
    {
        TouchPos = transform.position;
    }

    public void TouchMove()
    {
        forceInput = manager.getArrow().ChangeArrow(TransformTouchPositionInGame());
    }

    public void RelaseTouch()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(forceInput.x, 0.01f, forceInput.y) * 100);
        manager.getArrow().RemoveArrow();
    }


    private void OnMouseDrag()
    {
        forceInput =  manager.getArrow().ChangeArrow(TransformMousePositionInGame());
    }
    
    void OnCollisionEnter(Collision objectCollision)
    {
       
        if (objectCollision.gameObject.GetComponent<NpcBallScript>() != null)
        {
            manager.IncreaseScore();
        }
        else if (objectCollision.gameObject.GetComponent<WallScript>() != null)
        {
            manager.IncreaseScore();
        }
    }

    public void IncreaseSpeed(float boost)
    {
        GetComponent<Rigidbody>().velocity *= boost;
    }

    private void OnMouseUp()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(forceInput.x, 0.01f, forceInput.y)*100);
        manager.getArrow().RemoveArrow(); // set sphere position.
    }


    private Vector2 TransformTouchPositionInGame()
    {
        float FalseX = Input.GetTouch(0).position.x;
        float FalseZ = Input.GetTouch(0).position.y;
        float TrueX = FalseX - Screen.width / 2;
        float TrueY = FalseZ - Screen.height / 2;
        return new Vector2(TrueX, TrueY);
    }

    private Vector2 TransformMousePositionInGame()
    {
        float FalseX = Input.mousePosition.x;
        float FalseZ = Input.mousePosition.y;
        float TrueX = FalseX-Screen.width/2;
        float TrueY = FalseZ-Screen.height/2;
        return new Vector2(TrueX, TrueY);
    }

    public void setManager(PLayerManager m)
    {
        manager = m;
        m.setMainBall(this);
    }

    
}
