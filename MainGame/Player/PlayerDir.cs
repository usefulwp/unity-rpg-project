using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerDir : MonoBehaviour {
    private PlayerAttack playerAttack;
    private  Vector3 targetPos;
    public Vector3 TargetPos
    {
        get { return targetPos; }
        set { targetPos = value; }
    }
    private TextAsset textAsset;
    private string[] pathArray;
    private GameObject clickEffect;
    private PlayerMove playerMove;

	// Use this for initialization
	void Start () {
       targetPos=transform.position;
        textAsset = Resources.Load<TextAsset>("SelectCharacter/TextInfo/path");
        clickEffect = Resources.Load<GameObject>("Effects/ClickMove");
        pathArray=textAsset.text.Split(',');
        playerMove=this.GetComponent<PlayerMove>();
        playerAttack=GetComponent<PlayerAttack>();

       
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && playerAttack.playerState != PlayerState.Death && playerAttack.playerState != PlayerState.SkillAttack)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayInfo;
            if (Physics.Raycast(ray, out rayInfo) && rayInfo.collider.tag == Tags.ground)
            {
                ShowClickEffect(rayInfo.point);
                LookAtTarget(rayInfo.point);
                playerAttack.playerState = PlayerState.ControlWalk;
                CursorManager.instance.SetCursorNormal();
            }
        }//
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && playerAttack.playerState != PlayerState.Death && playerAttack.playerState != PlayerState.SkillAttack)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayInfo;
            if (Physics.Raycast(ray, out rayInfo) && rayInfo.collider.tag == Tags.ground)
            {
                LookAtTarget(rayInfo.point);
            }
        }
        else
        {
            if (playerMove.IsMoving)
            {
                LookAtTarget(targetPos);
            }
        }
	}

  
    void ShowClickEffect(Vector3 point)
    {
        point = new Vector3(point.x,point.y+0.1f,point.z);

       GameObject.Instantiate<GameObject>(clickEffect, point, Quaternion.identity);
    }
    void LookAtTarget(Vector3 point)
    {
        TargetPos = new Vector3(point.x, transform.position.y, point.z);
        transform.LookAt(TargetPos);
    }
}
