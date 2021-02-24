using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBoule : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject prefabBall;
    GameMasterScript GM;
    int LimiteX;
    int LimiteY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateBall(int numberBall)
    {
        for(int i = 0; i< numberBall; i++)
        {
            GameObject balleGenerer  = Instantiate(prefabBall);
            int y = Random.Range(-LimiteY, +LimiteY);
            int x = Random.Range(-LimiteX, LimiteX);
            int value = Random.Range(-numberBall, numberBall); 
            balleGenerer.GetComponent<NpcBallScript>().setData(value,this); // ATTENTION PEUX PAS ETRE 0 0
            balleGenerer.transform.position = new Vector3(x, 3, y);
            
        }
    }
    public void IncreasePoint(int point)
    {
        GM.IncreasePointBall(point);
    }
    public void setGameManager(GameMasterScript gm,int lx,int ly,int d)
    {
        GM = gm;
        LimiteX = lx;
        LimiteY = ly;
        GenerateBall(d);
    }

    public float getSpeedBoost()
    {
        return GM.getSpeedBoost();
    }

}
