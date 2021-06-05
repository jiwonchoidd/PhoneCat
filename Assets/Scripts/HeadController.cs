using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
 [SerializeField] Transform target;
[SerializeField] Transform headBone;
[SerializeField] float headMaxTurnAngle;
[SerializeField] float headTrackingSpeed;
private float speedModifier;
private float timer=0;
public GameObject dataobj;
[SerializeField] ParticleSystem particleObject;
private int petstat=0;
public int pettinglevellim=100;
void LateUpdate()
{    if(Input.touchCount == 0){
     target.transform.position=new Vector3(0, -50 , 0);

      timer=0;
      }
    
     if(Input.touchCount > 0)
         {

            Touch touch=Input.GetTouch(0);
            Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // 변환 안하고 바로 Vector3로
            Ray ray = Camera.main.ScreenPointToRay(theTouch);    // 터치한 좌표 레이로 바꾸엉
            RaycastHit hit;    // 정보 저장할 구조체 만들고
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
        // 캣이라는 태그를 가진 콜라이더에 닿은 레이캐스트
        if(hit.collider.CompareTag("cat"))
            {
              headMove();
               timer += Time.deltaTime;
            target.transform.position=new Vector3(target.transform.position.x +touch.deltaPosition.x
            *speedModifier,target.transform.position.y,target.transform.position.z);
              if(timer>0.5f && timer<3)
              {
              particleAuraPlay();
              ++petstat;
              if(petstat==pettinglevellim){
              dataobj.GetComponent<DataManager>().IncreaseProgress();
              petstat=0;
              }
              }
            }
        }
        }
}

     public void particleAuraPlay(){
      if(!particleObject.isPlaying){
          particleObject.Play();
       }     
    }
void headMove(){
  Quaternion currentLocalRotation = headBone.localRotation;
  headBone.localRotation = Quaternion.identity;

  Vector3 targetWorldLookDir = target.position - headBone.position;
  Vector3 targetLocalLookDir = headBone.InverseTransformDirection(targetWorldLookDir);

  targetLocalLookDir = Vector3.RotateTowards(
    Vector3.forward,
    targetLocalLookDir,
    Mathf.Deg2Rad * headMaxTurnAngle, // Note we multiply by Mathf.Deg2Rad here to convert degrees to radians
    0 // We don't care about the length here, so we leave it at zero
  );

  // Get the local rotation by using LookRotation on a local directional vector
  Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, Vector3.up);

  // Apply smoothing
  headBone.localRotation = Quaternion.Slerp(
    currentLocalRotation,
    targetLocalRotation, 
    1 - Mathf.Exp(-headTrackingSpeed * Time.deltaTime)
  );
}

}
