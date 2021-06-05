using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class RandomMove_Bluetooth : MonoBehaviour
{
    private bool randommove = true;
    //애니메이션
    public Animator animator;
    //private bool istickle = false;
    //public bool iscatcome = false;
    
    //private float speedModifier;

    public float moveSpeed = 3f;
    public float rotSpeed = 100f;
    private bool isWandering = false;
    private bool isRotationLeft = false;
    private bool isRotationRight = false;
    private bool isWalking = false;
   // private bool isGrab = false;
    private bool isIdle = false;
    private bool isSitdown = false;
    private bool isSitdown2 = false;
    private bool isSitdown3 = false;

    //private int cattouchcount = 0;
    //float timer = 0;
    // 사운드 
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip[] ad;

    //public AudioClip walksound;
    // Start is called before the first frame update
    void Start()
    {
        //speedModifier = 0.015f;
        audioSource = GetComponent<AudioSource>();
        audioSource2 = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {


        // 헤매는 코드 코루틴함수사용     
        if (randommove == true )
        {
            if (isWandering == false)
            {
                StartCoroutine(Wander());
            }
            if (isRotationRight == true)
            {
                animator.SetBool("ismove", true);
                transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;

            }
            if (isRotationLeft == true)
            {
                animator.SetBool("ismove", true);
                transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            if (isWalking == true)
            {
                //음쥑이는 모션~!
                animator.SetBool("ismove", true);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            if (isIdle == true)
            {
                animator.SetBool("isidle", true);
            }
            if (isSitdown == true)
            {
                animator.SetBool("issit", true);
            }
            if (isSitdown2 == true)
            {
                animator.SetBool("issit2", true);
            }
            if (isSitdown3 == true)
            {
                animator.SetBool("issit3", true);
            }

        }
    }
    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 2);
        int rotateWait = Random.Range(1, 3);
        int rotateLorR = Random.Range(0, 7);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(4, 10);
        int idletime = Random.Range(2, 8);
        int sitdowntime = Random.Range(6, 10);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        //WalkSound();
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        //애니메이션 끄기
        animator.SetBool("ismove", false);
        //
        yield return new WaitForSeconds(rotateWait);

        if (rotateLorR == 1)
        {
            isRotationRight = true;
            //WalkSound();
            yield return new WaitForSeconds(rotTime);
            animator.SetBool("ismove", false);
            isRotationRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotationLeft = true;
            //WalkSound();
            yield return new WaitForSeconds(rotTime);
            animator.SetBool("ismove", false);
            isRotationLeft = false;
        }
        if (rotateLorR == 3)
        {
            isIdle = true;
            yield return new WaitForSeconds(idletime);
            animator.SetBool("isidle", false);
            isIdle = false;
        }
        if (rotateLorR == 4)
        {
           
            isSitdown = true;
            yield return new WaitForSeconds(sitdowntime);
            animator.SetBool("issit", false);
            isSitdown = false;
        }
        if (rotateLorR == 5)
        {
            if (!audioSource.isPlaying)
                Meow();
            isSitdown2 = true;
            yield return new WaitForSeconds(sitdowntime);
            animator.SetBool("issit2", false);
            isSitdown2 = false;
        }
        if (rotateLorR == 6)
        {

            isSitdown3 = true;
            yield return new WaitForSeconds(sitdowntime);
            animator.SetBool("issit3", false);
            isSitdown3 = false;
        }
        isWandering = false;
    }

    void Meow()
    {
        audioSource.clip = ad[Random.Range(0, ad.Length)];
        audioSource.Play();
    }
   


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Debug.Log("벽에 부딪힘");
            Debug.Log(collision.gameObject);
            transform.Rotate(0, 180, 0);
            if (!audioSource.isPlaying)
                Meow();
        }
        if (collision.collider.CompareTag("cat"))
        {
            Debug.Log("고양이에 부딪힘");
            Debug.Log(collision.gameObject);
            transform.Rotate(0, 180, 0);
        }
    }

 

}
