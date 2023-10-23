using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryDesPanel : MonoBehaviour {
    private Text desLabel;
    public static InventoryDesPanel instance;
	// Use this for initialization
	void Start () {
        instance = this;
        desLabel = transform.Find("Text").GetComponent<Text>();
        transform.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   public void Hide()
    {
        this.gameObject.SetActive(false);
    }
   public void Show(int id)
    {
        transform.position = Input.mousePosition+new Vector3(120,-100,0);
        this.gameObject.SetActive(true);
        
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        string des="";
        switch (info.objectType)
        {
            case ObjectType.Drug:
                des=GetDrugInfo(info);
                break;
            case ObjectType.Equip:
                des = GetEquipInfo(info);
                break;
            case ObjectType.Material:
                break;
            default:
                break;
        }
        desLabel.text = des;
    }
    string GetDrugInfo(ObjectInfo info)
    {
        string str="";
        str += "物品ID:" + info.id + "\n";
        str += "名字:" + info.name + "\n";
        str += "+HP:" + info.hp + "\n";
        str += "+MP:" + info.mp + "\n";
        str += "购买价:" + info.priceBuy + "\n"; 
        str += "出售价:" + info.priceSell + "\n";
        return str;
    }
    string GetEquipInfo(ObjectInfo info)
    {
        string str = "";
        str += "物品ID:" + info.id + "\n";
        str += "名字:" + info.name + "\n";
        switch (info.dressType)
        { 
            case DressType.Headgear:
                str += "穿戴类型:头盔\n";
                break;
            case DressType.Armor:
                str += "穿戴类型:盔甲\n";
                break;
            case DressType.Hand:
                str += "穿戴类型:护手\n";
                break;
            case DressType.Shoes:
                str += "穿戴类型：鞋子\n";
                break;
            case DressType.Weapon:
                str += "穿戴类型:武器\n";
                break;
        }
        switch (info.applyType)
        {
            case ApplyType.Magician:
                str += "适用类型:法师\n";
                break;
            case ApplyType.SwordMan:
                str += "适用类型:战士\n";
                break;
            case ApplyType.Common:
                str += "适用类型:通用\n";
                break;
            default:
                break;
        }
        str += "+攻击力:" + info.attack + "\n";
        str += "+防御力:" + info.defence + "\n";
        str += "+移动速度:" + info.speed + "\n";
        str += "购买价:" + info.priceBuy + "\n";
        str += "出售价:" + info.priceSell + "\n";
        return str;
    }
}
