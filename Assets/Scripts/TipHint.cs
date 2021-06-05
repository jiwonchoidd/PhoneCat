using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipHint : MonoBehaviour
{
    // Start is called before the first frame update
    private string[] tip_string = { "고양이는 배를 만지면 싫어합니다.", "눈 색깔로 성별을 구별할 수 있습니다.", "사료는 먹는 시간이 있답니다.", "심심할때 놀아주면 친밀도가 쌓여요!","대학생 둘이 이 게임을 만들었습니다."
                                        , "창밖을 보면 벌써 시간이 이렇게 되었나 싶죠.", "최지원 jae2021@gmail.com", "강동휘 minwha0617@gmail.com", "고양이를 털을 수집해서 돈을 얻으세요.", "고양이가 날리는 털을 잡으면 돈이 됩니다."
                                , "고양이는 높은곳을 좋아합니다.", "통신 플레이를 통해 새로운 고양이로 키워보세요.", "AR기능을 통해 고양이와 추억을 쌓으세요.", "정보 : 개발자는 랜선 집사입니다.", "냐옹 냐옹 냐옹 냐옹 냐옹"
                                ,"폰캣의 증강현실 AR은 영상합성 기술을 사용했습니다."};
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
