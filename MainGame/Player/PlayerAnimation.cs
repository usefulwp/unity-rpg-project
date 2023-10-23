using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private new Animation animation;
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;
	// Use this for initialization
	void Start () {

        animation = this.GetComponent<Animation>();
        playerMove = this.GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerAttack.playerState == PlayerState.ControlWalk)
        {
            if (playerMove.characterState.Equals(CharacterState.Idle))
            {
                animation.Play("Idle");
            }
            else if (playerMove.characterState.Equals(CharacterState.Move))
            {
                animation.Play("Run");
            }
        }
        else if (playerAttack.playerState==PlayerState.NormalAttack)
        {
           if(playerAttack.attackState==AttackState.Moving)
           {
               animation.Play("Run");
           }
        }
      
	}
    public void PlayAni(string aniName)
    {
        animation.Play(aniName);
    }
}
