using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    int BalleValue;
    int Charge;
    GeneratorBoule NpcBalleManager;
    bool CR_run;
    int chrono;
    void Start()
    {
        chrono = 0;
        CR_run = false;
        Charge = 0;
        GenerateColor();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Touch()
    {
        if (CR_run == false)
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
        if (BalleValue < 0)
        {
            BalleValue--;
        }
        else
        {
            BalleValue++;
        }
    }

    public void IncreaseSpeed(float boost)
    {
        GetComponent<Rigidbody>().velocity *= boost;
    }

    public void setData(int value,GeneratorBoule manager)
    {
        BalleValue = value;
        NpcBalleManager = manager;
    }

    void OnCollisionEnter(Collision objectCollision)
    {
        if (objectCollision.gameObject.GetComponent<MainBallScript>() != null)
        {
            if (Charge == 1)
            {
                objectCollision.gameObject.GetComponent<MainBallScript>().IncreaseSpeed(NpcBalleManager.getSpeedBoost());
            }
            objectCollision.gameObject.GetComponent<MainBallScript>().IncreaseSpeed(1);
            NpcBalleManager.IncreasePoint(BalleValue);
            lvUp();
        }
        else if (objectCollision.gameObject.GetComponent<WallScript>() != null)
        {
            NpcBalleManager.IncreasePoint(BalleValue);
            lvUp();
        }
        else if (objectCollision.gameObject.GetComponent<NpcBallScript>() != null)
        {
            if (Charge == 1)
            {
                objectCollision.gameObject.GetComponent<NpcBallScript>().IncreaseSpeed(NpcBalleManager.getSpeedBoost());
            }
            objectCollision.gameObject.GetComponent<NpcBallScript>().IncreaseSpeed(1);
            NpcBalleManager.IncreasePoint(BalleValue);
            lvUp();
        }
        GenerateColor();
    }

    private void GenerateColor()
    {

        if(BalleValue < 0)
        {
            float balleColor = (255 + (10 * BalleValue)) / 255f;
            GetComponent<Renderer>().material.color = new Color(1,balleColor, 0);
        }
        else
        {
            float balleColor = (255 - (10 * BalleValue)) / 255f;
            GetComponent<Renderer>().material.color = new Color(0,balleColor, 1);
        }
    }
}
