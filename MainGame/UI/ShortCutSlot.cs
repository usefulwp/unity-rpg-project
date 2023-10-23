using UnityEngine;
using UnityEngine.UI;
public enum ShortCutType
{ 
  Drug,
  Skill,
  None
}
public enum DrugType
{
    MP,
    HP,
    None
}
public class ShortCutSlot : MonoBehaviour {
    public ShortCutType shortCutType { get; set; }
    public DrugType drugType { get; set; }
    private Text label;
    private KeyCode keycode;
    private Image icon;
    private Image mask;
    private Text cdLabel;
    private PlayerStatus playerStatus;
    private ObjectInfo info;//药品信息
    private bool isColding;
    private float timerCD;
    private SkillInfo skillInfo;
    private PlayerAttack playerAttack;
    public int ID { get; set; }
	// Use this for initialization
	void Awake () { 
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        shortCutType = ShortCutType.None;
        drugType = DrugType.None;
        label = transform.Find("Text").GetComponent<Text>();
        icon=transform.Find("icon").GetComponent<Image>();
        mask = transform.Find("mask").GetComponent<Image>();
        cdLabel = transform.Find("cd").GetComponent<Text>();
        playerAttack = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAttack>();

        icon.gameObject.SetActive(false);
        mask.gameObject.SetActive(false);
        cdLabel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (playerAttack.playerState == PlayerState.Death)
        {
            return;
        }  
        if (Input.GetKeyDown(keycode))
        {
            switch (shortCutType)
            {
                case ShortCutType.Drug:              
                    UseDrug();               
                    break;
                case ShortCutType.Skill:
                    UseSkill();
                    break;
                case ShortCutType.None:
                    break;
              
            }
        }
        if (isColding)
        {
            timerCD += Time.deltaTime;
            mask.fillAmount = (skillInfo.cd-timerCD) / skillInfo.cd;
            cdLabel.text = (skillInfo.cd - timerCD).ToString("f1");
            if (timerCD >= skillInfo.cd)
            {
                isColding = false;
                timerCD = 0;
                mask.gameObject.SetActive(false);
                cdLabel.gameObject.SetActive(false);
            }
        }
	}
    public void SetShortCutState(int i)
    {
        keycode = KeyCode.Alpha0 + i;
        label.text = i.ToString();
        
    }
    public void Set(int id)
    {
        if (!ShortCutPanel.instance.JudgeShortCutSlotDuplication(id))
        {
            ID = id;
            icon.gameObject.SetActive(true);
            skillInfo = SkillsInfo.instance.GetSkillInfoByID(id);
            icon.sprite = Resources.Load<Sprite>("Icon/" + skillInfo.iconName);
            shortCutType = ShortCutType.Skill;
        }
        else
        {
            Debug.Log("该技能已经存在快捷栏上！！！");
        }
     
    }
    public void SetDrug(int id)
    {
        if (!ShortCutPanel.instance.JudgeShortCutSlotDuplication(id))
        {
            ID = id;
            icon.gameObject.SetActive(true);
            info = ObjectsInfo.instance.GetObjectInfoById(id);
            if (info.hp > 0)
            {
                drugType = DrugType.HP;
            }
            else if (info.mp > 0)
            {
                drugType = DrugType.MP;
            }

            icon.sprite = Resources.Load<Sprite>("Icon/" + info.iconName);
            shortCutType = ShortCutType.Drug;
        }
        else
        {
            Debug.Log("该药品已经存在快捷栏上！！！");
        }

    }
    void UseDrug()
    {
        if (playerStatus.IsUseDrug(drugType))
        {
            int num=InventoryPanel.instance.FetchGoods(ID);//物品使用后的剩余数量
            if (num > 0)
            { 
               playerStatus.GetDrug(info.hp, info.mp);
            }
            else if (num == 0)
            {
                playerStatus.GetDrug(info.hp, info.mp);
                icon.gameObject.SetActive(false);
                ID = 0;
                info = null;
                drugType = DrugType.None;
                shortCutType=ShortCutType.None;
            }
        }
        else
        {
            Debug.Log("状态已满，无法使用该物品!!!!");
        }
    }
    void UseSkill()
    {
        if (!isColding)
        {
            if (skillInfo.impactType == ImpactType.SingleTarget || skillInfo.impactType==ImpactType.MultiTarget)
            {
                if (skillInfo.expendMp<=playerStatus.CurrentMp)
                {
                    CursorManager.instance.SetCursorLockTarget();
                    playerAttack.playerState = PlayerState.SkillAttack;
                    playerAttack.SelectTarget(skillInfo,this);
                }
                else
                {
                    Debug.Log("mp不足");
                }
                return;
            }
            OnUseSkillState(skillInfo);
        }
        else
        {
            Debug.Log("技能冷却中");
        }
    }
    public bool OnUseSkillState(SkillInfo skillinfo)
    {      
            bool isSuccess = playerStatus.TakeMp(skillInfo.expendMp);
            if (isSuccess)
            {
                isColding = true;
                mask.gameObject.SetActive(true);
                cdLabel.gameObject.SetActive(true);
                mask.fillAmount = 1;
                if (skillInfo.impactType == ImpactType.Passive || skillInfo.impactType==ImpactType.Buff)
                {
                    playerAttack.UseSkill(skillinfo);
                }
                return true;
            }
            else
            {
                Debug.Log("英雄蓝量不足!!!");
                return false;
            }
    }
}
