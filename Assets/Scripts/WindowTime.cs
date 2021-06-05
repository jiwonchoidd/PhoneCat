using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTime : MonoBehaviour
{
    public GameObject dataobj;
    private new MeshRenderer window_renderer;
    public Material[] day;
    public Material[] highnoon;
    public Material[] night;
    private int nowtime;
    // Start is called before the first frame update
    void Start()
    {
        window_renderer = this.GetComponent<MeshRenderer>();
        StartCoroutine(WindowTimeC());
    }

    // Update is called once per frame

    IEnumerator WindowTimeC()
    {
        changeWindow();
        yield return new WaitForSeconds(30f);
    }

    void changeWindow()
    {
        nowtime=dataobj.GetComponent<DataManager>().nowTime_window();
        if(nowtime==0)
        {
        window_renderer.materials=day;
        }
        else if(nowtime==1)
        {
        window_renderer.materials=highnoon;
        }
        else
        {
        window_renderer.materials=night;
        }
        
    }
}
