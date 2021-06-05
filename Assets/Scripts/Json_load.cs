using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Json_load : MonoBehaviour 
{

    //public TextAsset ItemDatabase;
    public List<Item> AllItemList;
    string filePath;
    public GameObject[] invent;
    void Start()
    {
        filePath = Application.persistentDataPath + "/allitemdata.txt";
        Load();
        CheckHaveItem();
    }

    /*void Save()
    {

        string jdata = JsonUtility.ToJson(new Serialization<Item>(AllItemList));
        File.WriteAllText(filePath, jdata);
    }*/
    void Load()
    {
        //if(!File.Exists(filePath)) {ResetItemClick(); return; }

        string jdata = File.ReadAllText(filePath);
        AllItemList = JsonUtility.FromJson<Serialization<Item>>(jdata).target;
    }

    void CheckHaveItem()
    {
        for (int i = 1; i <= 6; i++)
        {
         string id = i.ToString();
         Item selItem = AllItemList.Find(x => x.id == id);
         if(selItem.isUsing=="1")
            {
                //보여지게 할 함수를 쓰던가해야지
                invent[i - 1].SetActive(false);
            }
        }
    }

    public void ButtonChangeAcc(int i)
    {
        GameObject acc = GameObject.FindWithTag("Acc"+i.ToString());
        
        if(acc.GetComponent<MeshRenderer>().enabled ==false)
        {
            acc.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        ResetAcc();

    }
    private void ResetAcc()
    {
        for (int i = 1; i <= 6; i++)
        {
            GameObject acc = GameObject.FindWithTag("Acc" + i.ToString());
            acc.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}