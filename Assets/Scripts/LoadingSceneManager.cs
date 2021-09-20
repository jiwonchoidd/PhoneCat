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
    public string CurVersion; // 현재 빌드버전
    string latsetVersion; // 최신버전
    public Slider slider;
    public string SceneName;
    private float time;
    // 앱 업데이트 확인
    // 버젼 확인 불값
    bool isSamePlayStoreVersion = false;
    //유니티 자체에서 bundleIdentifier를 읽을수도 있지만, 이렇게 읽을 수 도 있다.

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
        yield return www.SendWebRequest(); // 페이지 요청
        if (www.isNetworkError)
        {
            Debug.Log("error get page");
        }
        else
        {
            latsetVersion = www.downloadHandler.text; // 웹에 입력된 최신버전
        }
        VersionCheck();
    }

    public void OpenURL() // 스토어 열기
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