using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipHint : MonoBehaviour
{
    // Start is called before the first frame update
    private string[] tip_string = { "����̴� �踦 ������ �Ⱦ��մϴ�.", "�� ����� ������ ������ �� �ֽ��ϴ�.", "���� �Դ� �ð��� �ִ�ϴ�.", "�ɽ��Ҷ� ����ָ� ģ�е��� �׿���!","���л� ���� �� ������ ��������ϴ�."
                                        , "â���� ���� ���� �ð��� �̷��� �Ǿ��� ����.", "������ jae2021@gmail.com", "������ minwha0617@gmail.com", "����̸� ���� �����ؼ� ���� ��������.", "����̰� ������ ���� ������ ���� �˴ϴ�."
                                , "����̴� �������� �����մϴ�.", "��� �÷��̸� ���� ���ο� ����̷� Ű��������.", "AR����� ���� ����̿� �߾��� ��������.", "���� : �����ڴ� ���� �����Դϴ�.", "�Ŀ� �Ŀ� �Ŀ� �Ŀ� �Ŀ�"
                                ,"��Ĺ�� �������� AR�� �����ռ� ����� ����߽��ϴ�."};
    public Text tip_text;
    void Start()
    {
        Tip();
    }

    private void Tip()
    {
        int random = Random.Range(0, tip_string.Length);
        tip_text.text = "TIP - "+ tip_string[random];
    }
}
