using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{

    public GameObject Main;
    public GameObject DataInput_UI;
    public GameObject DataInput_;
    public InputManager inputManager;
    private int stage=1;

    private void Start()
    {
        
    }

    public InputField InputField;
    public void StartGame()
    {
        //DataInput_ = GameObject.Find("DataInput");
        //inputManager = DataInput_.GetComponent<InputManager>();
        PlayerPrefs.DeleteKey("PlayerName");
        PlayerPrefs.SetString("PlayerName", InputField.text);
        Debug.Log(PlayerPrefs.GetString("PlayerName", "visitor"));
        PlayerPrefs.SetInt("score", 0);
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
        PlayerPrefs.SetInt("switch", 1);
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
