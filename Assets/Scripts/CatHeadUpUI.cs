using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatHeadUpUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerinfoUI;
    public Text levelname_text;
    public GameObject headUpPos;
    public bool isMine;
    public GameObject interface_obj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerinfoUI.transform.position = headUpPos.transform.position;
        UITextput();


    }

    void UITextput()
    {
        if (isMine == true)
        {
            string myname = PlayerPrefs.GetString("name");
            int mylevel=PlayerPrefs.GetInt("statValue");

            levelname_text.text = "LV. " + Mathf.Floor(mylevel / 6f).ToString()+"\n"+myname;

        }
        else if (isMine == false)
        {
            string youname = interface_obj.GetComponent<Interface>().youname;
            int youlevel = interface_obj.GetComponent<Interface>().youlevel ;

            levelname_text.text = "LV. " + Mathf.Floor(youlevel / 6f).ToString() + "\n" + youname;

        }
    }
}
