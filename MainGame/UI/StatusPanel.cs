using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusPanel : BasePanel {
    public static StatusPanel instance;
    private Text attackDes;
    private Text defDes;
    private Text speedDes;
    private Text remainPoint;
    private Text summaryDes;
    private Button attackPlusBtn;
    private Button defPlusBtn;
    private Button speedPlusBtn;
    private PlayerStatus playerStatus;
	// Use this for initialization
    public override void Start()
    {
        base.Start();
        instance = this;
        attackDes = transform.Find("attackDes/Text").GetComponent<Text>();
        defDes = transform.Find("defDes/Text").GetComponent<Text>();
        speedDes = transform.Find("speedDes/Text").GetComponent<Text>();
        remainPoint = transform.Find("remainPoint/Text").GetComponent<Text>();
        summaryDes = transform.Find("summaryDes/Text").GetComponent<Text>();

        attackPlusBtn = transform.Find("attackDes/attackPlusBtn").GetComponent<Button>();
        defPlusBtn = transform.Find("defDes/defPlusBtn").GetComponent<Button>();
        speedPlusBtn = transform.Find("speedDes/speedPlusBtn").GetComponent<Button>();
        attackPlusBtn.onClick.AddListener(OnAttackPlusBtn);
        defPlusBtn.onClick.AddListener(OnDefPlusBtn);
        speedPlusBtn.onClick.AddListener(OnSpeedPlusBtn);
        playerStatus=GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        
    }

    private void OnSpeedPlusBtn()
    {
        if (playerStatus.AddPoint())
        {
            playerStatus.PlusSpeed++;
            UpdateShow();
        }
    }

    private void OnDefPlusBtn()
    {
        if (playerStatus.AddPoint())
        {
            playerStatus.PlusDefence++;
            UpdateShow();
        }
    }

    private void OnAttackPlusBtn()
    {
        if(playerStatus.AddPoint())
        {
            playerStatus.PlusAttack++;
            UpdateShow();
        }
    }
	
	
    public override void TransformState()
    {
        base.TransformState();
        UpdateShow();
    }
    void UpdateShow()
    {
        attackDes.text = playerStatus.OriginalAttack + " + " + playerStatus.EquipAttack + " + " + playerStatus.PlusAttack;
        defDes.text = playerStatus.OriginalDefence + " + " + playerStatus.EquipDefence + " + " + playerStatus.PlusDefence;
        speedDes.text = playerStatus.OriginalSpeed + " + " + playerStatus.EquipSpeed + " + " + playerStatus.PlusSpeed;
        remainPoint.text = playerStatus.RemainPoint.ToString();
        summaryDes.text = "攻击力:" + playerStatus.GetTotalAttack() + ",防御力:" + playerStatus.GetTotalDefence() + ",移动速度:" + playerStatus.GetTotalSpeed();
        if (playerStatus.RemainPoint > 0)
        {
            attackPlusBtn.gameObject.SetActive(true);
            defPlusBtn.gameObject.SetActive(true);
            speedPlusBtn.gameObject.SetActive(true);
        }
        else
        {
            attackPlusBtn.gameObject.SetActive(false);
            defPlusBtn.gameObject.SetActive(false);
            speedPlusBtn.gameObject.SetActive(false);
        }
       
    }
}
