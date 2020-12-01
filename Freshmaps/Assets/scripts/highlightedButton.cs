using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class highlightedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool entered = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        entered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        entered = false;
    }
}
