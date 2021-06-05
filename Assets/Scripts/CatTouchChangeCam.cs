using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTouchChangeCam : MonoBehaviour
{
    public GameObject cam_obj;
    public Animator cam;
    private int cattouchcount=0;
    // Start is called before the first frame update
    void Start()
    {
        cam = cam_obj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //터치 량이 0보다 클때
        if (Input.touchCount > 0)
        {
            Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // 변환 안하고 바로 Vector3로
            Ray ray = Camera.main.ScreenPointToRay(theTouch);    // 터치한 좌표 레이로 바꾸엉
            RaycastHit hit;    // 정보 저장할 구조체 만들고
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // 캣이라는 태그를 가진 콜라이더에 닿은 레이캐스트
                if (hit.collider.CompareTag("cat"))
                {
                    // 한 4번 터치 하면 이쪽 보게함
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        cattouchcount++;
                    }

                    if (cattouchcount > 4)
                    {
                        CamTrigger();
                        cattouchcount = 0;
                    }

                }

            }
        }

    }

    public void CamTrigger()
    {
        if(!cam.GetBool("iszoom"))
        cam.SetBool("iszoom",true);
        else
            cam.SetBool("iszoom", false);
    }

}
