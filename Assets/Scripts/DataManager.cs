using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;
[System.Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target)=> target = _target;
    public List<T> target;
}

[System.Serializable]
public class Item
{   
      public Item(string _id, string _name, string _number, string _isUsing)
    {
        id = _id;
        name = _name;
        number=_number;
        isUsing=_isUsing;
    }
    public string id, name, number, isUsing;
}



public class DataManager : MonoBehaviour
{
    public bool hungrysoundon = true;
    private string[] hungry_string = { "은(는) 배고프다냥.", "은(는) 허기졌다냥.", "은(는) 굶주렸다냥", "은(는) 꼬르륵..흑","은(는) 배고파 배고파.. " };
    private string[] simsim_string = { "은(는) 놀고싶다냥.", "은(는) 심심하다냥", "은(는) 외롭다냥.", "은(는) 심심해요..", "은(는) 집사를 찾는다냥" };
    private string[] good_string = { "은(는) 자신감이 넘친다냥.", "은(는) 우쭐하다냥.", "은(는) 행복하다냥.", "은(는) 기분이 좋다냥.", "은(는) 만족한다냥." };
    public GameObject alert_obj;
    public Animator alert_ani;
    public Text alert_text;
    public AudioClip shopBGM;
    public AudioClip mainBGM;

    public AudioSource audioSource;
    public AudioClip levelup_sound;
    public AudioClip rewards_sound;
    public GameObject level_fx;

    [SerializeField] ParticleSystem particleObject_hungry;
    [SerializeField] ParticleSystem particleObject_levelup;
    public Material Floor10;
    public Material Floor11;
    public Material Floor12;
    public Material Floor13;
    public Material Floor0;
    public Material Wall14;
    public Material Wall15;
    public Material Wall16;
    public Material Wall17;
    public Material Wall0;

    //바닥 벽지 텍스쳐
    public GameObject Floor_obj;
    public GameObject[] Wall_obj;
    private MeshRenderer Wall_renderer1;
    private MeshRenderer Wall_renderer2;
    private MeshRenderer Wall_renderer3;  
    private MeshRenderer Floor_renderer;

    public GameObject[] ItemObject;
    public TextAsset ItemDatabase;

    public List<Item> AllItemList;
    string filePath;
    public Text tx_name;
    public Text tx_money;
    public Text levelTier;
    public Slider progressBar;
    private string name;
    private int type;
    private int friendship;
    public int money;
    private int skin;
    int currentitem;
    int statValue;
    public GameObject cat;
    public Animator catAnimator;
    //코인 업 애니메이션
    public Animator coinAnimator;
    public GameObject coinAnime;
    //친밀도 업 애니메이션
    public Animator chinAnimator;
    public GameObject chinAnime;

    [SerializeField] int maxTier = 6;
   
    // 상태 배고픔 심심함
    public bool ishungry= false;
    public bool issimsim= false;

    // 레벨업 패널
    public GameObject rewards;

    private bool levelup_once=false;

