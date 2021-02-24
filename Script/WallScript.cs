using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    // Start is called before the first frame update
    private int WallValue;
    private GameMasterScript GM;
    private int Charge;
    private int chrono;
    private bool CR_run;
    void Start()
    {
        CR_run = false;
        chrono = 0;
        Charge = 0;
        GenerateColor();
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    public void Touch()
    {
        if(CR_run == false)
        {
            Charge = 1;
            chrono = 5;
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        CR_run = true;
        while (chrono > 0)
        {
            yield return new WaitForSeconds(1);
            chrono--;
        }
        CR_run = false;
        Charge = 0;
        StopCoroutine(Timer());

    }

    private void lvUp()
    {
        if( WallValue < 0)
        {
            WallValue--;
        }
        else
        {
            WallValue++;
        }
    }

    void OnCollisionEnter(Collision objectCollision)
    {
        if (objectCollision.gameObject.GetComponent<MainBallScript>() != null)
        {
            if(Charge == 1)
            {
                objectCollision.gameObject.GetComponent<MainBallScript>().IncreaseSpeed(GM.getSpeedBoost());  
            }
            GM.IncreasePointWall(WallValue);
            lvUp();
            objectCollision.gameObject.GetComponent<MainBallScript>().IncreaseSpeed(1);
        }
        else if (objectCollision.gameObject.GetComponent<NpcBallScript>() != null)
        {
            if(Charge == 1)
            {
                objectCollision.gameObject.GetComponent<NpcBallScript>().IncreaseSpeed(GM.getSpeedBoost());
            }
            objectCollision.gameObject.GetComponent<NpcBallScript>().IncreaseSpeed(1);
            GM.IncreasePointWall(WallValue);
            lvUp();
        }
        GenerateColor();
    }

    

    public void setWallValue(int WallV, GameMasterScript gm)
    {
        WallValue = WallV;
        GM = gm;
    }

    private void GenerateColor()
    {
        
        if (WallValue < 0)
        {
            float murColor = (255 + (10 * WallValue))/255f;
            //Debug.Log(murColor);
            GetComponent<Renderer>().material.color = new Color(1, murColor, 0);
        }
        else
        {
            float murColor = (255 - 10 * WallValue) / 255f;
            //Debug.Log(murColor);
            GetComponent<Renderer>().material.color = new Color(0, murColor, 1);
        }
    }

}
