using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerManager : MonoBehaviour
{
    // menu contextuel 
    // plus animation : icone peuvent être plus explisif
    // ajouter un bouton pause 
    // Start is called before the first frame update
    MainBallScript mainBall;
    ArrowManager arrow;
    GameMasterScript GM;

    private void Awake()
    {
        GetComponentInChildren<MainBallScript>().setManager(this);
        GetComponentInChildren<ArrowManager>().setManager(this);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void IncreaseScore()
    {
        GM.IncreasePointBall(1);
    }

    public void setArrow(ArrowManager element)
    {
        arrow = element;
    }

    public void setMainBall(MainBallScript element)
    {
        mainBall = element;
    }
    public MainBallScript getMainBall()
    {
        return mainBall;
    }

    public ArrowManager getArrow()
    {
        return arrow;
    }
    public void setGameMaster(GameMasterScript gm)
    {
        GM = gm;
    }

}
