using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animatorImage;
    public Animator animatorText;
    public TMP_Text popUpText;

    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        animatorImage.SetTrigger("pop");
        animatorText.SetTrigger("pop");
    }
}
