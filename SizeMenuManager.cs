using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    UnityEngine.UI.Button startButton;
    [SerializeField]
    UnityEngine.UI.Button quitButton;
    [SerializeField]
    UnityEngine.UI.Text gameTitle;
    [SerializeField]
    UnityEngine.UI.Button tutoButton;
    [SerializeField]
    UnityEngine.UI.Text imagePubOne;
    [SerializeField]
    UnityEngine.UI.Text imagePubTwo;
    void Start()
    {

        setPositionTitre();
        setButtonPosition();
        startButton.onClick.AddListener(PlayGame);
        setButtonPositionQuit();
        setButtonTutoPosition();
        quitButton.onClick.AddListener(EndGame);
        tutoButton.onClick.AddListener(TutoGame);
        SetPositionPub();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TutoGame()
    {
        tutoButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Tuto ( en chantier )";
    }

    private void EndGame()
    {
        Application.Quit();
    }

    private void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        // recim-start();
    }


    void SetPositionPub()
    {
        imagePubOne.GetComponent<RectTransform>().transform.position = new Vector3(1 * Screen.width / 8, 2*Screen.height / 4, 0);
        imagePubOne.fontSize = Screen.height / 20;
        imagePubOne.fontStyle = FontStyle.Bold;
        imagePubOne.alignment = TextAnchor.MiddleCenter;
        imagePubOne.text = "PUB1";
        imagePubOne.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 4, Screen.height);
        imagePubTwo.GetComponent<RectTransform>().transform.position = new Vector3(7 * Screen.width / 8, 2*Screen.height / 4, 0);
        imagePubTwo.fontSize = Screen.height / 20;
        imagePubTwo.fontStyle = FontStyle.Bold;
        imagePubTwo.alignment = TextAnchor.MiddleCenter;
        imagePubTwo.text = "PUB2";
        imagePubTwo.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 4, Screen.height);
    }

    void setPubInfo()
    {

    }

    void SetConfigButtonTuto()
    {
        tutoButton.GetComponentInChildren<UnityEngine.UI.Text>().fontSize = Screen.height / 20;
        tutoButton.GetComponentInChildren<UnityEngine.UI.Text>().fontStyle = FontStyle.Bold;
        tutoButton.GetComponentInChildren<UnityEngine.UI.Text>().alignment = TextAnchor.MiddleCenter;
        tutoButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Tuto";
        tutoButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height / 10);
    }

    void setButtonTutoPosition()
    {
        int h = 3 * Screen.height / 8;
        int x = Screen.width / 2;
        tutoButton.GetComponent<RectTransform>().position = new Vector3(x, h, 0);
        SetConfigButtonTuto();
    }

    void SetConfigTitle()
    {
        gameTitle.fontSize = Screen.height / 10;
        gameTitle.fontStyle = FontStyle.Bold;
        gameTitle.alignment = TextAnchor.MiddleCenter;
        gameTitle.text = "PROJET MOBILE";
        gameTitle.rectTransform.sizeDelta = new Vector2(Screen.width / 2, Screen.height / 3);
    }

    void SetConfigButton()
    {
        startButton.GetComponentInChildren<UnityEngine.UI.Text>().fontSize = Screen.height/20;
        startButton.GetComponentInChildren<UnityEngine.UI.Text>().fontStyle = FontStyle.Bold;
        startButton.GetComponentInChildren<UnityEngine.UI.Text>().alignment = TextAnchor.MiddleCenter;
        startButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "PLAY GAME";
        startButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height / 10);
    }

    void SetConfigButtonQuit()
    {
        quitButton.GetComponentInChildren<UnityEngine.UI.Text>().fontSize = Screen.height / 20;
        quitButton.GetComponentInChildren<UnityEngine.UI.Text>().fontStyle = FontStyle.Bold;
        quitButton.GetComponentInChildren<UnityEngine.UI.Text>().alignment = TextAnchor.MiddleCenter;
        quitButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "END GAME";
        quitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height / 10);
    }

    void setPositionTitre()
    {
        int h = Screen.height - Screen.height / 10;
        int x = Screen.width / 2;
        gameTitle.rectTransform.position = new Vector3(x, h, 0);
        SetConfigTitle();
    }

    void setButtonPosition()
    {
        int h = 5* Screen.height / 8;
        int x = Screen.width / 2;
        startButton.GetComponent<RectTransform>().position = new Vector3(x, h, 0);
        SetConfigButton();
    }

    void setButtonPositionQuit()
    {
        int h =  Screen.height / 8;
        int x = Screen.width / 2;
        quitButton.GetComponent<RectTransform>().position = new Vector3(x, h, 0);
        SetConfigButtonQuit();
    }

}
