using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowView : MonoBehaviour {
    private Transform Character;
    private Vector3 offDistance;
    private float scrollSpeed = 2f;
    private float rotateSpeed=2f;

	void Start () {
        Character = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Transform>();
        transform.LookAt(Character.position);
        offDistance = transform.position-Character.position;   
	}
	

    void LateUpdate()
    {
        transform.position = Character.position+offDistance;
        ScrollView();
        RotateView();
    }
    //视野的拉近与拉远
    void ScrollView()
    { 
        float distance=offDistance.magnitude;
        distance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance=Mathf.Clamp(distance,6,33);
        offDistance=distance * offDistance.normalized;
    }
    //视野的左右,上下旋转
    void RotateView()
    { 
        if(Input.GetMouseButton(1))
        {
            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;
           transform.RotateAround(Character.position,Character.up,rotateSpeed*Input.GetAxis("Mouse X"));
           transform.RotateAround(Character.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
           if (transform.eulerAngles.x < 3.3f || transform.eulerAngles.x > 76f)
           {
               transform.position = originalPos;
               transform.rotation = originalRotation;
           }
           offDistance = transform.position-Character.position;
        }
       
    }
}
