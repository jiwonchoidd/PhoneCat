using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Coll : MonoBehaviour
{
    //광고 관리 오브젝트 
    public GameObject admanager_obj;
    private string catname;
    private string[] text1_string = {"와 실뭉치로 놀아주웠습니다. 친밀도가 상승했습니다.", "와 실뭉치로 놀아주웠습니다. 고양이와 좀 더 가까워졌습니다.", "와 실뭉치로 놀아주웠습니다. 고양이는 여운이 남습니다.." };
    private string[] text2_string = { "와 쥐돌이로 놀아주웠습니다. 친밀도가 상승했습니다.", "와 쥐돌이로 놀아주웠습니다. 좀 더 친해진듯 찍찍..", "와 쥐돌이로 놀아주웠습니다. 고양이는 여운이 남습니다.." };
    private string[] text3_string = { "와 낚시대로 놀아주웠습니다. 친밀도가 상승했습니다.", "와 낚시대로 놀아주웠습니다. 낚시대가 부셔질듯이", "와 낚시대로 놀아주웠습니다. 고양이는 여운이 남습니다.." };
    private string[] text0_string = { "(이)가 놀면서 장롱 밑에서 ", "(이)가 놀면서 소파 밑에서", "(이)가 놀면서 TV 밑에서", "(이)가 놀면서 당신 발 밑에서", "(이)가 놀면서 당신 핸드폰 밑에서", "(이)가 놀면서 서랍장 밑에서" };
    private string[] text4_string = { "(이)가 즐거워합니다. 하지만 조금 쉬고 싶어 합니다. ", "(이)가 지금은 더 놀고싶어 하지 않는 듯 합니다.", "(이)가 매우 만족해합니다. 하지만 혼자만의 시간이 필요한 것 같습니다." };
    //게임 오브젝트가 부딪히면..그에 맞는 애니메이션을 취함

    public GameObject playLogic_obj;

    public GameObject catFrontPos;

    private Rigidbody instance_rb;

    public Animator cat_ani;

    public GameObject data_obj;

    public GameObject panel_reward;
    private int rewardvalue;

    public Sprite money;
    public Sprite toy1;
    public Sprite toy2;
    public Sprite toy3;
    public Image image_reward;
    public GameObject image_obj;
    public Text image_text;
    private Rigidbody catrb;
    // Start is called before the first frame update

    private void Start()
    {
        cat_ani = this.GetComponent<Animator>();
        catname = PlayerPrefs.GetString("name");
    }

   
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Toy"))
        {
            
            if(playLogic_obj.GetComponent<PlayLogic>().isThrow==true)
            {
                //야옹
                playLogic_obj.GetComponent<PlayLogic>().Meow();
                playLogic_obj.GetComponent<PlayLogic>().instance_Playlogic.transform.position = catFrontPos.transform.position;
                playLogic_obj.GetComponent<PlayLogic>().CameraChangeColl();
                playLogic_obj.GetComponent<PlayLogic>().hit = true;
                instance_rb = playLogic_obj.GetComponent<PlayLogic>().instance_Playlogic.GetComponent<Rigidbody>();
                instance_rb.isKinematic = true;
                cat_ani.SetBool("ismove2", false);
                cat_ani.SetBool("toyball", true);
                playLogic_obj.GetComponent<PlayLogic>().isThrow = false;
                //playLogic_obj.GetComponent<PlayLogic>().hit = false;
                int random = Random.Range(0, 2);
                StartCoroutine(DelayTime(random));
            }
        }
        if(collision.collider.CompareTag("Toy2"))
        {

            if (playLogic_obj.GetComponent<PlayLogic>().isThrow == true)
            {
                //야옹
                playLogic_obj.GetComponent<PlayLogic>().Meow();
                playLogic_obj.GetComponent<PlayLogic>().instance_Playlogic.transform.position = catFrontPos.transform.position;
                playLogic_obj.GetComponent<PlayLogic>().CameraChangeColl();
                playLogic_obj.GetComponent<PlayLogic>().hit = true;
                instance_rb = playLogic_obj.GetComponent<PlayLogic>().instance_Playlogic.GetComponent<Rigidbody>();
                instance_rb.isKinematic = true;
                instance_rb.AddForce(Vector3.up*100);
                cat_ani.SetBool("ismove2", false);
                int ran = Random.Range(0, 2);
                if (ran == 0)
                    cat_ani.SetBool("toyball", true);
                else
                    cat_ani.SetBool("toyball2", true);
                playLogic_obj.GetComponent<PlayLogic>().isThrow =false;
                //playLogic_obj.GetComponent<PlayLogic>().hit = false;
                if (ran == 0)
                    StartCoroutine(DelayTime(ran));
                else
                    StartCoroutine(DelayTime(2));
            }
        }
        if (collision.collider.CompareTag("Toy3"))
        {

            if (playLogic_obj.GetComponent<PlayLogic>().isThrow2 == true && playLogic_obj.GetComponent<PlayLogic>().checktime == 1)
            {
                //야옹
                playLogic_obj.GetComponent<PlayLogic>().checktime = 2;
                playLogic_obj.GetComponent<PlayLogic>().Meow();
                playLogic_obj.GetComponent<PlayLogic>().instance_Playlogic.transform.position = catFrontPos.transform.position;
                //playLogic_obj.GetComponent<PlayLogic>().CameraChangeColl();
                playLogic_obj.GetComponent<PlayLogic>().hit = true;
                instance_rb = playLogic_obj.GetComponent<PlayLogic>().target_fishing.GetComponent<Rigidbody>();
                instance_rb.isKinematic = true;
                cat_ani.SetBool("ismove2", false);
                //고양이 rb 끄기?
                catrb = gameObject.GetComponent<Rigidbody>();
                catrb.isKinematic = true;
                int ran = Random.Range(0, 2);
                if (ran == 0)
                    cat_ani.SetBool("isgrab", true);
                else
                    cat_ani.SetBool("isgrab", true);
                Vector3 target = new Vector3(0f, 0.48f, -1.5f);
                transform.LookAt(target);
                playLogic_obj.GetComponent<PlayLogic>().isThrow2 = false;
                if (ran == 0)
                    StartCoroutine(DelayTime(ran));
                else
                    StartCoroutine(DelayTime(3));
            }
        }

    }
    // 01, 02, 03;
    private void Playendpanelopen(int i)
    {
        panel_reward.SetActive(true);

        image_reward = image_obj.GetComponent<Image>();
        if(i==1)
        {
            //심심한 상태였다면?
            if(data_obj.GetComponent<DataManager>().issimsim==true)
            {
                // toy1
                int random = Random.Range(0, text1_string.Length);
                image_text.text = catname+ text1_string[random];
                image_reward.sprite = toy1;
                //
                data_obj.GetComponent<DataManager>().IncreaseProgress2();
                playtimecheck();
                //광고를 보여주세요
                admanager_obj.GetComponent<AdManager>().ShowInterstitialAd();
            }
            // 심심하지 않았던 상태?
            else if(data_obj.GetComponent<DataManager>().issimsim == false)
            {
                int random = Random.Range(0, text4_string.Length);
                image_text.text = catname + text4_string[random];
                image_reward.sprite = toy1;
                playtimecheck();
                data_obj.GetComponent<DataManager>().IncreaseProgress();
                //광고를 보여주세요
                admanager_obj.GetComponent<AdManager>().ShowInterstitialAd();
            }
        }
        else if(i==2)
        {
            if (data_obj.GetComponent<DataManager>().issimsim == true)
            {
                // toy2
                int random = Random.Range(0, text2_string.Length);
                image_text.text = catname + text2_string[random];
                image_reward.sprite = toy2;
                //
                data_obj.GetComponent<DataManager>().IncreaseProgress2();
                playtimecheck();
                //광고를 보여주세요
                admanager_obj.GetComponent<AdManager>().ShowInterstitialAd();
            }
            else if (data_obj.GetComponent<DataManager>().issimsim == false)
            {
                data_obj.GetComponent<DataManager>().IncreaseProgress();
                int random = Random.Range(0, text4_string.Length);
                image_text.text = catname + text4_string[random];
                image_reward.sprite = toy2;
                playtimecheck();
                //광고를 보여주세요
                admanager_obj.GetComponent<AdManager>().ShowInterstitialAd();
            }
        }
        else if (i == 3)
        {
            if (data_obj.GetComponent<DataManager>().issimsim == true)
            {
                data_obj.GetComponent<DataManager>().IncreaseProgress2();
                // toy3
                int random = Random.Range(0, text3_string.Length);
                image_text.text = catname + text3_string[random];
                image_reward.sprite = toy3;
                //
                data_obj.GetComponent<DataManager>().IncreaseProgress();
                playtimecheck();
                //광고를 보여주세요
                admanager_obj.GetComponent<AdManager>().ShowInterstitialAd();
            }
            else if (data_obj.GetComponent<DataManager>().issimsim == false)
            {
                data_obj.GetComponent<DataManager>().IncreaseProgress();
                int random = Random.Range(0, text4_string.Length);
                image_text.text = catname + text4_string[random];
                image_reward.sprite = toy3;
                playtimecheck();
                //광고를 보여주세요
                admanager_obj.GetComponent<AdManager>().ShowInterstitialAd();
            }
        }
        else
        {
            int ramo = Random.Range(1, 11);
            rewardvalue = ramo * 100;
            //0이거나 else일 경우에는 돈을 주자
            int random = Random.Range(0, text0_string.Length);
            image_text.text = catname + text0_string[random]+" 동전을 주웠습니다. 다 세어보니 "+rewardvalue+"원 정도 되는 것 같습니다.";
            image_reward.sprite = money;
            data_obj.GetComponent<DataManager>().play_rewards(ramo);
            playtimecheck();
        }
        
    }

    IEnumerator DelayTime(int i)
    {
        yield return new WaitForSeconds(7f);
        Playendpanelopen(i);
       
    }

    //놀아줬을때 시간 재입력
    void playtimecheck()
    {
        string st = data_obj.GetComponent<DataManager>().nowTime();
        Debug.Log("string" + st);
        PlayerPrefs.SetString("toyplay", st);
        PlayerPrefs.Save();
        Debug.Log("놀아준 시간 저장!");
    }

}
