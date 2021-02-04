using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IconChange : MonoBehaviour
{
    public Mage pm;
    public AnimationControllerCustom anim;

    public Sprite Fire;
    public Sprite Ice;
    public Sprite Lightning;
    // Use this for initialization
    void Start ()
    {

        this.GetComponent<Image>().sprite = Fire;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (pm.SpellMode == 1)
        {
            this.GetComponent<Image>().sprite = Fire;
        }
        else if (pm.SpellMode == 2)
        {
            this.GetComponent<Image>().sprite = Ice;
        }
        else this.GetComponent<Image>().sprite = Lightning;

    }
}
