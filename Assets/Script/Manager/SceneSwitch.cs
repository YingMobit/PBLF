using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{

    public GameObject Main;
    public GameObject DataInput_UI;
    private int stage=1;
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("Start");
    }

    public void Continue()
    {
        stage = 1;
    }

    public void ToEnd()
    {
        SceneManager.LoadScene("End");
    }

    public void DataInput()
    {
        DataInput_UI.SetActive(true);
    }

    public void Update()
    {
        if (Main != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                stage = -stage;
            }

            if (stage == 1) { Main.SetActive(false); Time.timeScale = 1; }
            if (stage == -1) { Main.SetActive(true); Time.timeScale = 0; }
        }
    }
}
