using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum PlayerState
{ 
    ControlWalk,
    NormalAttack,
    SkillAttack,
    Death
}
public enum AttackState
{ 
    Moving,
    Idle,
    Attack
}
public class PlayerAttack :  MonoBehaviour{
    public PlayerState playerState = PlayerState.ControlWalk;
    public AttackState attackState = AttackState.Idle;
    public string aniNameIdle;
    public string aniNameNow;
    public string aniNameNormalAttack;
    public float timeAniNameNormalAttack;
    public float timer;//攻击计时器
    [Range(0,1)]
    public float attackRate;
    public Transform targetNormalAttack;
    public float minDistance;
    public bool isShowEffect=false;
    [Range(0, 1)]
    public float missRate;
    private GameObject normalEffectPrefab;
    private PlayerMove playerMove;
    private PlayerAnimation playerAnimation;
    private PlayerStatus playerStatus;
    private new Renderer renderer;
    private Color previousColor;
    private Image missImage;
    private float missTime=1.5f;
    private SkillInfo skillInfo;
    private ShortCutSlot shortCutSlot;
    private bl_HUDText hudRoot;
    private Vector3 bornPoint;
    private PlayerDir playerDir;
	// Use this for initialization
	void Start () {
        bornPoint = this.transform.position;
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponent<PlayerAnimation>();
        normalEffectPrefab = Resources.Load<GameObject>("Effects/hit-blue-1");
        playerStatus = GetComponent<PlayerStatus>();
        renderer=transform.Find("body").GetComponent<Renderer>();
        previousColor = renderer.material.color;
        missImage=transform.Find("PlayerCanvas/miss").GetComponent<Image>();
        missImage.gameObject.SetActive(false);
        hudRoot = GameObject.Find("HUDText").GetComponent<bl_HUDText>();
        playerDir = GetComponent<PlayerDir>();
	}
	
