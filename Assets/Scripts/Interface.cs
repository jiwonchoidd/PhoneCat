using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SVSBluetooth;
using System.Text;

public class Interface : MonoBehaviour
{
    public Text toplevel;
    public GameObject camchange;
    public Animator camchange_ani;
    public GameObject player_other;
    public Material Floor10_other;
    public Material Floor11_other;
    public Material Floor12_other;
    public Material Floor13_other;
    public Material Floor0_other;
    public Material Wall14_other;
    public Material Wall15_other;
    public Material Wall16_other;
    public Material Wall17_other;
    public Material Wall0_other;
    public Material bangal;
    public Material bangalgray;
    public Material black;
    public Material doopcheese;
    public Material doopcheesegray;
    public Material russian;
    public Material siam;
    public Material tuksido;
    public Material white;
    public Material eye_orange;
    public Material eye_blue;
    //바닥 벽지 텍스쳐
    public GameObject Floor_obj_other;
    public GameObject[] Wall_obj_other;
    private MeshRenderer Wall_renderer1_other;
    private MeshRenderer Wall_renderer2_other;
    private MeshRenderer Wall_renderer3_other;
    private MeshRenderer Floor_renderer_other;
    public GameObject[] ItemObject;

    private new SkinnedMeshRenderer renderer;
    private SkinnedMeshRenderer renderer2;
    private SkinnedMeshRenderer renderer3;
    public GameObject eyeL_other;
    public GameObject eyeR_other;
    public GameObject body_other;
    public GameObject disconnect_pannel;
    // 고양이 내려오는 애니메이션 하기위해 위치값은 애니메이션화함
    public GameObject othercat;
    public Animator othercat_ani;
    // 여기는 캣 애니메이션 담당 오브젝트
    public GameObject myCat_obj;
    public GameObject youCat_obj;
    public Animator myCat_ani;
    public Animator youCat_ani;

    public Image image; // a picture that displays the status of the bluetooth adapter upon request
    public Text textField; // field for displaying messages and events
    public Text name;
    public GameObject dataObj; //게임 데이터를 보내주자~
    public Button startbutton;
    const string MY_UUID = "0b7062bc-67cb-492b-9879-09bf2c7012b2"; // UUID constant which is set via script
    bool amihost = false;
    BluetoothForAndroid.BTDevice[] devices;
    string lastConnectedDeviceAddress;
    public bool isConnect = false;
    int isConnectcheck = 0;
    //내 고양이 정보
    string myname;
    int myskin;
    int mylevel;
    int myitem;
    //호스트만 보내줌
    int myfloor;
    int mywall;
    // 상대방 고양이 정보를 받아오자
    public string youname;
    public int youskin;
    public int youlevel;
    public int youitem;
    public int youfloor;
    public int youwall;
    public string[] youinfo;
    // 상대방의 연결을 기다리고 있씁니다 - > 고양이를 전송합니다
    public Text summit;

    //교미 패널
    public GameObject catpanel_imgmyobj;
    public GameObject catpanel_imgyourobj;
    public Sprite[] catpanel_sprite;
    public Image catpanel_mycatimg;
    public Image catpanel_yourcatimg;
    public Text catpanel_text;
    public Text catpanel_mycattext;
    public Text catpanel_yourcattext;
    public GameObject heart;
    public Animator heart_ani;
    public GameObject locksex;
    // 오디오
    public AudioClip bright_bell;
    public AudioSource effect;
    //진행
    public GameObject prog_obj;
    public Animator prog_ani;
    [SerializeField] ParticleSystem particleHeart;
    private bool issex = false;

