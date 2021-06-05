using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProfileText_panel : MonoBehaviour
{
    // Start is called before the first frame update
    public Text gender;
    public Text type;
    public Text like;
    public Text dislike;

    public Text profile_name; // 프로필에 이름에 넣어볼거야~

    private string name; // 이름 입력한걸 가져올거야
    
    
    public GameObject catImage;
    public Image dataobj;
    public Sprite cat0;
    public Sprite cat1;
    public Sprite cat2;
    public Sprite cat3;
    public Sprite cat4;
    public Sprite cat5;
    public Sprite cat6;
    public Sprite cat7;
    public Sprite cat8;
    public Sprite cat9;
    public Sprite cat10;
    public Sprite cat11;
    public Sprite cat12;
    public Sprite cat13;
    public Sprite cat14;
    public Sprite cat15;
    public Sprite cat16;
    public Sprite cat17;

    int skin;


    public void _save()
    {
        PlayerPrefs.SetString("name", name);
        PlayerPrefs.Save();
    }

    public void _load()
    {
        
        
    }

        void Start()
    {
        
        profileAdapt();
        dataobj=catImage.GetComponent<Image>();
    }

    public void profileAdapt(){

        
        skin =PlayerPrefs.GetInt("skin");
        if(skin==0) //뱅갈
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat0;
            gender.text = "남";
            type.text = "느긋함";
            like.text = "놀아주기";
            dislike.text = "외로움";
        }
        else if(skin==1) //회색 뱅갈
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat1;
            gender.text = "남";
            type.text = "활동적";
            like.text = "놀아주기";
            dislike.text = "외로움";
        }
        else if(skin==2) //블랙&턱시도
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat2;
            gender.text = "남";
            type.text = "사교적";
            like.text = "놀아주기";
            dislike.text = "외로움";

        }
        else if(skin==3) //치즈
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat3;
            gender.text = "남";
            type.text = "활동적";
            like.text = "놀아주기";
            dislike.text = "무관심";
        }
        else if(skin==4) //그레이 뱅갈
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat4;
            gender.text = "남";
            type.text = "느긋함";
            like.text = "놀아주기";
            dislike.text = "외로움";
        }
        else if(skin==5) //러시안블루
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat5;
            gender.text = "남";
            type.text = "온순함";
            like.text = "놀아주기";
            dislike.text = "장난치기";
        }
        else if(skin==6) //샴
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat6;
            gender.text = "남";
            type.text = "온순함";
            like.text = "놀아주기";
            dislike.text = "외로움";
        }
        else if(skin==7) //블랙&턱시도
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat7;
            gender.text = "남";
            type.text = "사교적";
            like.text = "놀아주기";
            dislike.text = "외로움";
        }
        else if(skin==8) //화이트
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat8;
            gender.text = "남";
            type.text = "예민함";
            like.text = "조용함";
            dislike.text = "과한관심";
        }
        else if(skin==9) //뱅갈
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat0;
            gender.text = "여";
            type.text = "느긋함";
            like.text = "놀아주기";
            dislike.text = "외로움";
        }
        else if(skin==10) // 그레이뱅갈
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat1;
            gender.text = "여";
            type.text = "느긋함";
            like.text = "놀아주기";
            dislike.text = "외로움";
        }
        else if(skin==11) // 블랙&턱시도
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat2;
            gender.text = "여";
            type.text = "사교적";
            like.text = "놀아주기";
            dislike.text = "외로움";
        }
        else if(skin==12) // 치즈
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat3;
            gender.text = "여";
            type.text = "활동적";
            like.text = "놀아주기";
            dislike.text = "무관심";
        }
        else if(skin==13) // 그레이 뱅갈
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat4;
            gender.text = "여";
            type.text = "느긋함";
            like.text = "놀아주기";
            dislike.text = "외로움";
        }
        else if(skin==14) // 러시안블루
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat5;
            gender.text = "여";
            type.text = "온순함";
            like.text = "놀아주기";
            dislike.text = "장난치기";
        }
        else if(skin==15) // 샴
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat6;
            gender.text = "여";
            type.text = "온순함";
            like.text = "놀아주기";
            dislike.text = "외로움";
        }
        else if(skin==16) // 블랙&턱시도
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat7;
            gender.text = "여";
            type.text = "사교적";
            like.text = "놀아주기";
            dislike.text = "외로움";
        }
        else if(skin==17) //화이트
        {
            name = PlayerPrefs.GetString("name");
            profile_name.text = name;
            dataobj.sprite = cat8;
            gender.text = "여";
            type.text = "예민함";
            like.text = "조용함";
            dislike.text = "과한관심";
        }
    }    
}
