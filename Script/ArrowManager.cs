using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    // Start is called before the first frame update
    LineRenderer Linemaker;
    PLayerManager pLayer;
    SubArrowScript subCaster;
    const int MaxDistance = 10;
    bool onclick;
    void Start()
    {
        subCaster = FindObjectOfType<SubArrowScript>();
        Linemaker = GetComponent<LineRenderer>();
        onclick = false;
        Linemaker.SetPosition(0,new Vector3(0, transform.position.y, 0));
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        
    }

    void Update()
    {
        
    }

    private float DroiteGenerator(float x, float y)
    {
        return y / x;
    }

    private Vector2 SystemeDetection(float valeurX, float valeurY)
    {
        float X = 1;
        float Y = DroiteGenerator(valeurX, valeurY);
        float pointIntersectionX = Mathf.Round((Mathf.Sqrt((MaxDistance * MaxDistance) / (X * X + Y * Y)))*100f)/100f;
        float pointIntersectionY = pointIntersectionX * Y;
        return new Vector2(pointIntersectionX, pointIntersectionY);
    }

    private bool InCercle(float x, float y)
    {
        if(x*x + y*y > MaxDistance * MaxDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector2 LimiteValeur(float ValueX, float ValueY)
    {
        return Vector2.zero;
    }

    private float CorrectionQ(float Value, bool Negatif)
    {
        if(Value < 0 && Negatif == false|| Value> 0 && Negatif == true)
        {
            return -Value;
        }
        else { return Value;  }
    }

    private Vector2 calculatorPosition(Vector2 positionmouse)
    {
        onclick = true;
        float px = transform.position.x;
        float py = transform.position.z;
        float distancePx = (px- positionmouse.x)/10;
        float distancePy = (py-  positionmouse.y)/10;
        if (InCercle(distancePx, distancePy))
        {
            bool Xnegatif = false;
            bool Ynegatif = false;
            if( distancePx < 0)
            {
                Xnegatif = true;
            }
            if( distancePy < 0)
            {
                Ynegatif = true;
            }
            Vector2 newPosition = new Vector2(0, 10);
            if (distancePx != 0)
            {
                newPosition = SystemeDetection(distancePx, distancePy);
            }
            distancePx = CorrectionQ(newPosition.x, Xnegatif);
            distancePy = CorrectionQ(newPosition.y, Ynegatif);
            
            
        }
        
        Linemaker.SetPosition(1, new Vector3(px+distancePx, transform.position.y, py+distancePy));
        subCaster.ChangeArrow(distancePx, distancePy);
        return new Vector2(distancePx, distancePy);
    }
    public Vector2 ChangeArrow(Vector2 positionMouse)
    {
        return calculatorPosition(positionMouse);
    }

    

    public void RemoveArrow()
    {
        onclick = false;
        Linemaker.SetPosition(0, new Vector3(transform.position.x,transform.position.y,transform.position.z));
        Linemaker.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        subCaster.RemoveArrow();
    }
























    public void setManager(PLayerManager m)
    {
        pLayer = m;
        m.setArrow(this);
    }
}
