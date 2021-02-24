using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour
{
    // Start is called before the first frame update
    List<WallScript> WallList;
    //NPCBallManager npcBallManager;
    public List<int> wallValue;
    private PLayerManager pLayer;
    UIsetManager UiManager;
    CanevasEndGame endGame;
    GeneratorBoule NpcManager;
    private int score;
    private int chrono;
    private int LimiteX;
    private int LimiteY;
    private int difficulty;
    private float SpeedBoost;
    private bool end;
    private int Revive;
    // essayer un systheme baser sur les couleur en HEXA pour generer des valeur negatif max valueur dans rouge 
    // positif max valeur dans bleu
    // valeur dans le vert


    /* STOP GAME = 
        Bloquer Input et increase score 
         
    */

    void Awake()
    {
        Revive = 0;
        end = false;
        endGame = FindObjectOfType<CanevasEndGame>();
        SpeedBoost = 1.5f;
        chrono = 60;
        LimiteY = 10;
        LimiteX = 10;
        difficulty = 5;

        WallList = new List<WallScript>(FindObjectsOfType<WallScript>());
        pLayer = FindObjectOfType<PLayerManager>();
        NpcManager = FindObjectOfType<GeneratorBoule>();
        pLayer.setGameMaster(this);

        NpcManager.setGameManager(this,LimiteX,LimiteY,difficulty);
        
        UiManager = FindObjectOfType<UIsetManager>();
        UiManager.setGameMaster(this);
        wallValue = (generatorListInt(difficulty,4));
        for(int i=0;i < wallValue.Count; i++)
        {
            WallList[i].setWallValue(wallValue[i], this);
        }
    }

    public int getRevive()
    {
        return Revive;
    }

    public void Continue()
    {
        // recim-revive();
        chrono = 30;
        end = false;
        StartCoroutine(Timer());
        Revive++;
    }

    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if(end == false)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Began)
                {
                    Vector3 point = new Vector3(t.position.x, t.position.y, 0);
                    Ray ray = Camera.main.ViewportPointToRay(point);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.GetComponent<WallScript>())
                        {
                            hit.collider.gameObject.GetComponent<WallScript>().Touch();
                        }
                        else if (hit.collider.gameObject.GetComponent<NpcBallScript>())
                        {
                            hit.collider.gameObject.GetComponent<NpcBallScript>().Touch();
                        }
                        else if (hit.collider.gameObject.GetComponent<MainBallScript>())
                        {
                            hit.collider.gameObject.GetComponent<MainBallScript>().Touch();
                        }
                    }
                }
                else if (t.phase == TouchPhase.Moved)
                {
                    pLayer.getMainBall().TouchMove();
                }
                else if (t.phase == TouchPhase.Ended)
                {
                    pLayer.getMainBall().RelaseTouch();
                }

            }
        }
    }

    private IEnumerator Timer()
    {
        while(chrono > 0)
        {
            yield return new WaitForSeconds(1);
            chrono--;
        }
        end = true;
        endGame.ShowCanevas(score.ToString());
        StopCoroutine(Timer());
        
    }

    private int ValeurListe(List<int> l)
    {
        int value = 0;
        for(int i = 0; i< l.Count; i++)
        {
            value += l[i];
        }
        return value;
    }

    private List<int> generatorListInt(int difficulty, int longeurListe)
    {
        List<int> l = new List<int>();
        for(int i = 0;i< longeurListe; i++)
        {
            l.Add(Random.Range(-difficulty, difficulty));
        }
        int n = Random.Range(0, longeurListe);
        int alpha = ValeurListe(l);
        //Debug.Log(alpha);
        if (alpha > difficulty)
        {
            //Debug.Log("Num");
            l[n] -= (alpha - difficulty);
            
        }
        else if(alpha < difficulty)
        {
            l[n] -= (alpha + difficulty);
        }
        //Debug.Log(l[0] + " " + l[1] + " " + l[2] + " " + l[3]);
        return l;

    }

    public void IncreasePointBall(int value)
    {
        if( chrono > 0)
        {
            score += value;
        }
    }

    public void IncreasePointWall(int value)
    {
        if(chrono > 0)
        {
            score += value;
        }
    }

    public int getScore()
    {
        return score;
    }

    public int getTime()
    {
        return chrono;
    }

    public bool GameClear()
    {
        return (chrono == 0);
    }

    public float getSpeedBoost()
    {
        return SpeedBoost;
    }
}