    public GameObject sexpanel;
    void Start()
    {
        
        effect = GetComponent<AudioSource>();
        //심장 애니메이션
        heart_ani = heart.GetComponent<Animator>();
        // 각 고양이 애니메이션 담당
        myCat_ani = myCat_obj.GetComponent<Animator>();
        youCat_ani = youCat_obj.GetComponent<Animator>();
        //
        othercat_ani =othercat.GetComponent<Animator>();
        camchange_ani = camchange.GetComponent<Animator>();
        Wall_renderer1_other = Wall_obj_other[0].GetComponent<MeshRenderer>();
        Wall_renderer2_other = Wall_obj_other[1].GetComponent<MeshRenderer>();
        Wall_renderer3_other = Wall_obj_other[2].GetComponent<MeshRenderer>();
        Floor_renderer_other = Floor_obj_other.GetComponent<MeshRenderer>();
        renderer = body_other.GetComponent<SkinnedMeshRenderer>();
        renderer2 = eyeL_other.GetComponent<SkinnedMeshRenderer>();
        renderer3 = eyeR_other.GetComponent<SkinnedMeshRenderer>();

        Initialize();
        name.gameObject.SetActive(false);
        startbutton.gameObject.SetActive(false);

        //내 고양이 정보
        myname = PlayerPrefs.GetString("name");
        myskin = PlayerPrefs.GetInt("skin");
        mylevel = PlayerPrefs.GetInt("statValue");
        myitem = PlayerPrefs.GetInt("item");
        //호스트만 보내줌
        if(!PlayerPrefs.HasKey("item_floor"))
        PlayerPrefs.SetInt("item_floor", 18);
        myfloor = PlayerPrefs.GetInt("item_floor");
        if (!PlayerPrefs.HasKey("item_wall"))
            PlayerPrefs.SetInt("item_wall", 19);
        mywall = PlayerPrefs.GetInt("item_wall");

        //연결되었을때 계속 연결되어있는지 확인해줌. 해제되었으면 로비로 나가게 해주자.
        StartCoroutine(ConnectCheck_onGame());
    }

    void Update()
    {
        if (image.gameObject.active)
            GetBluetoothStatus();

        //만약에 연결이 안되어있다면.. 계속해서 본인의 이름을 보냄
        if (isConnect == false)
            StartCoroutine(CheckConnect());

    }
    private void OnEnable()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        BluetoothForAndroid.ReceivedIntMessage += PrintVal1;
        BluetoothForAndroid.ReceivedFloatMessage += PrintVal2;
        BluetoothForAndroid.ReceivedStringMessage += PrintVal3;
        BluetoothForAndroid.ReceivedStringMessage += PrintVal4;

        BluetoothForAndroid.BtAdapterEnabled += PrintEvent1;
        BluetoothForAndroid.BtAdapterDisabled += PrintEvent2;
        BluetoothForAndroid.DeviceConnected += PrintEvent3;
        BluetoothForAndroid.DeviceDisconnected += PrintEvent4;
        BluetoothForAndroid.ServerStarted += PrintEvent5;
        BluetoothForAndroid.ServerStopped += PrintEvent6;
        BluetoothForAndroid.AttemptConnectToServer += PrintEvent7;
        BluetoothForAndroid.FailConnectToServer += PrintEvent8;

