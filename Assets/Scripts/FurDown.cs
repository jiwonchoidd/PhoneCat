using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurDown : MonoBehaviour
{

    public GameObject fur_obj;
    public GameObject firePos;
    public GameObject dataobj;
    GameObject instance_Fur;
    public AudioClip getFur_SFX;
    public AudioSource audioSource;
    private bool isfur=false;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        StartCoroutine(AutoFur());
    }

    // Update is called once per frame
    void Update()
    {


        //털을 클릭하면 털이 사라지며 돈을 얻는다. 
        //터치 량이 0보다 클때
        if (Input.touchCount > 0)
        {
            Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // 변환 안하고 바로 Vector3로
            Ray ray = Camera.main.ScreenPointToRay(theTouch);    // 터치한 좌표 레이로 바꾸기
            RaycastHit hit;    // 정보 저장할 구조체 만들고
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // 털이라는 태그를 가진 콜라이더에 닿은 레이캐스트
                if (hit.collider.CompareTag("Fur"))
                {

                    // 터치를 때면 작동합니다.
                    if(Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        FurGetit(hit);
                    }
                }
            }
        }


        /*        if(Input.GetMouseButton(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);   
                    if (Physics.Raycast(ray, out hit))
                    {
                        // 털이라는 태그를 가진 콜라이더에 닿은 레이캐스트
                        if (hit.collider.CompareTag("Fur"))
                        {

                            FurGetit(hit);

                        }
                    }
                }*/

    }

    public void FurDrop()
    {
        var instance = Instantiate(fur_obj, firePos.transform.position, firePos.transform.rotation);
        instance_Fur = instance;
        int leftorright = Random.Range(0, 2);
        if (leftorright == 1)
        {
            instance.GetComponent<Rigidbody>().AddForce(-transform.right * 250);
        }
        else
        {
            instance.GetComponent<Rigidbody>().AddForce(transform.right * 250);
        }
    }

    private IEnumerator AutoFur()
    {
        while (true)
        {
        int randomtime = Random.Range(2, 30);
        Debug.Log(randomtime+"초 뒤에 털이 나옵니다.");
        yield return new WaitForSeconds(randomtime);
        FurDrop();
        }
    }

    private void FurGetit(RaycastHit hit)
    {
        //구조체에 저장된 게임 오브젝트 즉, 선택된 오브젝트 삭제
   
        
        Destroy(hit.transform.gameObject);
        audioSource.clip = getFur_SFX;
        if (!audioSource.isPlaying)
            audioSource.Play();

        // 털을 수집하면 돈을 줍니다.
            StartCoroutine(FurMoney());

    }

    private IEnumerator FurMoney()
    {
        dataobj.GetComponent<DataManager>().money +=450;
        yield return new WaitForSeconds(1.5f);
        dataobj.GetComponent<DataManager>().coinAnimator.SetTrigger("isEarn");
        dataobj.GetComponent<DataManager>()._save();
        dataobj.GetComponent<DataManager>()._load();
    }

}
