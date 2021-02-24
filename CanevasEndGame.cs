using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanevasEndGame : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Button buttonMenu;
    [SerializeField]
    UnityEngine.UI.Button buttonPlayAgain;
    [SerializeField]
    UnityEngine.UI.Button buttonSecondChance;
    [SerializeField]
    UnityEngine.UI.Text title;
    GameMasterScript gm;

    // Start is called before the first frame update
    void Start()
    {
        
        setPositionText("Your Score :\n");
        setBoutonPlayAgainPosition("Play Again");
        setBoutonReturnMenuPosition("Main Menu");
        setButtonSecondChancePosition("Second Chance");
        gameObject.SetActive(false);
        buttonMenu.onClick.AddListener(ClickMenu);
        buttonPlayAgain.onClick.AddListener(ClickPlayAgain);
        buttonSecondChance.onClick.AddListener(ClickSecondChance);
        gm = FindObjectOfType<GameMasterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        //recim-end(score,false,60);
    }

    public void ClickPlayAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        // recim-end(score,false,60);
    }

    public void ClickSecondChance()
    {
        if(gm.getRevive()== 0)
        {
            gm.Continue();
            gameObject.SetActive(false);
        }
        else
        {
            buttonSecondChance.GetComponentInChildren<UnityEngine.UI.Text>().text = "NO MORE CHANCE";
        }
    }

    public void ShowCanevas(string score)
    {
        title.text = "Your Score :\n" + score;
        gameObject.SetActive(true);
    }

    private void setPositionText(string text)
    {
        int y = Screen.height / 2 + Screen.height / 6;
        int x = Screen.width / 2;
        title.rectTransform.position = new Vector3(x, y, 0);
        title.rectTransform.sizeDelta = new Vector2(Screen.width / 2, Screen.height / 5);
        initText(title,text);
    }

    private void setBoutonPlayAgainPosition(string text)
    {
        int y = Screen.height / 2 - Screen.height / 6;
        int x = Screen.width / 2 - Screen.width / 4;
        buttonPlayAgain.GetComponent<RectTransform>().position = new Vector3(x, y, 0);
        buttonPlayAgain.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 6, Screen.height / 8);
        initText(buttonPlayAgain.GetComponentInChildren<UnityEngine.UI.Text>(),text);

    }

    private void setBoutonReturnMenuPosition(string text)
    {
        int y = Screen.height / 2 - Screen.height / 6;
        int x = Screen.width / 2 + Screen.width / 4;
        buttonMenu.GetComponent<RectTransform>().position = new Vector3(x, y, 0);
        buttonMenu.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 6, Screen.height / 8);
        initText(buttonMenu.GetComponentInChildren<UnityEngine.UI.Text>(),text);
    }

    private void setButtonSecondChancePosition(string text)
    {
        int y = Screen.height / 2 - Screen.height / 6;
        int x = Screen.width / 2;
        buttonSecondChance.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 6, Screen.height / 8);
        buttonSecondChance.GetComponent<RectTransform>().position = new Vector3(x, y, 0);
        initText(buttonSecondChance.GetComponentInChildren<UnityEngine.UI.Text>(), text);
    }

    private void initText(UnityEngine.UI.Text text,string t)
    {
        text.GetComponent<UnityEngine.UI.Text>().alignment = TextAnchor.MiddleCenter;
        text.GetComponent<UnityEngine.UI.Text>().fontSize = (int)Mathf.Min(text.rectTransform.sizeDelta.x / 5, text.rectTransform.sizeDelta.y / 3);
        text.GetComponent<UnityEngine.UI.Text>().fontStyle = FontStyle.BoldAndItalic;
        text.fontSize = Screen.height / 32;
        text.GetComponent<UnityEngine.UI.Text>().color = Color.green;
        text.text = t;
    }
}