    private void Start()
    {   
            if (PlayerPrefs.HasKey("statValue"))
            {
                statValue = PlayerPrefs.GetInt("statValue");
                levelTier.text = Mathf.Floor(statValue / ((float)maxTier)).ToString();

                UpdateProgressBar();
            }
            else
            {
                statValue = 6;
                levelTier.text = Mathf.Floor(statValue / ((float)maxTier)).ToString();
                PlayerPrefs.SetInt("statValue", statValue);
                PlayerPrefs.Save();
                UpdateProgressBar();
            }
        //nowTime_window();
        //PlayerPrefs.DeleteKey("lastplay");
        catAnimator=cat.GetComponent<Animator>();
        coinAnimator = coinAnime.GetComponent<Animator>();
        Wall_renderer1 = Wall_obj[0].GetComponent<MeshRenderer>();
        Wall_renderer2 = Wall_obj[1].GetComponent<MeshRenderer>();
        Wall_renderer3 = Wall_obj[2].GetComponent<MeshRenderer>();
        Floor_renderer = Floor_obj.GetComponent<MeshRenderer>();
          // 전체 아이템 리스트 불러오기
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length-1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            line[i] = line[i].Trim(); //이거 추가
            string[] row = line[i].Split('\t');
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3]));
        }

        filePath = Application.persistentDataPath + "/allitemdata.txt";
        //저장된 이름이 없으면 첫 화면으로 갑니다.
        //코루틴을 이용해 4초마다 나오는 저장기능 시간 저장!
        

        if (PlayerPrefs.HasKey("name"))
        {  
            // 이름 있으면 로드!
            _load();
            Load();
            //지속적 시간저장
            StartCoroutine(TimeSave());
            //상태 표시
            StartCoroutine(AlertCoroutine());
            if (PlayerPrefs.HasKey("item"))
            {
                int currentitem=PlayerPrefs.GetInt("item");
                changeEquipment(currentitem);
            }
               if (PlayerPrefs.HasKey("item_wall"))
            {
                int currentwall=PlayerPrefs.GetInt("item_wall");
                changeEquipment(currentwall);

            }
               if (PlayerPrefs.HasKey("item_floor"))
            {
                int currentfloor=PlayerPrefs.GetInt("item_floor");
                changeEquipment(currentfloor);
            }
        
        }
        else
        {
            // 랜덤 스킨 부여
            SceneManager.LoadScene("HelloWorld");
            skin = UnityEngine.Random.Range(0, 18);
            PlayerPrefs.SetInt("skin", skin);
            PlayerPrefs.Save();
            Cattexture.isgiveskin = true;
        }
        alert_ani = alert_obj.GetComponent<Animator>();
    }


    public void _save()
    {
        PlayerPrefs.SetString("name", name);
        PlayerPrefs.SetInt("type", type);
        //PlayerPrefs.SetInt("friendship", friendship);
        PlayerPrefs.SetInt("money", money);
       //PlayerPrefs.SetFloat("progressBar.value", progressBar.value);
        PlayerPrefs.Save();
    }
    public void _load()
    {
        name = PlayerPrefs.GetString("name");
        type = PlayerPrefs.GetInt("type");
        //friendship = PlayerPrefs.GetInt("friendship");
        money = PlayerPrefs.GetInt("money");
        //progressBar.value = PlayerPrefs.GetFloat("progressBar.value");
        //화면상의 텍스트로 출력해주겠니?
        tx_name.text = name;
        tx_money.text = "" + money;
        //progressBar.value = progressBar.value;
    }

    void Save()
    {
        
        string jdata = JsonUtility.ToJson(new Serialization<Item>(AllItemList));
        File.WriteAllText(filePath, jdata);
    }
     void Load()
    {
        //if(!File.Exists(filePath)) {ResetItemClick(); return; }

        string jdata = File.ReadAllText(filePath);
        AllItemList=JsonUtility.FromJson<Serialization<Item>>(jdata).target;
    }

    public void Finditem(string id)
    { 
        Item selItem = AllItemList.Find(x => x.id == id);
        if(id==selItem.id)
        {
            int price=int.Parse(selItem.number);
            money = PlayerPrefs.GetInt("money");
            if(selItem.isUsing=="1")
            {
                // ----------------------------------장식품------------------------------
                if(selItem.id=="1" || selItem.id=="2" || selItem.id=="3" || selItem.id=="4" || selItem.id=="5" || selItem.id=="6")
                {
                // 이미 갖고 있을때
                print("아이템 착용");
                // 하지만 장착한걸 눌렀다면
                int currentitem=PlayerPrefs.GetInt("item");
                if(currentitem == int.Parse(selItem.id))
                {
                ItemObject[currentitem-1].SetActive(false);
                //영인 상태에서는 아무것도 안끼고있는 상태임
                PlayerPrefs.SetInt("item", 0);
                PlayerPrefs.Save();
                }
                else{
                //장착하는 함수
                equipment(selItem.id);
                catAnimator.SetTrigger("click");
                }
                }
                // ----------------------------------바닥------------------------------
                else if(selItem.id=="10" || selItem.id=="11" || selItem.id=="12" || selItem.id=="13")
                {
                // 이미 갖고 있을때
                print("아이템 착용");
                // 하지만 장착한걸 눌렀다면
                int currentitem=PlayerPrefs.GetInt("item_floor");
                if(currentitem == int.Parse(selItem.id)){
                //영인 상태에서는 아무것도 안끼고있는 상태임
                PlayerPrefs.SetInt("item_floor", 18);
                PlayerPrefs.Save();
                equipment("18");
                }
                else
                //장착하는 함수
                equipment(selItem.id);
                }
                else if(selItem.id=="14" || selItem.id=="15" || selItem.id=="16" || selItem.id=="17")
                {
                // 이미 갖고 있을때
                print("아이템 착용");
                // 하지만 장착한걸 눌렀다면
                int currentitem=PlayerPrefs.GetInt("item_wall");
                if(currentitem == int.Parse(selItem.id)){
                //영인 상태에서는 아무것도 안끼고있는 상태임
                PlayerPrefs.SetInt("item_wall", 19);
                PlayerPrefs.Save();
                equipment("19");
                }
                else
                //장착하는 함수
                equipment(selItem.id);
                }
            }
            else if(money-price<0 && selItem.isUsing=="0")
            {
                // 돈이 없을때
                print("돈없음 ㅅㄱ");
            }
            else
            {
            // 첫구매
            //만약 그게 사료라면
            if(selItem.id=="7" || selItem.id=="8" || selItem.id=="9")
            {
            int amount=0;
            if(PlayerPrefs.HasKey("food"+selItem.id))
            amount = PlayerPrefs.GetInt("food"+selItem.id);
            Debug.Log("food"+selItem.id);    
            PlayerPrefs.SetInt("food"+selItem.id, amount+1);
            money=money-price;
                    coinAnimator.SetTrigger("isBuy");
                    PlayerPrefs.SetInt("money", money);
            PlayerPrefs.Save();
            tx_money.text = "" + money;     
            }
            else
            {
            money=money-price;
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.Save();
            tx_money.text = "" + money;
            int a=int.Parse(selItem.id);  
            AllItemList.RemoveAt(a-1);
                    coinAnimator.SetTrigger("isBuy");
                    print("아이템 구매 완료");
            AllItemList.Insert(a-1, new Item(selItem.id,selItem.name,selItem.number,"1"));
            Save();
            Load();
            ShopCheck(selItem.id);
            }
            }
        }

    }

    public string Finditem_text(string id)
    {
        Item selItem = AllItemList.Find(x => x.id == id);
        string announce="";
        
        money = PlayerPrefs.GetInt("money");
        int price=int.Parse(selItem.number);
        if(id==selItem.id)
        {
            if(selItem.isUsing=="1")
            {
               // 이미 갖고 있을때
               // 하지만 장착한걸 눌렀다면
                
                int id2=int.Parse(selItem.id);                
                if(id2 <=6 && id2!=0)
                {
                    currentitem=PlayerPrefs.GetInt("item");
                }
                // 바닥  
                else if(id2 >=10 && id2 <=13)
                {
                    currentitem=PlayerPrefs.GetInt("item_floor");
                }
                // 벽지
                else if(id2 >=14 && id2 <=17)
                {
                    currentitem=PlayerPrefs.GetInt("item_wall");
                }
               
               if(currentitem == int.Parse(selItem.id))
               announce=selItem.name+"을(를) 해제합니다.";
               else
               announce=selItem.name+"을(를) 장착합니다.";
               return announce;
            }
            else if(money-price<0 && selItem.isUsing=="0")
            {
                // 돈이 없을때
                announce=selItem.name+"을(를) 살 돈이 부족합니다.";
                return announce;
            }
            else
            {
                // 첫구매
                announce=selItem.name+"을(를) 구매합니다.";
                return announce;
            }
        }
        else
        {
            return announce;
        }
    }


    public void equipment(string id)
    {
        int idcheck=int.Parse(id);
        // 1~6 장식
        // 7~9 사료
        // 10~13 바닥
        // 14~17 벽지
        if(idcheck >= 1 && idcheck <=6)
        {
            PlayerPrefs.SetInt("item", idcheck);
            PlayerPrefs.Save();
        }
        if(idcheck >= 10 && idcheck <=13)
        {
            PlayerPrefs.SetInt("item_floor", idcheck);
        PlayerPrefs.Save();
        }
        if(idcheck >= 14 && idcheck <=17)
        {
        PlayerPrefs.SetInt("item_wall", idcheck);
        PlayerPrefs.Save();
        }
        changeEquipment(idcheck);
     
    }
    public void changeEquipment(int id)
    {
        // 18 , 19가 벽 바닥의 초기화 즉 0인 부분
        if(id==0)
        {
            for(int i=0; i<6; i++)
            {
                ItemObject[i].SetActive(false);
            }
        }
        else if(id <=6 && id!=0)
        {
            for(int i=0; i<6; i++)
            {
                ItemObject[i].SetActive(false);
            }
            ItemObject[id-1].SetActive(true);
        }
        // 바닥  
        else if(id >=10 && id <=13)
        {
            if(id==10)
            Floor_renderer.material=Floor10;
            if(id==11)
            Floor_renderer.material=Floor11;
            if(id==12)
            Floor_renderer.material=Floor12;
            if(id==13)
            Floor_renderer.material=Floor13;
        }
        // 벽지
        else if(id >=14 && id <=17)
        {
            if(id==14)
            {Wall_renderer1.material=Wall14;
            Wall_renderer2.material=Wall14;
            Wall_renderer3.material=Wall14;}
            if(id==15)
            {Wall_renderer1.material=Wall15;
            Wall_renderer2.material=Wall15;
            Wall_renderer3.material=Wall15;}
            if(id==16)
            {Wall_renderer1.material=Wall16;
            Wall_renderer2.material=Wall16;
            Wall_renderer3.material=Wall16;}
            if(id==17)
            {Wall_renderer1.material=Wall17;
            Wall_renderer2.material=Wall17;
            Wall_renderer3.material=Wall17;}
        }
        else if(id == 18)
        {
            //바닥 초기화
            Floor_renderer.material=Floor0;
        }
        else if(id == 19)
        {
            // 벽지 초기화
           Wall_renderer1.material=Wall0;
           Wall_renderer2.material=Wall0;
           Wall_renderer3.material=Wall0;
        }
    }

    public bool ShopCheck(string id)
    {
        Item selItem = AllItemList.Find(x => x.id == id);
        if(selItem.isUsing=="1")
        return true;
        else
        return false;
    }
    
    private void UpdateProgressBar()
    {
        progressBar.value = statValue % maxTier;
        float a=Mathf.Floor(statValue / ((float)maxTier));
        PlayerPrefs.SetInt("statValue", statValue);

        // 몸 크기
        CatSizeDetermine(statValue);


        /*레벨이 5가 된다면 레벨업 리워드 함수가 실행되게 할거야~ / 
         * 레벨로 정하니까 5레벨에서 경험치 넣어도 패널이 자꾸뜨네 열받네(소수점 삭제때문에 그런듯) /
         * 스탯밸류가 지정범위일 경우에 껏다 키면 돈이 5000원으로 초기화되고 보상창이 다시 뜸/
         * 보상받고 스탯밸류를 1을 올리게되면 10의 단위가 틀어지므로 좋은 생각이 아님/ 근데 statvalue++가 1올리는거인거 알지?
         * 한번만 주고 끝내는 방법..
         * 스탯밸류의 의해 보상주는 것 보다는 닫기 버튼을 클릭하면 5000원을 추가하는걸로 만들면 5000원이 초기화 안될거같다!
         * 돈 초기화 문제는 해결 되었으니 이제 패널이 한번만 뜨게 만들면 될 거 같은데....
         * 그냥 스탯밸류를 1올리는 걸로하고 모든 친밀도 획득을 1로 통일하는게 제일 나을듯
         */

        if (statValue==30) { 
            Levelup_rewards();
            statValue++;
            UpdateProgressBar();

        } else if (statValue == 60)
        {
            Levelup_rewards();
            statValue++;
            UpdateProgressBar();


        }
        else if (statValue == 90)
        {
            Levelup_rewards();
            statValue++;
            UpdateProgressBar();


        }
        else if (statValue == 120)
        {
            Levelup_rewards();
            statValue++;
            UpdateProgressBar();

        }
        else if (statValue == 150)
        {
            Levelup_rewards();
            statValue++;
            UpdateProgressBar();

        }
        else if (statValue == 180)
        {
            Levelup_rewards();
            statValue++;
            UpdateProgressBar();

        }

        if (a<31)  // && a>1를 사용하니 1부터 시작하긴 하나 0이 안보이는것 뿐인거같다.(레벨업을 하면 레벨 1 과정 2번 반복)
        {
        levelTier.text = Mathf.Floor(statValue / ((float)maxTier)).ToString();
        PlayerPrefs.SetInt("statValue", statValue);
        PlayerPrefs.Save();
        }

        if (statValue == 11 || statValue == 17 || statValue == 23 || statValue == 29 || statValue == 35 || statValue == 41 || statValue == 47 || statValue == 53 || statValue == 59
            || statValue == 65 || statValue == 71 || statValue == 77 || statValue == 83 || statValue == 89 || statValue == 95 || statValue == 101 || statValue == 107 || statValue == 113 || statValue == 119 || statValue == 125 || statValue == 131
            || statValue == 137 || statValue == 143 || statValue == 149 || statValue == 155 || statValue == 161 || statValue == 167 || statValue == 171 || statValue == 179)
            levelup_once = true;

        if (statValue == 12 || statValue == 18 || statValue == 24 || statValue == 30 || statValue == 36 || statValue == 42 || statValue == 48 || statValue == 54 || statValue == 60
            || statValue == 66 || statValue == 72 || statValue == 78 || statValue == 84 || statValue == 90 || statValue == 96 || statValue == 102 || statValue == 108 || statValue == 114 || statValue == 120 || statValue == 126 || statValue == 132
            || statValue == 138 || statValue == 144 || statValue == 150 || statValue == 156 || statValue == 162 || statValue == 168 || statValue == 172 || statValue == 180 )
        {
            if(levelup_once == true)
            LevelUp_effect();
            levelup_once = false;
        }

    }

    private void Levelup_rewards() // 판넬열기
    {
       
            rewards.SetActive(true);

        audioSource = level_fx.GetComponent<AudioSource>();
        audioSource.clip = rewards_sound;
        if (!audioSource.isPlaying)
            audioSource.Play();

    }

    private void LevelUp_effect()
    {
        

        if (!particleObject_levelup.isPlaying)
        {
            particleObject_levelup.Play(); //
        }

        audioSource = level_fx.GetComponent<AudioSource>();
        audioSource.clip = levelup_sound;
        if (!audioSource.isPlaying)
            audioSource.Play();


    }

    public void Levelreset()
    {
        PlayerPrefs.DeleteKey("statValue");
        statValue = 6;
        UpdateProgressBar();
    }

   public void IncreaseProgress()
    {
        statValue++;
        UpdateProgressBar();
        chinAnimator.SetTrigger("isEarn"); // 친밀도 올라가는 애니메이션
    }

    public void IncreaseProgress2()
    {
        statValue = statValue + 2;
        UpdateProgressBar();
        chinAnimator.SetTrigger("isEarn"); // 친밀도 올라가는 애니메이션
    }

    public void IncreaseProgress3() // 이게 디벨롭 레벨업 버튼
    {
        statValue++;
        UpdateProgressBar();
        chinAnimator.SetTrigger("isEarn"); // 친밀도 올라가는 애니메이션
    }
    public void money_earn()
    {
        money += 10000;
        _save();
        _load();
    }
    //먹이주기 버튼 누르면 돈빠져나가요
    public void money_cheapfeed()
    {
        money -= 5;
        _save();
        _load();
    }

    public void money_middlefeed()
    {
        money -= 10;
        _save();
        _load();
    }
    public void money_primiumfeed()
    {
        money -= 50;
        _save();
        _load();
    }

    public void level_rewards() // 레벨보상 감사해요! 버튼 누를시 5000원 지급
    {
        money += 5000;
        coinAnimator.SetTrigger("isEarn");
        _save();
        _load();
    }

    public void play_rewards(int ramo) // 플레이 보상, 랜덤 돈 지급
    {
        money += ramo*100;
        coinAnimator.SetTrigger("isEarn");
        _save();
        _load();
    }
    //배고픔 시간 측정...
    IEnumerator TimeSave()
    {    
        //시간 관련
        while (true)
        {
            yield return new WaitForSeconds(3f);
            string st = nowTime();
            Debug.Log("string"+st);
            // 마지막으로 저장되었던 시간은 lastplay로 저장이 됩니다.
            PlayerPrefs.SetString("lastplay", st);
            PlayerPrefs.Save();
            Debug.Log("현재 시간 저장!");

            //심심한가? 
            Timesimsim();
            //배고픈가?
            Timehungry();
            yield return new WaitForSeconds(4f);
        }
    }
    void Timesimsim()
    {
        if (PlayerPrefs.HasKey("toyplay"))
            {
                TimeSpan timespan;
                DateTime userIndate;
                DateTime now;
                now = DateTime.Now;
                string st=PlayerPrefs.GetString("toyplay");
                //가져온 유저의 문자열 접속시간을 DateTIme으로 변경합니다.
                userIndate = Convert.ToDateTime(st);
                timespan = now - userIndate; //시간의 차이를 구합니다.
                // timespan.TotalSeconds//차이를 초로 나타냅니다.
                // timespan.TotalMinuites// 분으로 나타냅니다.
                // timespan.Miliseconds//밀리초로 나타냅니다.
                //UserInfo.tickets += (int)timespan.TotalSeconds / 600;
                //1시간 넘게 접속하지 않았다면? 
                Debug.Log("timespan.TotalMinuites : "+timespan.TotalMinutes);
                if(timespan.TotalMinutes > 17)
                {
                    issimsim=true;
                }
                else
                issimsim=false;
            }
        else if (!PlayerPrefs.HasKey("toyplay"))
        {
            //처음 시작 먹은 시간 없을때 예외처리 
            string st = nowTime();
            Debug.Log("string" + st);
            PlayerPrefs.SetString("toyplay", st);
            PlayerPrefs.Save();
            Debug.Log("초기 시간 저장!");
            issimsim = false;
        }
    }
    void Timehungry()
    {
        if (PlayerPrefs.HasKey("eatplay"))
            {
                        TimeSpan timespan;
                        DateTime userIndate;
                        DateTime now;
                        now = DateTime.Now;
                        //밥먹었던 시간!!
                        string st=PlayerPrefs.GetString("eatplay");
                        //가져온 유저의 문자열 접속시간을 DateTIme으로 변경합니다.
                        userIndate = Convert.ToDateTime(st);
                        timespan = now - userIndate; //시간의 차이를 구합니다.
                        Debug.Log("timespan.TotalMinuites : "+timespan.TotalMinutes);
                        //3분...그는 배고파집니다..
                        if(timespan.TotalMinutes > 90)
                        {
                            //배고프면 나올 행동들
                            Debug.Log("he is hungry");
                            //hungry.text="hungry";
                            ishungry=true;
                            particleemotion();
                            if(hungrysoundon)
                            cat.GetComponent<RandomMove>().Hungry_Sound();
                        }
                        else
                        ishungry=false;
            }
        else if(!PlayerPrefs.HasKey("eatplay"))
        {
            //처음 시작 먹은 시간 없을때 예외처리 
            string st =nowTime();
            Debug.Log("string"+st);
            PlayerPrefs.SetString("eatplay", st);
            PlayerPrefs.Save();
            Debug.Log("초기 시간 저장!"); 
            ishungry=false;
        }   
           
    }
    public string nowTime()
    {
        //스트링 값으로 현재 시간을 보내드립니다.
        DateTime now;
        now = DateTime.Now;
        string st = now.ToString("yyyy-MM-dd HH:mm:ss");
        return st;
    }

    public int nowTime_window()
    {
        DateTime now;
        now = DateTime.Now;
        int hour = now.Hour;
        int i;
        //Debug.Log(hour);
        if(hour>=11 && hour<=17)
        {
        //낮
        return i=0;
        }
        else if(hour>=18 && hour<=20)
        {
        //석양
        return i=1;
        }
        else if(hour>=7 && hour<=10)
        {
        //석양
        return i=1;
        }
        else
        {
        //밤
        return i=2;
        }
    }

    //파티클 object
      public void particleemotion(){
      if(!particleObject_hungry.isPlaying){
          particleObject_hungry.Play();
       }     
    }


    public void BackMain()
    {
        GameObject sm = GameObject.Find("BGM");
        AudioSource music = sm.GetComponent<AudioSource>(); //배경음악 저장해둠
        if (music.isPlaying)
        { 
        music.Pause();
        music.clip = mainBGM;
        music.Play();
        }
    }
        public void BackShop()
    {
        GameObject sm = GameObject.Find("BGM");
        AudioSource music = sm.GetComponent<AudioSource>(); //배경음악 저장해둠
        if (music.isPlaying)
        { 
        music.Pause();
        music.clip = shopBGM;
        music.Play();
        }
    }
    public void DeleteKeyAll()
    {
        PlayerPrefs.DeleteAll();
    }


    IEnumerator AlertCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(8f);
            if (issimsim==true && ishungry==true)
            {
                int random = UnityEngine.Random.Range(0,hungry_string.Length);
                int random2 = UnityEngine.Random.Range(0, simsim_string.Length);
                alert_text.text = name + hungry_string[random]+"\n"+name+ simsim_string[random2];
                alert_ani.SetTrigger("Show");
                yield return new WaitForSeconds(5f);
            }
            else if(issimsim == true)
            {
                int random = UnityEngine.Random.Range(0,simsim_string.Length);
                alert_text.text = name + simsim_string[random];
                alert_ani.SetTrigger("Show");
                yield return new WaitForSeconds(5f);
            }
            else if(ishungry == true)
            {
                int random = UnityEngine.Random.Range(0,hungry_string.Length);
                alert_text.text = name + hungry_string[random];
                alert_ani.SetTrigger("Show");
                yield return new WaitForSeconds(5f);
            }
            else
            {
                yield return new WaitForSeconds(15f);
                int random = UnityEngine.Random.Range(0, good_string.Length);
                alert_text.text = name + good_string[random];
                alert_ani.SetTrigger("Show");
                yield return new WaitForSeconds(10f);
                Debug.Log("양호한 상태");
            }
        }
    }

    private void CatSizeDetermine(int stat)
    {
        /*float x = cat.transform.localScale.x;

        float y = cat.transform.localScale.y;

        float z = cat.transform.localScale.z;

        print(x + ", " + y + ", " + z); // 5, 5, 5*/
        float scale= Mathf.Floor(stat/ ((float)maxTier));
        if (scale < 6f)
        {
            cat.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        }
        else if (scale < 11f)
        {
            cat.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
        }
        else if (scale < 21f)
        {
            cat.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        }
        else if (scale < 31f)
        {
            cat.transform.localScale = new Vector3(0.032f, 0.032f, 0.032f);
        }
    }
}
