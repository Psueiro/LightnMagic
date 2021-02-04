using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DifficultyCheck : MonoBehaviour
{
    public int difficulty;

    public Dropdown dd;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Options")
        {
            dd = GameObject.Find("Dropdown").GetComponent<Dropdown>();
            Difficulty();
        }
    }

    public void Difficulty()
    {
        switch(dd.value)
        {
            case 0:
                difficulty = 0;
                break;
            case 1:
                difficulty = 1;
                break;
            case 2:
                difficulty = 2;
                break;
        }

        Globales.diff = difficulty;
    }
}
