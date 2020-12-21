using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour
{
    
    public void OnPointerEnter()
    {
        if (gameObject.name == "PauseMenuButton")
        {
            GetComponentInChildren<Animator>().SetBool("Hover", true);
        }
    }

    public void OnPointerExit()
    {
        if (gameObject.name == "PauseMenuButton")
        {
            GetComponentInChildren<Animator>().SetBool("Hover", false);
        }
    }
}
