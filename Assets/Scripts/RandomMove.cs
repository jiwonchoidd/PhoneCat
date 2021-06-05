using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class RandomMove : MonoBehaviour
{
    [SerializeField] ParticleSystem particleObject;
    [SerializeField] ParticleSystem particleObject2;
    [SerializeField] ParticleSystem particleObject3;
    private bool randommove=true;
    public Vector3 catPos;
    //public Text testText;
    //애니메이션
    public Animator animator;
    private bool istickle= false;
    public bool iscatcome=false;
    //

    private Touch touch;
    private float speedModifier;

    public float moveSpeed=3f;
    public float rotSpeed=100f;
    private bool isWandering =false;
    private bool isRotationLeft=false;
    private bool isRotationRight=false;
    private bool isWalking=false;
    private bool isGrab=false;
    private bool isIdle=false;
    private bool isSitdown=false;
    private bool isSitdown2=false;
    private bool isSitdown3=false;

    private int cattouchcount=0;
    float timer=0;    
    // 사운드 
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip[] ad;
    public AudioClip grabsound;
    public AudioClip pursound;
    public AudioClip callsound;
    public AudioClip whatsound;
    public AudioClip hungrysound;
    //public AudioClip walksound;
    // Start is called before the first frame update
    void Start()
    {
        speedModifier=0.015f;
        audioSource=GetComponent<AudioSource>();
        audioSource2=GetComponent<AudioSource>();
        animator=GetComponent<Animator>();
        setPos();
    }

    void Update()
    {
        //터치 량이 0보다 클때
         if(Input.touchCount > 0)
         {
            Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // 변환 안하고 바로 Vector3로
            Ray ray = Camera.main.ScreenPointToRay(theTouch);    // 터치한 좌표 레이로 바꾸엉
            RaycastHit hit;    // 정보 저장할 구조체 만들고
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
        // 캣이라는 태그를 가진 콜라이더에 닿은 레이캐스트
        if(hit.collider.CompareTag("cat"))
            {
            
            //randommove=false;
		    timer += Time.deltaTime;
            //testText.text=""+timer;
            // 누르고 있으면 호출이 됩니다. 3초 이상
          
                if(timer>3f && timer<4f)
                {
                iscatcome=true;
                if(iscatcome==true)
                {
                    StopCoroutine(Wander());
                    setIdle();   
                    StartCoroutine(catCome());
                }
                }
                    //Grab();
                    //if (!audioSource.isPlaying)
                    //Meow();
                    // .5초 이상 누르면 좋아하는 tickle 동작
                    if(timer>0.5f&&timer<2&&isGrab==false)
                    Tickle();

                    // 한 4번 터치 하면 이쪽 보게함
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        cattouchcount++;
                    }

                    if(cattouchcount>4)
                    {
                        setIdle();
                        LookAtMe();
                        cattouchcount = 0;
                    }

                }    
            
        }   
        }
        //터치 량이 0일때
      else if(Input.touchCount==0){
          StopCoroutine(catCome());
            isGrab=false;
            timer=0;
            istickle=false;
            animator.SetBool("isgrab", false);
            randommove=true;
         } 
           

