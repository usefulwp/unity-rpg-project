using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopDrugPanel : BasePanel {
    public static ShopDrugPanel instance;
    private Button closeBtn;
    private InputField inputField;
    private Button confirmBtn;
    private Transform buyContainer;
    //点击购买按钮
    private Button buy1001;
    private Button buy1002;
    private int buyID;
    private PlayerStatus playStatus;
	// Use this for initialization
    public override void Start()
    {
        base.Start();
        instance = this;
        closeBtn=transform.Find("CloseBtn").GetComponent<Button>();
        inputField=transform.Find("buyContainer/InputField").GetComponent<InputField>();
        confirmBtn=transform.Find("buyContainer/confirmBtn").GetComponent<Button>();
        buy1001=transform.Find("ShopItem1/BuyBtn").GetComponent<Button>();
        buy1002 = transform.Find("ShopItem2/BuyBtn").GetComponent<Button>();
        buyContainer = transform.Find("buyContainer");
        buyContainer.gameObject.SetActive(false);

        playStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        closeBtn.onClick.AddListener(OnClickCloseBtn);
        confirmBtn.onClick.AddListener(OnClickConfirmBtn);

        buy1001.onClick.AddListener(() => { OnClickBuyBtn(1001); });
        buy1002.onClick.AddListener(() => { OnClickBuyBtn(1002); });
    }

    private void OnClickBuyBtn(int buyID)
    {
        //代码逻辑优化部分    
        inputField.text = "";        
       
        buyContainer.gameObject.SetActive(true);
        this.buyID = buyID;
    }

 

    private void OnClickConfirmBtn()
    {
        if (inputField.text == "")
        {
            Debug.Log("输入为空，请重新输入");
            return;
        } 
        int num = int.Parse(inputField.text);
        ObjectInfo info= ObjectsInfo.instance.GetObjectInfoById(buyID);
        int totalPrice = info.priceBuy * num;
        if (playStatus.GoldNum >= totalPrice)
        {
            playStatus.GoldNum -= totalPrice;
            InventoryPanel.instance.GetID(buyID,num);
            buyContainer.gameObject.SetActive(false);
            Debug.Log("购买成功");
        }
        else
        {
            Debug.Log("你的金币不足！！！");
        }
        
    }

    private void OnClickCloseBtn()
    {
        TransformState();
    }
    public override void TransformState()
    {
        base.TransformState();
        inputField.text = "";
    }
}
