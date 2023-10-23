using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryPanel : BasePanel{
    public static InventoryPanel instance;
    private List<InventorySlot> slotList=new List<InventorySlot>();
    public List<InventorySlot> SlotList { get { return slotList; } }
    private GameObject itemPrefab;
    private Text goldNum;
    private PlayerStatus playerStatus;
    private int buyID;
	// Use this for initialization
    public override void Start()
    {
        base.Start();
        instance = this;
        itemPrefab = Resources.Load<GameObject>("Prefabs/Inventory/Item");
        goldNum=transform.Find("Coin/Text").GetComponent<Text>();
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        for(int i=0;i<17;i++)
        {
            slotList.Add(transform.Find("SlotList/Slot" + i).GetComponent<InventorySlot>());
        }
       
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetID(Random.Range(1001, 1003));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetID(Random.Range(2001, 2027));
        }
    }
    public override void TransformState()
    {
        base.TransformState();
        goldNum.text = playerStatus.GoldNum.ToString();
    }
    //将物品放入到背包
    public void GetID(int id,int count=1)
    {
        int IDKindNum = 0;//物品种类数
        bool isInstance=false;
        foreach (InventorySlot slot in slotList)
        {
            if (slot.id != 0 && slot.id != id)
            { 
                IDKindNum++;
            }
            if (slot.id == id)
            {
                isInstance = true;
            }
        }
        SalePanel.instance.Init(id, IDKindNum, isInstance);//物品同步到商店
        buyID=id;
        InventorySlot tempSlot = null ;  
        foreach (InventorySlot slot in slotList)
        {
            if (slot.id == id)
            {
                tempSlot = slot;
                break;
            }
        }
        if (tempSlot != null)
        {
            tempSlot.PlusNumber(count);
        }
        else
        { 
           foreach(InventorySlot slot in slotList)
           {
               if (slot.id == 0)
               {
                   tempSlot = slot;
                   break;
               }
           }
            if(tempSlot!=null)
            {
                GameObject go=GameObject.Instantiate(itemPrefab);
                go.transform.SetParent(tempSlot.transform);
                go.transform.localPosition = Vector3.zero;
                tempSlot.SetItemInfo(id,count);
            }
            else 
            {
                Debug.Log("背包开始扩容!!!");//17
                            
                    for (int i = 17; i < 28; i++)//扩充10个格子
                    {
                        GameObject slot = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Slot"), transform.Find("SlotList"));
                        slotList.Add(slot.GetComponent<InventorySlot>());
                    }
                    Debug.Log("背包扩容成功！！！");
                
            }
        }
    }
    //购买物品时已经放入物品，这时要同步修改购买的物品数量
    public void SetNum(int buyID,int num)
    {
        InventoryItem[] inventoryItemArray = GetComponentsInChildren<InventoryItem>();
        foreach (InventoryItem item in inventoryItemArray)
        { 
          if(item.GetID()==buyID)
            {
                item.SetNum(num - 1);
            }
        }
    }

    public int FetchGoods(int id,int num=1)
    {
        InventorySlot tempSlot = null;
        foreach (InventorySlot slot in slotList)
        {
            if (slot.id == id)
            { 
               tempSlot=slot;
               break;
            }
        }
        if (tempSlot != null)
        {
            return tempSlot.GetComponent<InventorySlot>().MinusNum(num);
        }
        else
        {
            Debug.LogError("背包中不存在此物品！！！");
            return -1;
        }
    }
}
