using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ARplaceOnPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public GameObject placeObject;
    //소환된 오브젝트
    public GameObject spawnObject;
    public GameObject toast;
    public bool hold=false;
    public Text scan_status;
    public Transform target;
    public GameObject catAnime;
    public GameObject slider;
    // float timer=0;
    // Update is called once per frame
    private void Start()
    {
        slider.SetActive(false);
        catAnime.SetActive(false);
    }
    void Update()
    {
        //UpdateCenterObject();
        if(hold==false)
        placeObjectByTouch();
       
    }

    private void UpdateCenterObject()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        if(hits.Count>0)
        {
            Pose placementPose=hits[0].pose;
            placeObject.SetActive(true);
            placeObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        
             scan_status.text="고양이 소환";   
        }
        else
        {
          placeObject.SetActive(false); 
          scan_status.text="바닥 스캔중..";   
        }
    }

    public void ChangeHoldState_Button()
    {   
        if(hold==false)
        {
        toast.GetComponent<AndroidPlugin>().ShowToast("위치 고정", false);
        hold=true;
        }
        else if(hold==true)
        {
        toast.GetComponent<AndroidPlugin>().ShowToast("고정 취소", false);
        
        //timer += Time.deltaTime;
        //if(timer>1f){
        hold=false;
       // timer=0;
       // }
        }
        
    }


    private void placeObjectByTouch()
    {   
        
        if(Input.touchCount>0 && hold==false && !IsPointerOverUIObject(Input.mousePosition))
        {
            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            return;

            else
            {
            Touch touch = Input.GetTouch(0);

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
            {
                Pose hitPose = hits[0].pose;
                if(!spawnObject)
                {
                        spawnObject = Instantiate(placeObject, hitPose.position, hitPose.rotation);
                scan_status.text="고양이 소환"; 
                Vector3 lookAtPosition = spawnObject.transform.position;
                lookAtPosition.x =transform.position.x;
                lookAtPosition.z =transform.position.z;
                transform.LookAt(lookAtPosition);
                        slider.SetActive(true);
                        catAnime.SetActive(true);
                    }
                else
                {
                    spawnObject.transform.position=hitPose.position;
                    spawnObject.transform.rotation=hitPose.rotation;
                    Vector3 lookAtPosition = spawnObject.transform.position;
                    lookAtPosition.x =transform.position.x;
                    lookAtPosition.z =transform.position.z;
                    spawnObject.transform.LookAt(lookAtPosition);
                 
                }
            }
            }
        }
    }
    public bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition
            = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = touchPos;

        List<RaycastResult> results = new List<RaycastResult>();


        EventSystem.current
        .RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

}