        BluetoothForAndroid.DeviceSelected += PrintDeviceData;
    }
    private void OnDisable()
    {
        BluetoothForAndroid.ReceivedIntMessage -= PrintVal1;
        BluetoothForAndroid.ReceivedFloatMessage -= PrintVal2;
        BluetoothForAndroid.ReceivedStringMessage -= PrintVal3;
        BluetoothForAndroid.ReceivedStringMessage -= PrintVal4;

        BluetoothForAndroid.BtAdapterEnabled -= PrintEvent1;
        BluetoothForAndroid.BtAdapterDisabled -= PrintEvent2;
        BluetoothForAndroid.DeviceConnected -= PrintEvent3;
        BluetoothForAndroid.DeviceDisconnected -= PrintEvent4;
        BluetoothForAndroid.ServerStarted -= PrintEvent5;
        BluetoothForAndroid.ServerStopped -= PrintEvent6;
        BluetoothForAndroid.AttemptConnectToServer -= PrintEvent7;
        BluetoothForAndroid.FailConnectToServer -= PrintEvent8;

        BluetoothForAndroid.DeviceSelected -= PrintDeviceData;
    }


    //항상 플러그인 초기화해라 오류 적게
    public void Initialize()
    {
        BluetoothForAndroid.Initialize();
        GetBluetoothStatus();
    }

    // 상태확인
    public void GetBluetoothStatus()
    {
        if (BluetoothForAndroid.IsBTEnabled())
        {
            image.color = Color.green;
            // btison.gameObject.SetActive(false);    
        }
        else
        {
            image.color = Color.red;
            // btison.gameObject.SetActive(true);
        }
    }
    public void EnableBT()
    {
        BluetoothForAndroid.EnableBT();
    }
    public void DisableBT()
    {
        BluetoothForAndroid.DisableBT();
    }

    // methods for creating and stopping the server, connecting to the server and disconnecting
    public void CreateServer()
    {
        BluetoothForAndroid.CreateServer(MY_UUID);
        //내가 호스트
        amihost = true;
    }
    public void StopServer()
    {
        BluetoothForAndroid.StopServer();
    }
    public void ConnectToServer()
    {
        BluetoothForAndroid.ConnectToServer(MY_UUID);
    }
    public void Disconnect()
    {
        BluetoothForAndroid.Disconnect();
    }
    public void ConnectToServerByAddress()
    {
        if (devices != null)
        {
            if (devices[0].address != "none") BluetoothForAndroid.ConnectToServerByAddress(MY_UUID, devices[0].address);
        }
    }
    public void ConnectToLastServer()
    {
        if (lastConnectedDeviceAddress != null) BluetoothForAndroid.ConnectToServerByAddress(MY_UUID, lastConnectedDeviceAddress);
    }

    // methods for sending messages of various types

    //writemessage1은 연결 테스트용 1이 전해지면 연결중 0이 전해지면 연결 끊김
    public void WriteMessage1()
    {
        BluetoothForAndroid.WriteMessage(1);
    }
    public void WriteMessage2(float send)
    {
        //동작 확인
        BluetoothForAndroid.WriteMessage(send);
    }

    // 우리 친구 데이터 전송
    public void WriteMessage3()
    {
        //이름 전송
        BluetoothForAndroid.WriteMessage(myname);
    }



    // 통신의 전체적인 통신을 담당하는 4번째 메세지
    public void WriteMessage4()
    {
        // 통신에 필요한 모든 정보를 보낼꺼임
        // 쉽게 하기위해 나는 스트링으로 모두 싸매서 보낼 생각임

        // 호스트일 경우에는 바닥과 벽 정보를 보냄
        if (amihost == true)
        {
            BluetoothForAndroid.WriteMessage(myname + "," + myskin + "," + mylevel + "," + myitem + "," + myfloor + "," + mywall);
        }
        // 게스트일 경우에는 바닥과 벽 정보를 보내지 않음
        else
            BluetoothForAndroid.WriteMessage(myname + "," + myskin + "," + mylevel + "," + myitem);
    }

    // methods for displaying received messages on the screen
    void PrintVal1(int val)
    {
        //1이 보내지면 연결되고 있는중
        isConnectcheck = val;
    }
    void PrintVal2(float val)
    {
        //동작체크
        if(val==1f)
        {
            //교감 버튼 켰을때
            sexpanel.SetActive(true);
            SexBtnPanel();
        }
        if (val == 2f)
        {
            //교감 버튼 껐을때
            sexpanel.SetActive(false);
        }
        if (val == 3f)
        {
            //교감 시작했을때
            FinaldicisionSex();
        }
    }
    void PrintVal3(string val)
    {
        textField.text += val + "\n";
        //이름 온거
        isConnect = true;
        name.gameObject.SetActive(true);
        summit.text = "고양이를 전송할\n준비가 되었습니다.";
        if(val.Length<=4)
        name.text = val + "이(가) 연결되었습니다.";
        startbutton.gameObject.SetActive(true);
    }
    void PrintVal4(string val)
    {
        youinfo = val.Split(',');
        youname = youinfo[0];
        youskin = int.Parse(youinfo[1]);
        youlevel = int.Parse(youinfo[2]);
        youitem = int.Parse(youinfo[3]);
        // 반대로 게스트만 바닥 벽 정보가 필요하기 때문에 호스트가 아니면 받아옴
        if (amihost == false)
        {
            youfloor = int.Parse(youinfo[4]);
            youwall = int.Parse(youinfo[5]);
            PlayerotherAdpat(youfloor);
            PlayerotherAdpat(youwall);
        }
        PlayerotherAdpat(youitem);
        PlayerotherAdpat_Skin(youskin);
        // 마지막 연결하기를 누르면 이걸 전송한거임
        camchange_ani.SetBool("iszoom", true);
        othercat_ani.SetBool("isdown", true);
        youCat_ani.SetTrigger("jump");
        myCat_obj.GetComponent<RandomMove_Bluetooth>().enabled = true;
        youCat_obj.GetComponent<RandomMove_Bluetooth>().enabled = true;
    }
    public void GetBondedDevices()
    {
        devices = BluetoothForAndroid.GetBondedDevices();
        if (devices != null)
        {
            for (int i = 0; i < devices.Length; i++)
            {
                textField.text += devices[i].name + "   ";
                textField.text += devices[i].address;
                textField.text += "\n";
            }
        }
    }

    // methods for displaying events on the screen
    void PrintEvent1()
    {
        textField.text += "Adapter enabled" + "\n";
    }
    void PrintEvent2()
    {
        textField.text += "Adapter disabled" + "\n";
    }
    void PrintEvent3()
    {
        textField.text += "The device is connected" + "\n";
    }
    void PrintEvent4()
    {
        textField.text += "Device lost connection" + "\n";
    }
    void PrintEvent5()
    {
        textField.text += "Server is running" + "\n";
    }
    void PrintEvent6()
    {
        textField.text += "Server stopped" + "\n";
    }
    void PrintEvent7()
    {
        textField.text += "Attempt to connect to server" + "\n";
    }
    void PrintEvent8()
    {
        textField.text += "Connection to the server failed" + "\n";
    }
    void PrintDeviceData(string deviceData)
    {
        string[] btDevice = deviceData.Split(new char[] { ',' });
        textField.text += btDevice[0] + "   ";
        textField.text += btDevice[1] + "\n";
        lastConnectedDeviceAddress = btDevice[1];
    }

    // method for cleaning the log
    public void ClearLog()
    {
        textField.text = "";
    }


    // 이름을 보냄 1초마다 받아지면 연결이 되었다....
    IEnumerator CheckConnect()
    {
        if (isConnect == false)
        {
            WriteMessage3();
            yield return new WaitForSeconds(1);
        }
        else
        {
            // 연결이 되었다
            isConnect = true;
        }
    }

    // 멀티중에 상대방이 나가거나 블루투스가 끊겼을때
    IEnumerator ConnectCheck_onGame()
    {
        // 연결이 되었다면 시작한다.

        while (true)
        {
            if (isConnect == true)
            {
                isConnectcheck = 0;
                yield return new WaitForSeconds(1);
                WriteMessage1();
                yield return new WaitForSeconds(1);

                //만약 그대로 영이라면 연결이 해제되었다는 뜻이 아닐까
                if (isConnectcheck == 0)
                {
                    //상대방연결이 끊겼습니다. 메인화면으로 돌아가던가..
                    textField.text = "연결이 끊겼습니다.";
                    disconnect_pannel.SetActive(true);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }


    void PlayerotherAdpat(int id)
    {
        // 18 , 19가 벽 바닥의 초기화 즉 0인 부분
        if (id == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                ItemObject[i].SetActive(false);
            }
        }
        else if (id <= 6 && id != 0)
        {
            for (int i = 0; i < 6; i++)
            {
                ItemObject[i].SetActive(false);
            }
            ItemObject[id - 1].SetActive(true);
        }
        // 바닥  
        else if (id >= 10 && id <= 13)
        {
            if (id == 10)
                Floor_renderer_other.material = Floor10_other;
            if (id == 11)
                Floor_renderer_other.material = Floor11_other;
            if (id == 12)
                Floor_renderer_other.material = Floor12_other;
            if (id == 13)
                Floor_renderer_other.material = Floor13_other;
        }
        // 벽지
        else if (id >= 14 && id <= 17)
        {
            if (id == 14)
            {
                Wall_renderer1_other.material = Wall14_other;
                Wall_renderer2_other.material = Wall14_other;
                Wall_renderer3_other.material = Wall14_other;
            }
            if (id == 15)
            {
                Wall_renderer1_other.material = Wall15_other;
                Wall_renderer2_other.material = Wall15_other;
                Wall_renderer3_other.material = Wall15_other;
            }
            if (id == 16)
            {
                Wall_renderer1_other.material = Wall16_other;
                Wall_renderer2_other.material = Wall16_other;
                Wall_renderer3_other.material = Wall16_other;
            }
            if (id == 17)
            {
                Wall_renderer1_other.material = Wall17_other;
                Wall_renderer2_other.material = Wall17_other;
                Wall_renderer3_other.material = Wall17_other;
            }
        }
        else if (id == 18)
        {

            Floor_renderer_other.material = Floor0_other;
        }
        else if (id == 19)
        {

            Wall_renderer1_other.material = Wall0_other;
            Wall_renderer2_other.material = Wall0_other;
            Wall_renderer3_other.material = Wall0_other;
        }

    }

    public void PlayerotherAdpat_Skin(int skin)
    {
        if (skin == 0)
        {
            renderer.material = bangal;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 1)
        {
            renderer.material = bangalgray;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 2)
        {
            renderer.material = black;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 3)
        {
            renderer.material = doopcheese;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 4)
        {
            renderer.material = doopcheesegray;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 5)
        {
            renderer.material = russian;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 6)
        {
            renderer.material = siam;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 7)
        {
            renderer.material = tuksido;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 8)
        {
            renderer.material = white;
            renderer2.material = eye_orange;
            renderer3.material = eye_orange;
        }
        else if (skin == 9)
        {
            renderer.material = bangal;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 10)
        {
            renderer.material = bangalgray;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 11)
        {
            renderer.material = black;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 12)
        {
            renderer.material = doopcheese;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 13)
        {
            renderer.material = doopcheesegray;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 14)
        {
            renderer.material = russian;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 15)
        {
            renderer.material = siam;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 16)
        {
            renderer.material = tuksido;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
        else if (skin == 17)
        {
            renderer.material = white;
            renderer2.material = eye_blue;
            renderer3.material = eye_blue;
        }
    }

    public void CamChaanger()
    {
        if(camchange_ani.GetBool("iszoom"))
        camchange_ani.SetBool("iszoom", false);
        else
        camchange_ani.SetBool("iszoom", true);
    }


    // 프로필 교미 패널 버튼 누르면
    public void SexBtnPanel()
    {
        /*//교미 패널
        public GameObject catpanel_imgmyobj;
        public GameObject catpanel_imgyourobj;
        public Sprite[] catpanel_sprite;
        public Image catpanel_mycatimg;
        public Image catpanel_yourcatimg;
        public Text catpanel_text;
        public Text catpanel_mycattext;
        public Text catpanel_yourcattext;
        public GameObject heart;
        public Animator heart_ani;*/
        heart_ani = heart.GetComponent<Animator>();
        // 패널 그림 체크
        catpanel_mycatimg = catpanel_imgmyobj.GetComponent<Image>();
        catpanel_yourcatimg = catpanel_imgyourobj.GetComponent<Image>();
        catpanel_mycatimg.sprite = catpanel_sprite[myskin];
        catpanel_yourcatimg.sprite = catpanel_sprite[youskin];
        // 패널 텍스트 체크
        catpanel_mycattext.text = "이름 : " + myname + "\n" + "성별 : " + Tell_Gender(myskin) + "\n" + "레벨 : " + LevelTranslate(mylevel) + "\n" + "종류 : " + Tell_Sp(myskin);
        catpanel_yourcattext.text = "이름 : " + youname + "\n" + "성별 : " + Tell_Gender(youskin) + "\n" + "레벨 : " + LevelTranslate(youlevel) + "\n" + "종류 : " + Tell_Sp(youskin);
        // 가능 애니메이션 & 교배 버튼 눌릴수 있게 
        catpanel_text.text = "고양이끼리 서로를 탐색중..";
        // 코루틴 써서 약간에 딜레이가 있게..
        StartCoroutine(DelayText());
    }
    private string Tell_Gender(int myskin)
    {
        if (myskin <= 8)
        {
            string a = "남";
            return a;
        }
        else
        {
            string a = "여";
            return a;
        }
    }

    private string Tell_Sp(int skin)
    {
        if (skin == 0 || skin == 9)
        {
            string a = "뱅갈";
            return a;
        }
        else if (skin == 1 || skin == 10)
        {
            string a = "뱅갈그레이";
            return a;
        }
        else if (skin == 2 || skin == 11)
        {
            string a = "블랙";
            return a;
        }
        else if (skin == 3 || skin == 12)
        {
            string a = "치즈";
            return a;
        }
        else if (skin == 4 || skin == 13)
        {
            string a = "돕치즈";
            return a;
        }
        else if (skin == 5 || skin == 14)
        {
            string a = "러시안";
            return a;
        }
        else if (skin == 6 || skin == 15)
        {
            string a = "샴";
            return a;
        }
        else if (skin == 7 || skin == 16)
        {
            string a = "턱시도";
            return a;
        }
        else 
        {
            /*if(skin == 8 || skin == 17)*/
            string a = "화이트";
            return a;
        }
    }

    private string LevelTranslate(int level)
    {
        return Mathf.Floor(level / 6f).ToString();
    }
    IEnumerator DelayText()
    {
        yield return new WaitForSeconds(1.5f);
        if(issex == true)
        {
            catpanel_text.text = "조건 불충족 (이미 시도함)";
            locksex.SetActive(true);
        }
        else if (int.Parse(LevelTranslate(mylevel)) <= 10 || int.Parse(LevelTranslate(youlevel)) <= 10)
        {
            catpanel_text.text = "조건 불충족 (레벨)";
            locksex.SetActive(true);
        }
        else if(Tell_Gender(myskin) == Tell_Gender(youskin))
        {
            catpanel_text.text = "조건 불충족 (성별)";
            locksex.SetActive(true);
        }
        else
        {
            heart_ani.SetBool("dekiru", true);
            catpanel_text.text = "교감을 하면 "+myname+ "이(가) 초기화되고, \n새로운 고양이로 바뀝니다. 시도하시겠습니까?";
            //패널 열기
            locksex.SetActive(false);
        }
    }
    //버튼에 넣을것
    public void FinaldicisionSex()
    {
        StartCoroutine(FinaldicisionSex_Co());
    }
    IEnumerator FinaldicisionSex_Co()
    {
        issex = true;
        soundeffect();
        prog_obj.SetActive(true);
        prog_ani = prog_obj.GetComponent<Animator>();
        prog_ani.SetBool("isprogress", true);
        yield return new WaitForSeconds(2f);
        prog_ani.SetBool("isprogress", false);
        ParticleHeart();
        //스킨 변경
        ChangeMycatSkin();
        // 리프레쉬
        SexBtnPanel();
        locksex.SetActive(true);
        yield return new WaitForSeconds(1f);
        heart_ani.SetBool("dekiru", false);
        prog_obj.SetActive(false);
    }
    // 오디오
    void soundeffect()
    {
        effect.clip = bright_bell;
        effect.Play();
    }
    public void ParticleHeart()
    {
        if (!particleHeart.isPlaying)
        {
            Debug.Log("파티클 왜 안됨?");
            particleHeart.Play();
        }
    }
    private void ChangeMycatSkin()
    {
        //내 고양이
        myCat_obj.GetComponent<Cattexture>().changemet();
        myskin=myCat_obj.GetComponent<Cattexture>().skin;
        //너의 고양이
        youskin= youskin + 1;
        if (youskin >= 18)
        {
            youskin = 0;
        }
        PlayerotherAdpat_Skin(youskin);

        //레벨도 리셋 
        mylevel = 6;
        youlevel = 6;
        toplevel.text = "1";
        PlayerPrefs.SetInt("statValue", mylevel);
        PlayerPrefs.Save();
    }
}
