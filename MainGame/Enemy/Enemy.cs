using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EnemyState
{ 
   Idle,
    Walk,
    Attack,
    Death,
    Wound
}
public class Enemy : MonoBehaviour {
    public EnemyState  enemyState=EnemyState.Idle;
    public float currentHp;
    public float totalHP;
    public float exp;
    public int attack;
    public float missRate;
    public string aniNameIdle;
    public string aniNameWalk;
    public string aniNameDeath;
    public string aniNameNormalAttack;
    public string aniNameNow;
    public float timeAniNameNormalAttack;
    public string aniNameCrazyAttack;
    public float timeAniNameCrazyAttack;
    public float crazyAttackRate;//普攻暴击率
    [Range(0,1)]
    public float attackRate;//攻速
    public float attackTimer;
    public Transform attackTarget;//攻击的英雄位置
    public float minDistance;
    public float maxDistance; 
    public float pursuitSpeed;//追击速度
    public float patrolTime;//攻击计时器
    public float patrolTimer;
    public float patrolSpeed;
    public EnemySpawn enemySpawn;

    private float missTime=1.5f;
    private new Animation animation;
    
    private CharacterController characterController;
    private new Renderer renderer;
    private Color previousColor;
    private Image fillImage;
    private Transform missTransform;
    private PlayerAttack playerAttack;
	// Use this for initialization
	void Start () {
        animation = GetComponent<Animation>();
        characterController = GetComponent<CharacterController>();
        
        renderer = transform.Find("shader").GetComponent<Renderer>();
        fillImage = transform.Find("SliderCanvas/hpBg/Image").GetComponent<Image>();
        missTransform = transform.Find("SliderCanvas/miss");
        missTransform.gameObject.SetActive(false);
        fillImage.fillAmount = currentHp / totalHP;
        previousColor = renderer.material.color;
        playerAttack=GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAttack>();
	}
	
	// Update is called once per frame
    void Update()
    {

        switch (enemyState)
        {
            case EnemyState.Idle:
            case EnemyState.Walk:
                Patrol();
                break;
            case EnemyState.Attack:
                AutoAttack();
                break;
            case EnemyState.Death:
                animation.CrossFade(aniNameDeath);
                break;
            case EnemyState.Wound:             
                break;
        }

        if (missTransform.gameObject.activeInHierarchy)
        {
            missTime -= Time.deltaTime;
            if (missTime <= 0)
            {
                missTransform.gameObject.SetActive(false);
                missTime = 1.5f;
            }
        }
    }

    private void Patrol()
    {
        animation.CrossFade(aniNameNow);
        if (aniNameNow == aniNameWalk)
        {
            characterController.SimpleMove(transform.forward * patrolSpeed);
        }
        patrolTimer += Time.deltaTime;
        if (patrolTimer > patrolTime)
        {
            patrolTimer = 0;
            RandomState();
        }
    }

    private void RandomState()
    {
        int value = Random.Range(0, 2);
        if (value == 0)
        {
            aniNameNow = aniNameIdle;
        }
        else
        {
            if (aniNameNow != aniNameWalk)
            {
                transform.Rotate(transform.up, Random.Range(0, 360));
            }
            aniNameNow = aniNameWalk;
        }
    }

    public bool TakeDamage(float attack, Transform player)
    {
        if (enemyState == EnemyState.Death)
        {
            return true;
        }
        enemyState = EnemyState.Attack;
        attackTarget = player;
        aniNameNow = aniNameNormalAttack;
        float missValue = Random.Range(0, 1f);
        if (missValue < missRate)
        {
            missTransform.gameObject.SetActive(true);
        }
        else
        {
            currentHp -= attack;
            StartCoroutine(ShowBobyRed());
            fillImage.fillAmount = currentHp / totalHP;
            if (this.currentHp <= 0)
            {
                enemyState = EnemyState.Death;
                GameObject.Destroy(this.gameObject, 2f);
                if (enemySpawn != null)
                { 
                    enemySpawn.currentNum--;
                }                
                attackTarget.GetComponent<PlayerStatus>().GainExp(exp);
                attackTarget = null;

                switch (this.name)
                {
                    case "Feishe_01(Clone)":
                        BarNPC.instance.sheKillCount++;
                        break;
                    case "DunJiaZhongHun_01(Clone)":
                         BarNPC.instance.dunKillCount++;
                        break;
                    case"ChangDaoZhongHun_04(Clone)":
                        BarNPC.instance.daoKillCount++;
                        break;
                }
                return true;
            }
        }
        return false;
    }


    IEnumerator  ShowBobyRed()
    {
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        renderer.material.color = previousColor;
    }
    void AutoAttack()
    {
        if (attackTarget != null)
        {
            float distance = Vector3.Distance(transform.position, attackTarget.position);
            if (distance > maxDistance)
            {
                attackTarget = null;
                enemyState = EnemyState.Idle;
                currentHp = totalHP;
                fillImage.fillAmount = 1;
            }
            else if (distance <= minDistance)
            {
                transform.LookAt(attackTarget);
                attackTimer += Time.deltaTime;
                animation.CrossFade(aniNameNow);
                if (aniNameNow == aniNameNormalAttack)
                {
                    if (attackTimer > timeAniNameNormalAttack)
                    {
                        bool isDead=attackTarget.GetComponent<PlayerAttack>().TakeDamage(attack);
                        if (isDead)
                        {
                            attackTarget = null;
                            enemyState = EnemyState.Idle;
                            currentHp = totalHP;
                            fillImage.fillAmount = 1;
                        }
                        aniNameNow = aniNameIdle;
                    }
                }
                else if (aniNameNow == aniNameCrazyAttack)
                {
                    if (attackTimer > timeAniNameCrazyAttack)
                    {

                        bool isDead=attackTarget.GetComponent<PlayerAttack>().TakeDamage(attack*2);
                        if (isDead)
                        {
                            attackTarget = null;
                            enemyState = EnemyState.Idle;
                            currentHp = totalHP;
                            fillImage.fillAmount = 1;
                        }
                        aniNameNow = aniNameIdle;
                    }
                }
                if (attackTimer > (2f / attackRate))
                {
                    attackTimer = 0;
                    RandomAttack();
                }
            }
            else
            {
                transform.LookAt(attackTarget);
                characterController.SimpleMove(transform.forward * patrolSpeed);
                animation.CrossFade(aniNameWalk);
            }
        }
        else
        {
            enemyState = EnemyState.Idle;
            currentHp = totalHP;
            fillImage.fillAmount = 1;
        }
    }

    private void RandomAttack()
    {
        float value = Random.Range(0, 1f);
        if (value < crazyAttackRate)
        {
            aniNameNow = aniNameCrazyAttack;
        }
        else
        {
            aniNameNow = aniNameNormalAttack;
        }
    }
    void OnMouseEnter()
    {
        if(playerAttack.playerState==PlayerState.SkillAttack)
        {
            return;
        }
        CursorManager.instance.SetCursorAttack();
    }
    void OnMouseExit()
    {
        if (playerAttack.playerState == PlayerState.SkillAttack)
        {
            return;
        }
        CursorManager.instance.SetCursorNormal();
    }
}

