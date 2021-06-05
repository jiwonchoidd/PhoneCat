using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeAnime : MonoBehaviour
{
    public GameObject CatSpawner;
    private Animator animator;
    public Slider animeSlider;
    private int checktime = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = CatSpawner.GetComponent<ARplaceOnPlane>().spawnObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator = CatSpawner.GetComponent<ARplaceOnPlane>().spawnObject.GetComponent<Animator>();
        if (animeSlider.value==1 && checktime==0)
        {
            //기본값이라 아무것도 안해줌
            checktime=1;
   
        }
        if(animeSlider.value==2 && checktime == 0)
        {
            //
            animator.SetBool("isgrab", true);
            checktime = 1;
            
        }
        if(animeSlider.value==3 && checktime == 0)
        {
            animator.SetBool("issit", true);
            checktime = 1;
           
        }
        if(animeSlider.value==4 && checktime == 0)
        {
            animator.SetBool("issit2", true);
            checktime = 1;
           
        }
        if(animeSlider.value==5 && checktime == 0)
        {
            animator.SetBool("issit3", true);
            checktime = 1;
            
        }
        if (animeSlider.value == 6 && checktime == 0)
        {
            animator.SetBool("isidle", true);
            checktime = 1;
        
        }

    }
    public void idle()
    {
        animator.SetBool("isgrab", false);
        animator.SetBool("issit", false);
        animator.SetBool("issit2", false);
        animator.SetBool("issit3", false);
        animator.SetBool("isidle", false);
        checktime = 0;
    }
}
