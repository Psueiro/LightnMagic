using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    void Start ()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        switch (sceneName)
        {
            case "Intro":
            case "Credits":
            case "Options":
                SoundManager.instance.Play(SoundID.TITLE, true, 0.3f);
                break;
            case "Tutorial":
            case "Level":
                SoundManager.instance.Play(SoundID.LEVEL1, true, 0.3f);
                break;
            case "Level2":
                SoundManager.instance.Play(SoundID.LEVEL2, true, 0.3f);
                break;
            case "Level3":
                SoundManager.instance.Play(SoundID.LEVEL3, true, 0.3f);
                break;
        }
    }

    void Update ()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (Input.GetKeyDown(KeyCode.T) )
        {
            SceneManager.LoadScene("Intro");
        }

if (Input.GetKeyDown(KeyCode.Mouse0) && sceneName == "Preintro")
        {
            SceneManager.LoadScene("Intro");
        }


        if (Input.GetKeyDown(KeyCode.N))
        {
            switch (sceneName)
            {
                case "Tutorial":
                    SceneManager.LoadScene("Level");
                    break;
                case "Level":
                    SceneManager.LoadScene("Level2");
                    break;
                case "Level2":
                    SceneManager.LoadScene("Level3");
                    break;
                case "Level3":
                    SceneManager.LoadScene("Intro");
                    break;
            }
        }
    }
    public void Button(string text)
    {
        switch (text)
        {
            case "Start":
                SceneManager.LoadScene("Tutorial");
                break;
            case "Back":
                SceneManager.LoadScene("Intro");
                break;
            case "Credits":
                SceneManager.LoadScene("Credits");
                break;
            case "Options":
                SceneManager.LoadScene("Options");
                break;
            case "Quit":
                Application.Quit();
                break;
        }
    }
}
