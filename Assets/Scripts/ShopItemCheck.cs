using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject datamanagerobj;
    public GameObject img;
    void Update()
    {
        checkhave();
    }
    void checkhave()
    {
        string objname = this.gameObject.name;
        bool a=datamanagerobj.GetComponent<DataManager>().ShopCheck(objname);
        if(a==true)
        {
            img.SetActive(true);
        }
        else
            img.SetActive(false);
    }
}