// 헤매는 코드 코루틴함수사용     
if(randommove==true && iscatcome==false)
{
        if(isWandering == false)
        {
        StartCoroutine(Wander());
        }
        if(isRotationRight==true)
        {
            animator.SetBool("ismove", true);
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
            transform.position +=transform.forward*moveSpeed * Time.deltaTime;

        }
         if(isRotationLeft==true)
        {
            animator.SetBool("ismove", true);
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            transform.position +=transform.forward*moveSpeed * Time.deltaTime;
        }
        if(isWalking==true){
            //음쥑이는 모션~!
            animator.SetBool("ismove", true);
            transform.position +=transform.forward*moveSpeed * Time.deltaTime;
        }
        if(isIdle==true){
            animator.SetBool("isidle", true);
        }
        if(isSitdown==true){
            animator.SetBool("issit", true);
        }
        if(isSitdown2==true){
             animator.SetBool("issit2", true);
        }
         if(isSitdown3==true){
             animator.SetBool("issit3", true);
        }
          
        }
}
    IEnumerator Wander()
    {
        int rotTime =Random.Range(1,2);
        int rotateWait=Random.Range(1,3);
        int rotateLorR=Random.Range(0,7);
        int walkWait = Random.Range(1,3);
        int walkTime = Random.Range(4,10);
        int idletime = Random.Range(2,8);
        int sitdowntime = Random.Range(6,10);

        isWandering=true;

        yield return new WaitForSeconds(walkWait);
             if (!audioSource.isPlaying)
                Meow();
        isWalking = true;
        //WalkSound();
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        //애니메이션 끄기
        animator.SetBool("ismove", false);
        //
        yield return new WaitForSeconds(rotateWait);

        if(rotateLorR==1){
            isRotationRight=true;
            //WalkSound();
            yield return new WaitForSeconds(rotTime);
             animator.SetBool("ismove", false);
            isRotationRight=false;
        }
         if(rotateLorR==2){
            isRotationLeft=true;
            //WalkSound();
            yield return new WaitForSeconds(rotTime);
            animator.SetBool("ismove", false);
            isRotationLeft=false;
        }
         if(rotateLorR==3){
            isIdle=true;
            yield return new WaitForSeconds(idletime);
            animator.SetBool("isidle", false);
            isIdle=false;
        }
         if(rotateLorR==4){
            if (!particleObject3.isPlaying)
            {
                particleObject3.Play(); // 넣어봉쓰
            }
            if (!audioSource.isPlaying)
                Meow();
            isSitdown=true;
            yield return new WaitForSeconds(sitdowntime);
            animator.SetBool("issit", false); 
            isSitdown =false;
        }
         if(rotateLorR==5){
            if (!particleObject3.isPlaying)
            {
                particleObject3.Play(); // 넣어봉쓰
            }
            if (!audioSource.isPlaying)
                Meow();
            isSitdown2=true;
            yield return new WaitForSeconds(sitdowntime);
            animator.SetBool("issit2", false);
            isSitdown2 =false;
        }
        if(rotateLorR==6){
            if (!particleObject2.isPlaying)
            {
                particleObject2.Play(); // 넣어봉쓰
            }
            if (!audioSource.isPlaying)
                Meow();
            isSitdown3=true;
            yield return new WaitForSeconds(sitdowntime);
            animator.SetBool("issit3", false); 
            isSitdown3 =false;
        }
        isWandering = false;
    }

    void Meow(){
        audioSource.clip = ad[Random.Range(0, ad.Length)];
        audioSource.Play();
    }
    void Grab(){
        touch=Input.GetTouch(0);
        if(Input.touchCount==1){
            isGrab=true;
            if(isGrab==true)
            {
            audioSource.clip =grabsound;
            audioSource.Play();
            animator.SetBool("isgrab", true);
            //transform.localScale =new Vector3(2, 2, 2);    
            transform.position=new Vector3(transform.position.x +touch.deltaPosition.x
            *speedModifier,transform.position.y,transform.position.z+touch.deltaPosition.y*
            speedModifier);
            }
    }}
    void Tickle(){
      
        touch=Input.GetTouch(0);
        if(Input.touchCount==1){
            istickle=true;
            if(touch.phase==TouchPhase.Moved)
            {
             animator.SetTrigger("ishappy");
              audioSource.clip = pursound;
            if (!audioSource.isPlaying)
            audioSource.Play();
            }
       
        }
    }

    void LookAtMe()
    {
        Vector3 target2 = new Vector3(0, 0.48f, -4f);
        transform.LookAt(target2);
    }
    IEnumerator catCome()
    {   
        iscatcome= true;
        audioSource.clip = callsound;
        if (!audioSource.isPlaying)
        audioSource.Play();
        StopCoroutine(Wander());
        setIdle();
        particleemotion();
        yield return new WaitForSeconds(3f); 
        
        if (!audioSource.isPlaying){
                audioSource.clip =whatsound;
            audioSource.Play();
        }
        
        Vector3 target = new Vector3(0, 0.48f, -1.7f);
        Vector3 target2 = new Vector3(0, 0.48f, -2f);
        randommove =false;
       
        while(transform.position!=target)
        {  

                StopCoroutine(Wander());
                transform.LookAt(target2);
                
                //target.y =transform.position.y;
                //target.z =transform.position.z;            
                //transform.LookAt(target);
                //Vector3 velo = Vector3.zero;
                animator.SetBool("ismove2", true);
                //target = target - target;
                target.Normalize();
                target = new Vector3(target.x, 0.48f, target.z);
	            transform.position = Vector3.MoveTowards(gameObject.transform.position, target, 0.06f * Time.deltaTime);
                //WalkSound();
                yield return null;
        }
         animator.SetBool("ismove2", false);
         iscatcome=false;
    }


    void OnCollisionEnter (Collision collision)
    {
        if(collision.collider.CompareTag("Wall"))
        {
	    Debug.Log ("벽에 부딪힘");
        Debug.Log (collision.gameObject);
        transform.Rotate (0, 180, 0);
        if (!audioSource.isPlaying)
        Meow();
        }
    }

    public void setIdle(){
        //모든 애니메이션을 false로
        StopCoroutine(Wander());
        animator.SetBool("ismove", false);
        animator.SetBool("issit3", false);
        animator.SetBool("issit2", false);
        animator.SetBool("issit", false);
        animator.SetBool("ismove2", false);
        isSitdown3=false;
        isSitdown2=false;
        isSitdown=false;
        isIdle=false;
        isWalking=false;
        isRotationLeft=false;
        isRotationRight=false;
        randommove=false;
    }
    public void particleemotion(){
      if(!particleObject.isPlaying){
          particleObject.Play();
       }     
    }
    /*public void WalkSound()
    {
         audioSource2.clip = walksound;
            if (!audioSource2.isPlaying)
            audioSource2.Play();
    }*/
    //위치값 저장
    public void getPos()
    {
        catPos = transform.position;
        Debug.Log(catPos.x);
        Debug.Log(catPos.y);
        Debug.Log(catPos.z);
        SetVector3("pos",catPos);
    }
     public static void SetVector3(string key, Vector3 value)
    {
        PlayerPrefs.SetFloat(key + "X", value.x);
       // PlayerPrefs.SetFloat(key + "Y", value.y);
        PlayerPrefs.SetFloat(key + "Z", value.z);
        PlayerPrefs.Save();
    }
    //위치값 불러오기
    public void setPos()
    {
                catPos=GetVector3("pos");
                transform.position=catPos;
                Debug.Log(catPos);
        
    }
        public static Vector3 GetVector3(string key)
    {
        Vector3 v3 = Vector3.zero;
        v3.x = PlayerPrefs.GetFloat(key + "X");
        v3.y = 0.48f;
        v3.z = PlayerPrefs.GetFloat(key + "Z");
        return v3;
    }

    public void Hungry_Sound()
    {
        StartCoroutine(Hungry_Sound_Delay());
    }
    
    IEnumerator Hungry_Sound_Delay()
    {
        audioSource.clip = hungrysound;
        if(!audioSource.isPlaying)
        audioSource.Play();
        yield return new WaitForSeconds(6f);

    }

}
