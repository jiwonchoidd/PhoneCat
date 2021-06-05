using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayLogic : MonoBehaviour
{
    public GameObject pullupbutton;
    //터치 제한 
    public int checktime = 0;
    // 제한 회전
    // Your bounds
    // How much to rotate by
    public float horizontalSpeed=2f;
    public float verticalSpeed=2f;
    public GameObject handle_fishing;
    public GameObject target_fishing;

    private Animator animator;
    private Animator ball_animator;
    private bool isCamera=false;
    public bool isThrow=false;
    public bool isThrow2 = false;
    private bool isselected=false;
    //아이템들 게임 오브젝트 등록
    public GameObject dataobj;
    public GameObject ball;
    public GameObject ball_fire;
    public GameObject mouse;
    public GameObject mouse_fire;
    public GameObject fishing;
    public GameObject playobj;
    // 카메라 로케이션 값 받아오기
    public GameObject cm2_Camera;
    public Transform FirePos;
    public int select_Toy=0;
    public float speed=300;
    public float upspeed=300;
    public GameObject instance_Playlogic;

    public GameObject cat; 
    //private GameObject instance;
    public bool hit = false;
    //public AudioClip yumyum;
    public AudioClip[] ad;
    public AudioSource audioSource;
    private float rotX;
    private float rotY;

    void Start()
    {
        animator = GetComponent<Animator>();
        //일단 장난감들 다 안보이게
        ball_animator=ball.GetComponent<Animator>();
        audioSource = cat.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //&& dataobj.GetComponent<DataManager>().ishungry==false
        if (select_Toy==1 )
        {
            ThrowBall();
        }

        if(select_Toy == 2)
        {
            ThrowMice();
        }
        if (select_Toy == 3)
        {
            FishingMoving();
        }

        if (isThrow == true)
        {
            //공을 쫓아가라!!
            StartCoroutine(catgoPlay());
        }
        if (isThrow2 == true)
        {
            //공을 쫓아가라!!
            StartCoroutine(catgoPlay_Fishing());
        }
    }


    //볼 들고 있을때 터치한후 때어내면 발사가 되는 것
    void ThrowBall()
    {

        //터치 량이 0보다 클때
        if (Input.touchCount > 0 && !IsPointerOverUIObject(Input.mousePosition))
        {
            Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // 변환 안하고 바로 Vector3로
            Ray ray = Camera.main.ScreenPointToRay(theTouch);    // 터치한 좌표 레이로 바꾸엉
            RaycastHit hit;    // 정보 저장할 구조체 만들고
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // 캣이라는 태그를 가진 콜라이더에 닿은 레이캐스트
                if (hit.collider.CompareTag("Toy") )
                {
                    if (select_Toy == 1)
                    {
                        pullupbutton.SetActive(false);
                        ball_animator.SetBool("toggle", true);
                        float timer = 0f;
                        timer += Time.deltaTime;
                        speed = speed + (timer * 200);
                        upspeed = upspeed + (timer * 67);
                        if (Input.GetTouch(0).phase == TouchPhase.Ended)
                        {
                            select_Toy = 0;
                            Reset_Toybtn();
                            ball_animator.SetBool("toggle", false);
                            Fire_Ball();
                            //play 스크립트에서 false로 만들어줘야함.
                            isThrow = true;
                            ChangeCamera();
                        }
                    }
                }
            }
        }
    }

    void ThrowMice()
        {

            //터치 량이 0보다 클때
            if (Input.touchCount > 0 && !IsPointerOverUIObject(Input.mousePosition))
            {
                Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치
                Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // 변환 안하고 바로 Vector3로
                Ray ray = Camera.main.ScreenPointToRay(theTouch);    // 터치한 좌표 레이로 바꾸엉
                RaycastHit hit;    // 정보 저장할 구조체 만들고
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // 캣이라는 태그를 가진 콜라이더에 닿은 레이캐스트
                    if (hit.collider.CompareTag("Toy2"))
                    {
                        if (select_Toy == 2)
                        {
                        pullupbutton.SetActive(false);
                        float timer = 0f;
                            timer += Time.deltaTime;
                            speed = speed + (timer * 200);
                            upspeed = upspeed + (timer * 62);
                            
                            if (Input.GetTouch(0).phase == TouchPhase.Ended)
                            {
                                select_Toy = 0;
                                Reset_Toybtn();
                                Fire_Mice();
                                //play 스크립트에서 false로 만들어줘야함.
                                isThrow = true;
                                ChangeCamera();
                        }
                        }
                    }
                }
            }

        }

    void FishingMoving()
    {
        /* if (Input.GetMouseButton(0))
         {
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             RaycastHit hit;
             if (Physics.Raycast(ray, out hit))
             {
                 // 털이라는 태그를 가진 콜라이더에 닿은 레이캐스트
                 if (hit.collider.CompareTag("Toy3"))
                 {
                     if (select_Toy == 3)
                     {
                         isThrow2 = true;
                         float h = horizontalSpeed * Input.GetAxis("Mouse X");
                         float v = verticalSpeed * Input.GetAxis("Mouse Y");
                         rotX += h * Time.deltaTime;
                         rotY += v * Time.deltaTime;
                         // rotY의 값을 -60도 ~ 60도 사이로 제한한다.
                         rotX = Mathf.Clamp(rotX, -30f,30f);
                         rotY = Mathf.Clamp(rotY, 80f, 120f);
                         // 회전 벡터(오일러 각)를 만든다.
                         Vector3 dir = new Vector3(-rotY, rotX, 90);
                         handle_fishing.transform.localEulerAngles = dir;
                     }
                 }

             }
         }*/

        //터치 량이 0보다 클때
        if (Input.touchCount > 0 && !IsPointerOverUIObject(Input.mousePosition))
        {
            Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // 변환 안하고 바로 Vector3로
            Ray ray = Camera.main.ScreenPointToRay(theTouch);    // 터치한 좌표 레이로 바꾸엉
            RaycastHit hit;    // 정보 저장할 구조체 만들고

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // 캣이라는 태그를 가진 콜라이더에 닿은 레이캐스트
                if (hit.collider.CompareTag("Toy3"))
                {
                    if (select_Toy == 3)
                    {
                        pullupbutton.SetActive(false);
                        isThrow2 = true;
                        float h = horizontalSpeed * Input.GetAxis("Mouse X");
                        float v = verticalSpeed * Input.GetAxis("Mouse Y");
                        rotX += h * Time.deltaTime;
                        rotY += v * Time.deltaTime;
                        // rotY의 값을 -60도 ~ 60도 사이로 제한한다.
                        rotX = Mathf.Clamp(rotX, -30f, 30f);
                        rotY = Mathf.Clamp(rotY, 80f, 120f);
                        // 회전 벡터(오일러 각)를 만든다.
                        Vector3 dir = new Vector3(-rotY, rotX, 90);
                        handle_fishing.transform.localEulerAngles = dir;
                        //fishing.transform.RotateAround(handle_fishing.transform.position, Vector3.right, -v * Time.deltaTime);
                        //fishing.transform.RotateAround(handle_fishing.transform.position, Vector3.up, h * Time.deltaTime);
                        //리셋되자
                        if (Input.GetTouch(0).phase == TouchPhase.Began && checktime == 0 && checktime == 0)
                        {
                            isThrow2 = true;
                            checktime = 1;
                        }
                        /*else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                        {
                            *//*//리셋되자
                            Quaternion origin = Quaternion.Euler(-90f, 0f, 90);
                            fishing.transform.rotation = Quaternion.Lerp(fishing.transform.rotation, origin, Time.deltaTime * 100);
                            fishing.transform.localPosition = new Vector3(0f, -0.88f, 1.76f);*//*
                        }*/
                    }
                }
            }
        }
    }

    void Fire_Ball()
    {
        var instance = Instantiate(ball_fire,FirePos.transform.position, FirePos.transform.rotation);
        instance_Playlogic=instance;
        int leftorright = Random.Range(0,2);
        if(leftorright==1)
        {
            instance.GetComponent<Rigidbody>().AddForce(-transform.right*150);
        }
        else
        {
            instance.GetComponent<Rigidbody>().AddForce(transform.right*150);
        }
        instance.GetComponent<Rigidbody>().AddForce(transform.up*upspeed);
        instance.GetComponent<Rigidbody>().AddRelativeForce(-transform.forward*speed);
        //다시 스피드 초기화
        speed=500;
        upspeed=300;
    }
    void Fire_Mice()
    {
        var instance = Instantiate(mouse_fire, FirePos.transform.position, FirePos.transform.rotation);
        instance_Playlogic = instance;
        int leftorright = Random.Range(0, 2);
        if (leftorright == 1)
        {
            instance.GetComponent<Rigidbody>().AddForce(-transform.right * 150);
        }
        else
        {
            instance.GetComponent<Rigidbody>().AddForce(transform.right * 150);
        }
        instance.GetComponent<Rigidbody>().AddForce(transform.up * upspeed);
        instance.GetComponent<Rigidbody>().AddRelativeForce(-transform.forward * speed);
        //다시 스피드 초기화
        speed = 500;
        upspeed = 300;
    }

    IEnumerator catgoPlay()
    {
        cat.GetComponent<RandomMove>().setIdle();
        cat.GetComponent<RandomMove>().enabled = false;
        Vector3 target = new Vector3(instance_Playlogic.transform.position.x, 0.48f, instance_Playlogic.transform.position.z);
        yield return new WaitForSeconds(1f);
        while (hit == false && isThrow == true)
        {
            cat.transform.LookAt(target);
            cat.GetComponent<RandomMove>().animator.SetBool("ismove2", true);
            cat.transform.position = Vector3.MoveTowards(cat.transform.position, target, 0.06f * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator catgoPlay_Fishing()
    {
        cat.GetComponent<RandomMove>().setIdle();
        cat.GetComponent<RandomMove>().enabled = false;
        Vector3 target = new Vector3(target_fishing.transform.position.x, 0.48f, target_fishing.transform.position.z);
        yield return new WaitForSeconds(1f);
        while (hit == false && isThrow2 == true)
        {
            //cat.transform.LookAt(target);
            Quaternion OriginalRot = cat.transform.rotation;
            cat.transform.LookAt(target);
            Quaternion NewRot = cat.transform.rotation;
            transform.rotation = OriginalRot;
            transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, speed * Time.deltaTime);
            //
            cat.GetComponent<RandomMove>().animator.SetBool("ismove2", true);
            cat.transform.position = Vector3.MoveTowards(cat.transform.position, target, 0.06f * Time.deltaTime);
            yield return null;
        }
    }

    //버튼에 넣을 선택된거 게임오브젝트 true로 만듬
    public void Select_Toybtn(int toy)
    {
        if(toy==1)
        {
            // ball
            Reset_Toybtn();
            ball.SetActive(true);
            select_Toy=toy;
            isselected=true;
            ball_animator.SetBool("hold", true);
        }
        else if(toy==2)
        {
            // mouse
            Reset_Toybtn();
            mouse.SetActive(true);
            select_Toy=toy;
            isselected = true;
        }
        else if(toy==3)
        {
            // fishing
            Reset_Toybtn();
            fishing.SetActive(true);
            select_Toy=toy;
            isselected = true;
        }
    }

    public void Reset_Toybtn()
    {
        select_Toy=0;
        if(ball.activeSelf == true)
        ball.SetActive(false);
        if(mouse.activeSelf == true)
        mouse.SetActive(false);
        if(fishing.activeSelf == true)
        fishing.SetActive(false);
    }


    //버튼에 넣을 것입니다. 카메라 구도를 바꿔줍니다.
    // 놀잇감과 부딪혔을때 애니메이션을 보여줌 카메라 전환
    public void ChangeCamera()
    {
        if(isCamera==false)
        {
        animator.SetBool("food", true);
        isCamera=true;
        }
        else
        {
        animator.SetBool("food", false);
        isCamera=false;
        }
    }

    public void CameraChangeColl()
    {
        StartCoroutine(CameraChange());
    }
    IEnumerator CameraChange()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("play", true);
        yield return new WaitForSeconds(5f);
        animator.SetBool("play", false);
    }
    /*    IEnumerator catPlay()
        {

            Vector3 target = new Vector3(instance_Playlogic.transform.position.x, 0.48f, instance_Playlogic.transform.position.z);
            transform.LookAt(target);
            this.GetComponent<RandomMove>().animator.SetBool("ismove2", false);
            yield return new WaitForSeconds(0.2f);
            this.GetComponent<RandomMove>().animator.SetTrigger("jump");
            // audioSource.clip = yumyum;
            // if (!audioSource.isPlaying)
            //     audioSource.Play();
            yield return new WaitForSeconds(7f);
            isThrow = false;
            cat.GetComponent<RandomMove>().enabled = true;
            hit = false;
            //친밀도를 올려줌 사료마다 양 다르게 하자
            dataobj.GetComponent<DataManager>().IncreaseProgress();
        }
    */

    public bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition
            = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = touchPos;

        List<RaycastResult> results = new List<RaycastResult>();


        EventSystem.current
        .RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
    public void Meow()
    {
        audioSource.clip = ad[Random.Range(0, ad.Length)];
        audioSource.Play();
    }

}
