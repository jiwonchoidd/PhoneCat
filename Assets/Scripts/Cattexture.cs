using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cattexture : MonoBehaviour
{
    public Material bangal;
    public Material bangalgray;
    public Material black;
    public Material doopcheese;
    public Material doopcheesegray;
    public Material russian;
    public Material siam;
    public Material tuksido;
    public Material white;
    public Material eye_orange;
    public Material eye_blue;
    private new SkinnedMeshRenderer renderer;
    private SkinnedMeshRenderer renderer2;
    private SkinnedMeshRenderer renderer3;
    public GameObject eyeL;
    public GameObject eyeR;
    public GameObject body;
    public int skin;
    static public bool isgiveskin=false;
  private void Start() 
    {
        renderer = body.GetComponent<SkinnedMeshRenderer>();
        renderer2= eyeL.GetComponent<SkinnedMeshRenderer>();
        renderer3= eyeR.GetComponent<SkinnedMeshRenderer>();
        skinAdapt(); 
    }

    void Update()
    {
        if(isgiveskin==true)
        {
           skinAdapt(); 
           isgiveskin=false;
        }
        
    }
    public void changemet()
    {
        PlayerPrefs.DeleteKey("skin");
        skin = ++skin;
        if(skin>=18)
        {
            skin = 0;
        }
        PlayerPrefs.SetInt("skin", skin);
        PlayerPrefs.Save();
          skinAdapt(); 
    }
    public void skinAdapt(){
        skin=PlayerPrefs.GetInt("skin");
        if (skin == 0)
        {
            renderer.material = bangal;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 1)
        {
            renderer.material = bangalgray;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 2)
        {
            renderer.material = black;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 3)
        {
            renderer.material = doopcheese;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 4)
        {
            renderer.material = doopcheesegray;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 5)
        {
            renderer.material = russian;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 6)
        {
            renderer.material = siam;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 7)
        {
            renderer.material = tuksido;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 8)
        {
            renderer.material = white;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 9)
        {
            renderer.material = bangal;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 10)
        {
            renderer.material = bangalgray;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 11)
        {
            renderer.material = black;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 12)
        {
            renderer.material = doopcheese;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 13)
        {
            renderer.material = doopcheesegray;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 14)
        {
            renderer.material = russian;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 15)
        {
            renderer.material = siam;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 16)
        {
            renderer.material = tuksido;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 17)
        {
            renderer.material = white;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
    }
   
}
