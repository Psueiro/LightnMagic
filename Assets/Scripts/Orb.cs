using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Orb : MonoBehaviour
{
    public AnimationControllerCustom anim;

    private void Start()
    {
      //  anim = GetComponent<AnimationControllerCustom>();
      //  anim.ChangeAnimation("Idle");
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;                
            if (c.gameObject.layer == 8)
        {
            switch(sceneName)
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
}
