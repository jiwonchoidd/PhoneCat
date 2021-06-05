using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryself : MonoBehaviour
{
   public float DestroyTime = 15.0f;
    void Start ()
    {
        Destroy(gameObject, DestroyTime);  
    }
}
