using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopWeaponPanel : BasePanel {
    public static ShopWeaponPanel instance;
    private GameObject weaponItemPrefab;
    private Transform parent;
    private List<int> equipIDList;
    public GameObject BuyContainer { get; set; }
    private Button confirmBtn;
    private InputField inputfield;
    private PlayerStatus playerStatus;
    private int buyID;
    private Button closeBtn;
    public override void Start()
    {
        base.Start();
        instance = this;
        weaponItemPrefab=Resources.Load<GameObject>("Prefabs/Shop/weaponItem");
        parent = transform.Find("Scroll View/Viewport/Content");
        BuyContainer = transform.Find("buyContainer").gameObject;
        confirmBtn=BuyContainer.transform.Find("confirmBtn").GetComponent<Button>();
        inputfield=BuyContainer.transform.Find("InputField").GetComponent<InputField>();
        playerStatus=GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        BuyContainer.SetActive(false);
        confirmBtn.onClick.AddListener(OnClickConfirmBtn);

        closeBtn=transform.Find("closeBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(OnClickCloseBtn);
    }

    private void OnClickCloseBtn()
    {
        base.TransformState();
    }

    private void OnClickConfirmBtn()
    {
       if(inputfield.text!="")
       {
           ObjectInfo info=ObjectsInfo.instance.GetObjectInfoById(buyID);
           int num = int.Parse(inputfield.text);
           int spend=num*info.priceBuy;
           if(playerStatus.GoldNum<spend)
           {
               Debug.Log("你的金币不足!!!");
               inputfield.text = "";
               return;
           }
           else
           {
               playerStatus.GoldNum-=spend;
               InventoryPanel.instance.GetID(buyID);
               InventoryPanel.instance.SetNum(buyID,num);
               Debug.Log("购买成功!!!");
               inputfield.text = "";
               BuyContainer.SetActive(false);
           }
            
       }
       else
       {
         Debug.Log("输入不能为空!!!");
       }
    }
    public override void TransformState()
    {
        base.TransformState();
        InstantiateShop();
    }
    public void InstantiateShop()
    {
        equipIDList = ObjectsInfo.instance.GetEquipList();
        foreach (int id in equipIDList)
        { 
              GameObject go = GameObject.Instantiate(weaponItemPrefab, parent);
              go.transform.localPosition=Vector3.zero;
              go.GetComponent<WeaponItem>().SetInfo(id);
        }   
    }

    public void GetBuyIDByBtn(int id)
    {
        this.buyID = id;
    }
}
