using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipPanel : BasePanel {
    public static EquipPanel instance;
    private Transform headgear;
    private Transform armor;
    private Transform hand;
    private Transform weapon;
    private Transform shoes;
    private GameObject equipItemPrefab;
    private PlayerStatus playerStatus;
    private List<EquipItem> equipItemList=new List<EquipItem>();
    public override void Start()
    {
        base.Start();
        instance = this;
        headgear = transform.Find("headgear");
        armor = transform.Find("armor");
        hand = transform.Find("hand");
        weapon = transform.Find("weapon");
        shoes = transform.Find("shoes");
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        equipItemPrefab = Resources.Load<GameObject>("Prefabs/Equip/equipItem");
        
    }
    public override void TransformState()
    {
        base.TransformState();
    }
    //穿戴装备
    public bool Wear(int id)
    {
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        if(info.objectType!=ObjectType.Equip)
        {
            return false;
        }
        if (playerStatus.heroType == HeroType.Magician)
        {
            if (info.applyType == ApplyType.SwordMan)
            {
                return false;
            }
        }
        if (playerStatus.heroType == HeroType.SwordMan)
        {
            if (info.applyType == ApplyType.Magician)
            {
                return false;
            }
        }
        Transform parent=null;
        switch (info.dressType)
        {
            case DressType.Headgear:
                parent = headgear;   
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.Hand:
                parent = hand;
                break;
            case DressType.Weapon:
                parent = weapon;
                break;
            case DressType.Shoes:
                parent = shoes;
                break;
            case DressType.Accessory:
                break;
            default:
                break;
        }
        EquipItem equipItem=parent.GetComponentInChildren<EquipItem>();
        if (equipItem != null)//该装备栏有装备
        {
            if (equipItem.ID == id)//穿的相同的装备的处理
            {
                Debug.Log("该部位已穿戴相同的装备!");
                return false;
            }
            else//穿的不同装备进行替换
            {
                InventoryPanel.instance.GetID(equipItem.ID);
                equipItem.SetEquipInfo(info);
                Compute();
                return true;
            }

        }
        else
        {
            GameObject equip = GameObject.Instantiate(equipItemPrefab, parent);
            equip.transform.localPosition = Vector3.zero;
            equip.GetComponent<EquipItem>().SetEquipInfo(info);
            equipItemList.Add(equip.GetComponent<EquipItem>());
            Compute();
            return true;
        }
      
    }
    public void TakeOffEquip(int id,GameObject go)
    {
        InventoryPanel.instance.GetID(id);
        Destroy(go);
        equipItemList.Remove(go.GetComponent<EquipItem>());
        Compute();
    }

    //计算装备穿戴后加成的属性
     void Compute()
    {
        playerStatus.EquipAttack = 0;
        playerStatus.EquipDefence = 0;
        playerStatus.EquipSpeed = 0;
        foreach (EquipItem item in equipItemList)
        {
            ObjectInfo info=ObjectsInfo.instance.GetObjectInfoById(item.ID);
            playerStatus.EquipAttack += info.attack;
            playerStatus.EquipDefence += info.defence;
            playerStatus.EquipSpeed += info.speed;
        }
    }
}
