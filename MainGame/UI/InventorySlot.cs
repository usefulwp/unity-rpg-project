using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour {
    public int id;
    public void PlusNumber(int count=1)
    {
        this.GetComponentInChildren<InventoryItem>().SetNum(count);
    }
    //设置物品信息
    public void SetItemInfo(int id,int num)
    {
        this.id = id;
        this.GetComponentInChildren<InventoryItem>().SetInfo(id,num);
    }
    //物品数量为0时清空数据
    public void ClearInfo()
    {
        this.id = 0;
    }
    public int MinusNum(int num=1)
    {
        InventoryItem item = this.GetComponentInChildren<InventoryItem>();
        if (item != null)
        {
            return item.JudgeDrugNum(num);
        }
        else
        {
            Debug.LogError("背包中没有此物品！！！");
            return -1;
        }

    }
}
