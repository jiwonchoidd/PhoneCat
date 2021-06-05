using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanelopen : MonoBehaviour
{
    public GameObject Panel;
    public void togglepanel()
    {
        if(Panel !=null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }
}
