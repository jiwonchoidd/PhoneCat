using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class textbling : MonoBehaviour
{
  Text flashingText;

    void Start()
    {
        flashingText = GetComponent<Text>();
        StartCoroutine (BlinkText());
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            flashingText.text = "";
            yield return new WaitForSeconds(.5f);
            flashingText.text = "아무곳이나 터치하여 시작";
            yield return new WaitForSeconds(.5f);
        }
    }
}
