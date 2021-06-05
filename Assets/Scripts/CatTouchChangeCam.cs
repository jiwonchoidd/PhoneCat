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
        //��ġ ���� 0���� Ŭ��
        if (Input.touchCount > 0)
        {
            Vector2 pos = Input.GetTouch(0).position;    // ��ġ�� ��ġ
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // ��ȯ ���ϰ� �ٷ� Vector3��
            Ray ray = Camera.main.ScreenPointToRay(theTouch);    // ��ġ�� ��ǥ ���̷� �ٲپ�
            RaycastHit hit;    // ���� ������ ����ü �����
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // Ĺ�̶�� �±׸� ���� �ݶ��̴��� ���� ����ĳ��Ʈ
                if (hit.collider.CompareTag("cat"))
                {
                    // �� 4�� ��ġ �ϸ� ���� ������
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
