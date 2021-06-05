using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{

    
    // 새 장면을로드하고 싶을 때마다 호출
    // 새 장면을 sceneHistory 목록에 추가.
    public void btn_change_scene(string scene_name)
    {
        // 고양이 위치값 저장
        SceneManager.LoadScene(scene_name);
    }

    // 이전 장면을로드하고 싶을 때마다 호출
    // 히스토리에서 현재 장면을 제거한 다음 기록에서 새 마지막 장면을로드합니다.
    // 이력에 이전 장면을 저장할만큼 장면 사이를 이동하지 않은 경우 false를 반환.

    }