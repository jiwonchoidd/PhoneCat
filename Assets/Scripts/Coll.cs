using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Coll : MonoBehaviour
{
    //���� ���� ������Ʈ 
    public GameObject admanager_obj;
    private string catname;
    private string[] text1_string = {"�� �ǹ�ġ�� ����ֿ����ϴ�. ģ�е��� ����߽��ϴ�.", "�� �ǹ�ġ�� ����ֿ����ϴ�. ����̿� �� �� ����������ϴ�.", "�� �ǹ�ġ�� ����ֿ����ϴ�. ����̴� ������ �����ϴ�.." };
    private string[] text2_string = { "�� �㵹�̷� ����ֿ����ϴ�. ģ�е��� ����߽��ϴ�.", "�� �㵹�̷� ����ֿ����ϴ�. �� �� ģ������ ����..", "�� �㵹�̷� ����ֿ����ϴ�. ����̴� ������ �����ϴ�.." };
    private string[] text3_string = { "�� ���ô�� ����ֿ����ϴ�. ģ�е��� ����߽��ϴ�.", "�� ���ô�� ����ֿ����ϴ�. ���ô밡 �μ�������", "�� ���ô�� ����ֿ����ϴ�. ����̴� ������ �����ϴ�.." };
    private string[] text0_string = { "(��)�� ��鼭 ��� �ؿ��� ", "(��)�� ��鼭 ���� �ؿ���", "(��)�� ��鼭 TV �ؿ���", "(��)�� ��鼭 ��� �� �ؿ���", "(��)�� ��鼭 ��� �ڵ��� �ؿ���", "(��)�� ��鼭 ������ �ؿ���" };
    private string[] text4_string = { "(��)�� ��ſ��մϴ�. ������ ���� ���� �;� �մϴ�. ", "(��)�� ������ �� ���;� ���� �ʴ� �� �մϴ�.", "(��)�� �ſ� �������մϴ�. ������ ȥ�ڸ��� �ð��� �ʿ��� �� �����ϴ�." };
    //���� ������Ʈ�� �ε�����..�׿� �´� �ִϸ��̼��� ����

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
                //�߿�
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
                //�߿�
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
                //�߿�
                playLogic_obj.GetComponent<PlayLogic>().checktime = 2;
                playLogic_obj.GetComponent<PlayLogic>().Meow();
                playLogic_obj.GetComponent<PlayLogic>().instance_Playlogic.transform.position = catFrontPos.transform.position;
                //playLogic_obj.GetComponent<PlayLogic>().CameraChangeColl();
                playLogic_obj.GetComponent<PlayLogic>().hit = true;
                instance_rb = playLogic_obj.GetComponent<PlayLogic>().target_fishing.GetComponent<Rigidbody>();
                instance_rb.isKinematic = true;
                cat_ani.SetBool("ismove2", false);
                //����� rb ����?
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
            //�ɽ��� ���¿��ٸ�?
            if(data_obj.GetComponent<DataManager>().issimsim==true)
            {
                // toy1
                int random = Random.Range(0, text1_string.Length);
                image_text.text = catname+ text1_string[random];
                image_reward.sprite = toy1;
                //
                data_obj.GetComponent<DataManager>().IncreaseProgress2();
                playtimecheck();
                //���� �����ּ���
                admanager_obj.GetComponent<AdManager>().ShowInterstitialAd();
            }
            // �ɽ����� �ʾҴ� ����?
            else if(data_obj.GetComponent<DataManager>().issimsim == false)
            {
                int random = Random.Range(0, text4_string.Length);
                image_text.text = catname + text4_string[random];
                image_reward.sprite = toy1;
                playtimecheck();
                data_obj.GetComponent<DataManager>().IncreaseProgress();
                //���� �����ּ���
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
                //���� �����ּ���
                admanager_obj.GetComponent<AdManager>().ShowInterstitialAd();
            }
            else if (data_obj.GetComponent<DataManager>().issimsim == false)
            {
                data_obj.GetComponent<DataManager>().IncreaseProgress();
                int random = Random.Range(0, text4_string.Length);
                image_text.text = catname + text4_string[random];
                image_reward.sprite = toy2;
                playtimecheck();
                //���� �����ּ���
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
                //���� �����ּ���
                admanager_obj.GetComponent<AdManager>().ShowInterstitialAd();
            }
            else if (data_obj.GetComponent<DataManager>().issimsim == false)
            {
                data_obj.GetComponent<DataManager>().IncreaseProgress();
                int random = Random.Range(0, text4_string.Length);
                image_text.text = catname + text4_string[random];
                image_reward.sprite = toy3;
                playtimecheck();
                //���� �����ּ���
                admanager_obj.GetComponent<AdManager>().ShowInterstitialAd();
            }
        }
        else
        {
            int ramo = Random.Range(1, 11);
            rewardvalue = ramo * 100;
            //0�̰ų� else�� ��쿡�� ���� ����
            int random = Random.Range(0, text0_string.Length);
            image_text.text = catname + text0_string[random]+" ������ �ֿ����ϴ�. �� ����� "+rewardvalue+"�� ���� �Ǵ� �� �����ϴ�.";
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

    //��������� �ð� ���Է�
    void playtimecheck()
    {
        string st = data_obj.GetComponent<DataManager>().nowTime();
        Debug.Log("string" + st);
        PlayerPrefs.SetString("toyplay", st);
        PlayerPrefs.Save();
        Debug.Log("����� �ð� ����!");
    }

}
