using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Feed : MonoBehaviour
{
    //eat
    [SerializeField] ParticleSystem particleObject_EatEffect; 
    public GameObject dataobj;
    public AudioClip yumyum;
    public AudioClip meow;
    public AudioSource audioSource;
    public GameObject cat;
    private Animator animator_cat;
    //
    private int amount1;
    private int amount2;
    private int amount3;
    public GameObject food7_display;
    public GameObject food8_display;
    public GameObject food9_display;
    public GameObject foodspawn;
    public GameObject food7_obj;
    public GameObject food8_obj;
    public GameObject food9_obj;
    public GameObject putArea;
    private bool isselected=false;
    private int selectedfood;
    public float speedModifier=0.5f;
    private Touch touch;
    private Animator animator;
    private Animator animator2;
    private bool once=false;
    public bool ate=false;
    public GameObject askFeedPanel;
    public Text askFeedPanel_text;
    int yummy;
    public GameObject noTouch;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator2 = foodspawn.GetComponent<Animator>();
        audioSource=cat.GetComponent<AudioSource>();
        animator_cat=cat.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //만약 개수가 0개이면 보이지 않게 해주자
        if(this.GetComponent<FoodAmount>().food7.text=="0")
            food7_display.SetActive(false);
        else
            food7_display.SetActive(true);
        
        if(this.GetComponent<FoodAmount>().food8.text=="0")
            food8_display.SetActive(false);
        else
            food8_display.SetActive(true);
    
        if(this.GetComponent<FoodAmount>().food9.text=="0")
            food9_display.SetActive(false);
        else
            food9_display.SetActive(true);
        ///////////////////////////////////////////////////////////////////////////////////////////


        // 음식 종류 하나 선택했음
        if(isselected==true)
        {
            
                if(once==true)
                    {
                    int amount = PlayerPrefs.GetInt("food"+selectedfood);    
                    PlayerPrefs.SetInt("food"+selectedfood, amount-1);
                    once=false;
                    PlayerPrefs.Save();
                    animator2.SetBool("move", true);
                    }
                
        }        

        else
        {
            animator.SetBool("food", false);
            animator2.SetBool("move", false);
        }

    }

    public void useFood_btn(string food)
    {
        if(isselected==false)
        {
        animator2.SetBool("move", false);
        foodspawn.SetActive(true);
        if(food=="7")
        {
            askFeedPanel.SetActive(true);
            askFeedPanel_text.text="일반 사료가 선택되었습니다.";
            isselected=true;
            selectedfood=7;
            once=true;
        }
        if(food=="8")
        {
            askFeedPanel.SetActive(true);
            askFeedPanel_text.text="고급 사료가 선택되었습니다.";
            isselected=true;
            selectedfood=8;
            once=true;
        }
        if(food=="9")
        {
            askFeedPanel.SetActive(true);
            askFeedPanel_text.text="프리미엄 사료가 선택되었습니다.";
            isselected=true;
            selectedfood=9;
            once=true;
        }
        }
        else
        {
        //선택되었는데 또 누르면
        //resetfood();
        animator2.SetBool("move", false);
        }
    }

    public void askfeedokay()
    {
        animator.SetBool("food", true);
        putArea.SetActive(false);
        if(selectedfood==7)
        food7_obj.SetActive(true);
        if(selectedfood==8)
        food8_obj.SetActive(true);
        if(selectedfood==9)
        food9_obj.SetActive(true);
        Eat(selectedfood);
        noTouch.SetActive(true);
    }

    void Eat(int sf)
    {
        if(sf==7)
        {
     
            StartCoroutine(CatEat(sf));
        }
        else if(sf==8)
        {
           
            StartCoroutine(CatEat(sf));
        }
        else if(sf==9)
        {
            
            StartCoroutine(CatEat(sf));
        }
    }



public void resetfood()
{
        foodspawn.SetActive(false);
        isselected=false;
        food7_obj.SetActive(false);
        food8_obj.SetActive(false);
        food9_obj.SetActive(false);
        putArea.SetActive(true);
        animator2.SetBool("move", false);
}

IEnumerator CatEat(int sf)
{
    //냥 하고 0.2 초 기다린다음에
    audioSource.clip = meow;
    if (!audioSource.isPlaying)
    audioSource.Play();
    
    yield return new WaitForSeconds(1.5f);
    //먹기 시작함 우적우적
    animator_cat.SetBool("iseat", true);
    audioSource.clip = yumyum;
    if (!audioSource.isPlaying)
    audioSource.Play();
    //음식물 튀기는 파티클도 같이
    if(!particleObject_EatEffect.isPlaying)
    {
    particleObject_EatEffect.Play();
    }
    //7초동안 냠냠ㄴ냠냠냠
    yield return new WaitForSeconds(6f);
    animator_cat.SetBool("iseat", false);
    resetfood();
    //나는 배고프지 않아요 일단은 배 안고프게
    //dataobj.GetComponent<DataManager>().ishungry=false;

        //사료 7?
        if(sf==7)
        {
            if (dataobj.GetComponent<DataManager>().ishungry == false)
            {
            
            }
            else
            {
            Debug.Log("일반");
            dataobj.GetComponent<DataManager>().IncreaseProgress();
            eattimecheck();
            }
        }
        //사료 8?
        else if (sf==8)
        {
            Debug.Log("고급");
            
            if(!PlayerPrefs.HasKey("yummy"))
            {
                yummy=0;
            }
            else
            {
                yummy = PlayerPrefs.GetInt("yummy");
            }
            //3보다 크게 되면 yummy 리셋하고 배불러서 못먹게함
            if(yummy>1)
            {
                dataobj.GetComponent<DataManager>().IncreaseProgress();
                eattimecheck();
                yummy=0;
                PlayerPrefs.SetInt("yummy",yummy);
                PlayerPrefs.Save();
            }
            else
            {
            yummy=yummy+1;
            dataobj.GetComponent<DataManager>().IncreaseProgress2();
            //야미 값을 하나 더하고 저장함
            PlayerPrefs.SetInt("yummy",yummy);
            PlayerPrefs.Save();
            }
        }
        //사료 9?
        else if (sf==9)
        {
            Debug.Log("프리");
            dataobj.GetComponent<DataManager>().IncreaseProgress3();
            eattimecheck();
        }
        noTouch.SetActive(false);
    }

    void eattimecheck()
    {
            string st = dataobj.GetComponent<DataManager>().nowTime();
            Debug.Log("string"+st);
            PlayerPrefs.SetString("eatplay", st);
            PlayerPrefs.Save();
            Debug.Log("먹은 시간 저장!"); 
    }

}
