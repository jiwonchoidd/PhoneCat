using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossFade : MonoBehaviour
{

    public Animator transition;
    public float transitontime = 1f;
    void Update()
    {
       
    }

    public void LoadNextLevel(int i)
    {
        StartCoroutine(Loadlevel(i));
    }

    IEnumerator Loadlevel(int levelIndex)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitontime);
        SceneManager.LoadScene(levelIndex);
    }
}
