using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
  Idle,
    Move
}
public class PlayerMove : MonoBehaviour {
    public CharacterState characterState = CharacterState.Idle;
    public bool IsMoving { get; set; }
    private CharacterController characterController;
    private PlayerDir playDir;
    private PlayerAttack playerAttack;
    public float moveSpeed=3f;
	// Use this for initialization
    void Awake()
    {
        playDir = this.GetComponent<PlayerDir>();
        characterController = this.GetComponent<CharacterController>();
        playerAttack = GetComponent<PlayerAttack>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerAttack.playerState == PlayerState.ControlWalk)
        {
           
            float distance = Vector3.Distance(transform.position, playDir.TargetPos);
            if (distance > 0.3f)
            {
                IsMoving = true;
                characterState = CharacterState.Move;

                characterController.SimpleMove(transform.forward * moveSpeed);

            }
            else
            {
                IsMoving = false;
                characterState = CharacterState.Idle;
            }
        }

     
	}

    public void SimpleMove(Vector3 target)
    {
        transform.LookAt(target);
        characterController.SimpleMove(moveSpeed * transform.forward);
    }
    
}
