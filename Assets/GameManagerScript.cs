using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject Game;
    [SerializeField]
    Button Restart;
    [SerializeField]
    Button Quit;
    [SerializeField]
    Text FinalText;
    [SerializeField]
    GameObject CanvasGameObject;

    bool canShow;
    int selectedButton;
    const int MaxIDButton = 1;
    void Start()
    {
        selectedButton = 0;
        InitCanvas();
        HideCanvas();
        ButtonUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (canShow)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                IncreaseButtonSelected(1);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                IncreaseButtonSelected(-1);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                Action();
            }
        }
    }

    private void InitCanvas()
    {
        FinalText.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 3, Screen.height / 3);
        FinalText.gameObject.GetComponent<RectTransform>().position += new Vector3(0, Screen.height / 4, 0);
        Quit.gameObject.GetComponent<RectTransform>().position += new Vector3(0, -Screen.height / 4);


    }


    private void IncreaseButtonSelected(int increasedValue)
    {
        if( selectedButton == MaxIDButton)
        {
            selectedButton = 0;
        }
        else
        {
            selectedButton = 1;
        }
        ButtonUpdate();
    }

    public void GameStart()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ShowCanvas()
    {
        CanvasGameObject.SetActive(true);
        canShow = true;
    }

    public void HideCanvas()
    {
        CanvasGameObject.SetActive(false);
        canShow = false;
    }

    private void ButtonUpdate()
    {
        if( selectedButton == 0)
        {
            Restart.gameObject.GetComponent<Image>().color = Color.green;
            Quit.gameObject.GetComponent<Image>().color = Color.white;
        }
        else if (selectedButton == 1)
        {
            Restart.gameObject.GetComponent<Image>().color = Color.white;
            Quit.gameObject.GetComponent<Image>().color = Color.green;
        }

    }

    public void setEndGame(bool win)
    {
        PauseGame();
        ShowCanvas();
        if (win)
        {
            FinalText.text = "YOU WIN";
        }
        else
        {
            FinalText.text = "YOU LOSE";
        }
    }


    public void Action()
    {
        switch( selectedButton)
        {
            case 0:
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                break;
            default:
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                break;
        }
        Debug.Log(selectedButton);
    }



}


