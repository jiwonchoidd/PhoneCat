using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshots : MonoBehaviour
{
    [SerializeField]
    GameObject blink;
    public Animator shotAnimator;
    public GameObject shotAnime;
    public Canvas menu;

    public void Take_A_Shot()
    {
        StartCoroutine("CaptureIt");
    }
    IEnumerator CaptureIt()
    {
 
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "screenshot" + timeStamp + ".png";
        string pathToSave = fileName;
        yield return new WaitForSeconds(0.3f);
        menu.enabled = false;
        yield return new WaitForSeconds(0.3f);
        ScreenCapture.CaptureScreenshot(pathToSave);
        yield return new WaitForEndOfFrame();
        shotAnimator.SetTrigger("isShot");
        yield return new WaitForSeconds(0.3f);
        menu.enabled = true;

        // Instantiate(blink, new Vector2(0f, 0f), Quaternion.identity);
    }
}
