using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LoadingSceneManager : MonoBehaviour
{
    public string myStoreUrl = "https://play.google.com/store/apps/details?id=com.NSU.choijiwon";
    public string pastebinUrl= "https://pastebin.com/raw/2q7ZdQe3";
    public string CurVersion; // ���� �������
    string latsetVersion; // �ֽŹ���
    public Slider slider;
    public string SceneName;
    private float time;
    // �� ������Ʈ Ȯ��
    // ���� Ȯ�� �Ұ�
    bool isSamePlayStoreVersion = false;
    //����Ƽ ��ü���� bundleIdentifier�� �������� ������, �̷��� ���� �� �� �ִ�.

    void Awake()
    {
        Application.targetFrameRate = 30;
    }

    void Start() {
        StartCoroutine(LoadTxtData());
        StartCoroutine(LoadAsynSceneCoroutine());   
    }

    public void VersionCheck()
    {
        Debug.Log("Current Version" + CurVersion + "Lastest Version" + latsetVersion);

        if (CurVersion == latsetVersion)
        {
            isSamePlayStoreVersion = true;
        }
        else
        {
            OpenURL();
            isSamePlayStoreVersion = false;
        }
    }
    IEnumerator LoadTxtData()
    {
        UnityWebRequest www = UnityWebRequest.Get(pastebinUrl);
        yield return www.SendWebRequest(); // ������ ��û
        if (www.isNetworkError)
        {
            Debug.Log("error get page");
        }
        else
        {
            latsetVersion = www.downloadHandler.text; // ���� �Էµ� �ֽŹ���
        }
        VersionCheck();
    }

    public void OpenURL() // ����� ����
    {
        Application.OpenURL(myStoreUrl);
    }

    IEnumerator LoadAsynSceneCoroutine() {

        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");

        operation.allowSceneActivation = false;

        while (!operation.isDone) {

            time =+ Time.time;

            slider.value = time / 3f;

            if(time > 3 && isSamePlayStoreVersion) {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

    }

}