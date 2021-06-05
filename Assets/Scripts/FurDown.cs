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


        //���� Ŭ���ϸ� ���� ������� ���� ��´�. 
        //��ġ ���� 0���� Ŭ��
        if (Input.touchCount > 0)
        {
            Vector2 pos = Input.GetTouch(0).position;    // ��ġ�� ��ġ
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // ��ȯ ���ϰ� �ٷ� Vector3��
            Ray ray = Camera.main.ScreenPointToRay(theTouch);    // ��ġ�� ��ǥ ���̷� �ٲٱ�
            RaycastHit hit;    // ���� ������ ����ü �����
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // ���̶�� �±׸� ���� �ݶ��̴��� ���� ����ĳ��Ʈ
                if (hit.collider.CompareTag("Fur"))
                {

                    // ��ġ�� ���� �۵��մϴ�.
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
                        // ���̶�� �±׸� ���� �ݶ��̴��� ���� ����ĳ��Ʈ
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
        Debug.Log(randomtime+"�� �ڿ� ���� ���ɴϴ�.");
        yield return new WaitForSeconds(randomtime);
        FurDrop();
        }
    }

    private void FurGetit(RaycastHit hit)
    {
        //����ü�� ����� ���� ������Ʈ ��, ���õ� ������Ʈ ����
   
        
        Destroy(hit.transform.gameObject);
        audioSource.clip = getFur_SFX;
        if (!audioSource.isPlaying)
            audioSource.Play();

        // ���� �����ϸ� ���� �ݴϴ�.
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
