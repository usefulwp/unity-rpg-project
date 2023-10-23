using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalePanel : BasePanel
{
    public static SalePanel instance;
    private InventoryPanel inventoryPanel;
    private PlayerStatus playerStatus;
    private GameObject itemPrefab;
    private Text coinText;
    private Button sellBtn;
    private Text inputFieldText;
    private int saleID;
    public override void Start()
    {
        base.Start();
        instance = this;
        itemPrefab = Resources.Load<GameObject>("Prefabs/Inventory/Item");
        inventoryPanel = GameObject.Find("Canvas/InventoryPanel").GetComponent<InventoryPanel>();
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        coinText = transform.Find("Coin/Text").GetComponent<Text>();
        sellBtn = transform.Find("sellBtn").GetComponent<Button>();
        inputFieldText = transform.Find("InputField/Text").GetComponent<Text>();

        sellBtn.onClick.AddListener(OnClickSellBtn);
    }

    private void OnClickSellBtn()
    {
        if (inputFieldText.text != "")
        { 
           saleID=int.Parse(inputFieldText.text);
        }
        Debug.Log("输入的物品ID为"+saleID);
        inventoryPanel.FetchGoods(saleID);
        Sale();
        Debug.Log("物品已售出!!!");
        
    }
    public override void TransformState()
    {
        base.TransformState();
    }
    public void Sale()//加钱
    {
       playerStatus.GoldNum+=ObjectsInfo.instance.GetObjectInfoById(saleID).priceSell;
    }
    void Update()
    {
        coinText.text = playerStatus.GoldNum.ToString();

    }
   public void Init(int id,int idNum,bool IsInstantiate)//初始化  idNum 物品种类数
    {
        GameObject go = null;
        if (!IsInstantiate) 
       {
          go = GameObject.Instantiate(itemPrefab, transform.GetChild(0).GetChild(idNum));  
          go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icon/" + ObjectsInfo.instance.GetObjectInfoById(id).iconName);
          go.transform.localPosition = Vector3.zero;
          go.GetComponent<InventoryItem>().SetInfo(id,1);
       }
        
    }

}

