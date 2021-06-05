using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panel_open_anime : MonoBehaviour
{
    public GameObject panel;
    private Animator anime;

    public void OpenPanel()
    {
            Animator animator = panel.GetComponent<Animator>();
    }

    public void ClosePanel()
    {
        Animator animator = panel.GetComponent<Animator>();
    }

}
