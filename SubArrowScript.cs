using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubArrowScript : MonoBehaviour
{
    // Start is called before the first frame update
    LineRenderer arrow;
    MainBallScript mainBall;
    const int hauteur = 3; 
    bool click;

    void Start()
    {
        click = false;
        arrow = GetComponent<LineRenderer>();
        mainBall = GetComponentInParent<MainBallScript>();
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position = mainBall.GetComponent<RectTransform>().position;
        arrow.SetPosition(0, transform.position);
        if (click == false)
        {
            arrow.SetPosition(1, transform.position);
        }
    }

    public void ChangeArrow(float distancePx,float distancePy)
    {
        click = true;
        arrow.SetPosition(1, new Vector3(transform.position.x + distancePx, hauteur, transform.position.z + distancePy));
    }

    public void RemoveArrow()
    {
        click = false;
        arrow.SetPosition(0, transform.position);
        arrow.SetPosition(1, transform.position);
    }
}
