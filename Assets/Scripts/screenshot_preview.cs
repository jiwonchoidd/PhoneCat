using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class screenshot_preview : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject basic_img;
    Sprite defaultImage;
    string[] files = null;
    int whichScreenShotIsShown = 0;

    private void Start()
    {
        files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
        if (files.Length > 0)
        {
            GetPicturetureAndShowIt();
        }
    }
    private void Update()
    {
        if(files.Length ==0)
        {
            basic_img.SetActive(true);
        }
    }




    void GetPicturetureAndShowIt()
    {
        string pathToFile = files[whichScreenShotIsShown];
        Texture2D texture = GetScreenshotImage(pathToFile);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        canvas.GetComponent<Image>().sprite = sp;
    }

    Texture2D GetScreenshotImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists(filePath))
        {
            fileBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
            texture.LoadImage(fileBytes);
        }
        return texture;
    }

    public void DeleteImage()
    {
        if (files.Length > 0)
        {
            string pathToFile = files[whichScreenShotIsShown];
            if (File.Exists(pathToFile))
                File.Delete(pathToFile);
            files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
            if (files.Length > 0)
                NextPicture();
            else
                canvas.GetComponent<Image>().sprite = defaultImage;
        }
    }



    public void NextPicture()
    {
        if (files.Length > 0)
            whichScreenShotIsShown += 1;
        if (whichScreenShotIsShown > files.Length - 1)
            whichScreenShotIsShown = 0;
        GetPicturetureAndShowIt();
    }
    public void PreviousPicture()
    {
        if (files.Length > 0)
        {
            whichScreenShotIsShown -= 1;
            if (whichScreenShotIsShown < 0)
                whichScreenShotIsShown = files.Length - 1;
            GetPicturetureAndShowIt();
        }
    }
}
