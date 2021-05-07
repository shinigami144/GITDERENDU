using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject Boule;

    int numberBall;
    GameObject SpikeBoule;
    void Start()
    {
       numberBall = 1;
       SpikeBoule =  Instantiate(Boule);
       SpikeBoule.GetComponent<BouleScript>().Init(this, numberBall);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBall()
    {
        numberBall += 1;
        Destroy(SpikeBoule);
        SpikeBoule = Instantiate(Boule);
        SpikeBoule.GetComponent<BouleScript>().Init(this, numberBall);
    }
}
