using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelopen : MonoBehaviour
{
    public GameObject panel;
    public GameObject panel1;

    public void openPanel()
    {
        if(panel != null)
        {
            panel.SetActive(true);
        }
    }
    public void closePanel()
    {
        panel1.SetActive(false);
    }
}