	// Update is called once per frame
	void Update () {
        SelectEnemy();
        NormalAttack();
        //单个目标
        SingleTarget();
        MultiTarget();
        if (missImage.gameObject.activeInHierarchy)
        {
            missTime -= Time.deltaTime;
            if (missTime <= 0)
            {
                missImage.gameObject.SetActive(false);
                missTime = 1.5f;
            }
        } 
	}
    //单个目标
    void SingleTarget()
    {
        if (playerState == PlayerState.SkillAttack && playerState != PlayerState.Death)
        {
            if (skillInfo!=null&&skillInfo.impactType == ImpactType.SingleTarget)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.tag.Equals(Tags.enemy))
                    {
                        if (Vector3.Distance(transform.position, hitInfo.collider.transform.position) > skillInfo.distance)
                        {
                            Debug.Log("超出施法距离");
                            CursorManager.instance.SetCursorNormal();
                            playerState = PlayerState.ControlWalk;
                            attackState = AttackState.Idle;
                        }
                        else//在技能范围内
                        {
                            if (shortCutSlot.OnUseSkillState(skillInfo))
                            {
                                StartCoroutine(OnUseSingleTargetSkill(skillInfo, hitInfo));
                            }
                        }
                    }
                }
            }
        }      
    }
    void MultiTarget()
    {
        if (playerState == PlayerState.SkillAttack && playerState != PlayerState.Death)
        {
            if (skillInfo != null&&skillInfo.impactType == ImpactType.MultiTarget)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (Vector3.Distance(transform.position, hitInfo.point) > skillInfo.distance)
                        {
                            Debug.Log("超出施法距离");
                            CursorManager.instance.SetCursorNormal();
                            playerState = PlayerState.ControlWalk;
                            attackState = AttackState.Idle;
                        }
                        else//在技能范围内
                        {
                            if (shortCutSlot.OnUseSkillState(skillInfo))
                            {
                                StartCoroutine(OnUseMultiTargetSkill(skillInfo,hitInfo));
                            }
                        }
                    }
                }
            }
        }
    }
    //处理群体技能目标
    IEnumerator OnUseMultiTargetSkill(SkillInfo skillinfo,RaycastHit hitInfo)
    {
        playerAnimation.PlayAni(skillinfo.animationName);
        yield return new WaitForSeconds(skillinfo.animationTime);
        playerState = PlayerState.ControlWalk;
        GameObject effect=GameObject.Instantiate(Resources.Load<GameObject>("Effects/frost-fx/" + skillinfo.effectName), hitInfo.point, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        effect.GetComponent<MagicSphere>().CauseDamage(playerStatus.TotalAttack,this.transform);
        CursorManager.instance.SetCursorNormal();
    }
    void SelectEnemy()
    {
        if (Input.GetMouseButtonDown(0)&&playerState!=PlayerState.Death&&playerState!=PlayerState.SkillAttack)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.tag == Tags.enemy)
            {
                targetNormalAttack = hitInfo.collider.transform;
                playerState = PlayerState.NormalAttack;
                timer = 0;
                isShowEffect = false;
            }
            else
            {
                playerState = PlayerState.ControlWalk;
                attackState = AttackState.Idle;
                targetNormalAttack = null;
            }
        }
    }
    void NormalAttack()
    {
        if (playerState == PlayerState.NormalAttack)
        {
            if (targetNormalAttack != null)
            {
                float distance = Vector3.Distance(transform.position, targetNormalAttack.position);
                if (distance <= minDistance)
                {
                    transform.LookAt(targetNormalAttack.position);
                    attackState = AttackState.Attack;
                    timer += Time.deltaTime;
                    playerAnimation.PlayAni(aniNameNow);
                    if (timer >= timeAniNameNormalAttack)
                    {
                        aniNameNow = aniNameIdle;//动画复位
                        if (!isShowEffect)
                        {
                            Vector3 dest=new Vector3(targetNormalAttack.position.x,targetNormalAttack.position.y+1.5f,targetNormalAttack.position.z);
                            GameObject.Instantiate(normalEffectPrefab,dest, Quaternion.identity);
                            bool isDead=targetNormalAttack.GetComponent<Enemy>().TakeDamage(playerStatus.TotalAttack, transform);
                            if (isDead)
                            {
                                targetNormalAttack = null;
                                playerState = PlayerState.ControlWalk;
                                attackState = AttackState.Idle;
                            }
                            isShowEffect = true;
                        }
                    }
                    if (timer > (1.4f/ attackRate))
                    {
                        timer = 0;
                        aniNameNow = aniNameNormalAttack;
                        isShowEffect = false;
                    }
                }
                else
                {
                    attackState = AttackState.Moving;
                    playerMove.SimpleMove(targetNormalAttack.position);
                }
            }
            else
            {
                playerState = PlayerState.ControlWalk;
            }
        }
    }

    public bool TakeDamage(float attack)//伤害计算  敌人攻击力*(250-defence)/250
    {
        if (playerState == PlayerState.Death) return true;
        float tempDamage = attack * (250 - playerStatus.TotalDefence) / 250;
        tempDamage= tempDamage < 0 ? 20 : tempDamage;
        float missValue = Random.Range(0,1f);
        if (missValue < missRate)
        {
            missImage.gameObject.SetActive(true);
        }
        else
        {
            playerStatus.CurrentHp -= tempDamage;
            hudRoot.NewText("-"+tempDamage,transform,Color.red,8,20f,-1f,2.2f,bl_Guidance.LeftDown);
            StartCoroutine(ShowBobyRed());
            if (playerStatus.CurrentHp <= 0)
            {
                playerState = PlayerState.Death;
                playerAnimation.PlayAni("Death");               
                if (playerStatus.CurrentHp < 0)
                {
                    playerStatus.CurrentHp = 0;
                }
                HeadPanel.instance.UpdateShow();
                StartCoroutine(BackCity());
                return true;
            }
            HeadPanel.instance.UpdateShow();
        }
        return false;
    }
    IEnumerator BackCity()//回城效果
    {
        yield return new WaitForSeconds(2.3f);
        transform.position = bornPoint;
        playerStatus.CurrentHp = playerStatus.TotalHp;
        playerStatus.CurrentMp = playerStatus.TotalMp;
        playerState = PlayerState.ControlWalk;
        playerDir.TargetPos = transform.position;
        attackState=AttackState.Idle;
        HeadPanel.instance.UpdateShow();
    }

    IEnumerator ShowBobyRed()
    {
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        renderer.material.color = previousColor;
    }
    public void UseSkill(SkillInfo info)
    {
        switch (info.impactType)
        {
            case ImpactType.Passive:
                StartCoroutine(OnUsePassiveSkill(info));
                break;
            case ImpactType.Buff:
                StartCoroutine(OnUseBuffSkill(info));
                break;
            case ImpactType.SingleTarget:
                break;
            case ImpactType.MultiTarget:
                break;
        }
    }
    //处理增益技能
    IEnumerator OnUsePassiveSkill(SkillInfo info)
    {
        playerState = PlayerState.SkillAttack;
        playerAnimation.PlayAni(info.animationName);//技能动作
        yield return new WaitForSeconds(info.animationTime);//技能动作时间
        playerState = PlayerState.ControlWalk;
        int hp=0,mp=0;
        switch (info.impactProperty)
        {         
            case ImpactProperty.Hp:
                hp = info.impactValue;
                break;
            case ImpactProperty.Mp:
                mp = info.impactValue;
                break;     
        } 
        playerStatus.GetDrug(hp,mp);
        GameObject go=GameObject.Instantiate(Resources.Load<GameObject>("Effects/frost-fx/" + info.effectName), transform.position, Quaternion.identity);
    }
    //处理增强技能
    IEnumerator OnUseBuffSkill(SkillInfo info)
    {
        playerState = PlayerState.SkillAttack;
        playerAnimation.PlayAni(info.animationName);
        yield return new WaitForSeconds(info.animationTime);
        playerState = PlayerState.ControlWalk;
        GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Effects/frost-fx/" + info.effectName), transform.position, Quaternion.identity);
        switch (info.impactProperty)
        {
            case ImpactProperty.Attack:
                playerStatus.TotalAttack += info.impactValue;
                break;
            case ImpactProperty.Defence:
                break;
            case ImpactProperty.Speed:                
                break;
            case ImpactProperty.AttackSpeed:
                attackRate += info.impactValue;
                break;
        }
        yield return new  WaitForSeconds(info.impactTime);
        switch (info.impactProperty)
        {
            case ImpactProperty.Attack:
                playerStatus.TotalAttack -= info.impactValue;
                break;
            case ImpactProperty.Defence:
                break;
            case ImpactProperty.Speed:
                break;
            case ImpactProperty.AttackSpeed:
                attackRate -= info.impactValue;
                break;
        }
    }
    //处理选择目标前的准备工作
    public void SelectTarget(SkillInfo info,ShortCutSlot slot)
    {
        skillInfo = info;
        shortCutSlot = slot;
    }
    //处理单个目标
    IEnumerator OnUseSingleTargetSkill(SkillInfo skillInfo,RaycastHit hitInfo)
    {
        playerAnimation.PlayAni(skillInfo.animationName);
        yield return new WaitForSeconds(skillInfo.animationTime);
        playerState = PlayerState.ControlWalk;
        attackState = AttackState.Idle;
        GameObject.Instantiate(Resources.Load<GameObject>("Effects/frost-fx/" + skillInfo.effectName), new Vector3(hitInfo.point.x,hitInfo.point.y-1,hitInfo.point.z), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        hitInfo.transform.GetComponent<Enemy>().TakeDamage(playerStatus.TotalAttack,playerStatus.transform);
        CursorManager.instance.SetCursorNormal();        
    }
   
}
