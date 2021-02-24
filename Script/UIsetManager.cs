using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIsetManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private UnityEngine.UI.Text score;
    [SerializeField]
    private UnityEngine.UI.Text chrono;
    private GameMasterScript GM;

    private const int pourcentExtendScreen = 8;
    private int increaseTime;

    void Start()
    {
        increaseTime = 0;
        SetScorePositionCanevas();
        SetTimePositionCanevas();
        initText(score);
        initText(chrono);
    }

    void FixedUpdate()
    {
        score.text = "Score : \n" + GM.getScore().ToString();
        chrono.text = "Time : \n" + GM.getTime().ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void initText(UnityEngine.UI.Text text)
    {
        text.GetComponent<UnityEngine.UI.Text>().alignment = TextAnchor.UpperCenter;
        text.GetComponent<UnityEngine.UI.Text>().fontSize =(int)Mathf.Min(text.rectTransform.sizeDelta.x/5,text.rectTransform.sizeDelta.y/2);
        text.GetComponent<UnityEngine.UI.Text>().fontStyle = FontStyle.BoldAndItalic;
        text.GetComponent<UnityEngine.UI.Text>().color = Color.green;
    }

    private void SetScorePositionCanevas()
    {
        // 0 0 0 -> -1480 -720 0
        int y = Screen.height - (pourcentExtendScreen * Screen.height)/100;
        int x = (pourcentExtendScreen * Screen.width)/100;
        score.rectTransform.position = new Vector3(x, y, 0);
        int height = Screen.height*pourcentExtendScreen / 100;
        int width = Screen.width*pourcentExtendScreen / 100;
        score.rectTransform.sizeDelta = new Vector2(width, height);
    }

    private void SetTimePositionCanevas()
    {
        int y = Screen.height - (pourcentExtendScreen * Screen.height) / 100;
        int x = Screen.width - (pourcentExtendScreen * Screen.width) / 100;
        chrono.rectTransform.position = new Vector3(x, y, 0);

        int height = Screen.height * pourcentExtendScreen / 100;
        int width = Screen.width * pourcentExtendScreen / 100;
        chrono.rectTransform.sizeDelta = new Vector2(width, height);
    }


    public void setGameMaster(GameMasterScript gm)
    {
        GM = gm;
    }
}
