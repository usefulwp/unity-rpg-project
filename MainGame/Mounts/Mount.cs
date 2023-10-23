using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mount : MonoBehaviour {
    public Transform player;   
    public float moveSpeed;
    public float followDistance;

    private CharacterController characterController;
    private new Animation animation;
    private bool isFollow=false;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        characterController = GetComponent<CharacterController>();
        animation = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        Follow();
	}
    void OnMouseEnter()
    {
        CursorManager.instance.SetCursorNPCTalk();
    }
    void OnMouseExit()
    {
        CursorManager.instance.SetCursorNormal();
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        { 
          isFollow=true;
        }
    }
    void Follow()//跟随
    {
        if(isFollow)
        {
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            if (Vector3.Distance(player.position, transform.position) > followDistance)
            {
                animation.Play("Run");
                characterController.SimpleMove(moveSpeed * transform.forward);
            }
            else
            {
                animation.Play("Idle");
            }
        }       
    }
}

