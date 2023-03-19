using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TextChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.magenta;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.white;
    }
}
