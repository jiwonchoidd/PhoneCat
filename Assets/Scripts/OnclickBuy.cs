using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OnclickBuy : MonoBehaviour
{
    public GameObject dataobj;
    public string objid;
    public Button btn;
    public Text buyOrEquip;
    Animator catAnimator;




    // Start is called before the first frame update
    void Start()
    {

        btn = this.transform.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(purchaseClick); //스크립트로 버튼 이벤트 수행
        }

    }
    void Update()
    {
        objid = this.gameObject.name;
        string a = dataobj.GetComponent<DataManager>().Finditem_text(objid);
        buyOrEquip.text = "" + a;
    }
    void purchaseClick()
    {
        objid = this.gameObject.name;
        dataobj.GetComponent<DataManager>().Finditem(objid);
       
    }

   
}