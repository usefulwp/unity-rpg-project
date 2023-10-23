using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponItem : MonoBehaviour {
    private Image icon;
    private new Text name;
    private Text effect;
    private Text price;
    private Text applyType;
    private Button buyBtn;
    private int id;
	// Use this for initialization
	void Awake () {
		icon=transform.Find("icon").GetComponent<Image>();
        name=transform.Find("name/Text").GetComponent<Text>();
        effect=transform.Find("effect/Text").GetComponent<Text>();
        price =transform.Find("price/Text").GetComponent<Text>();
        applyType=transform.Find("applyType/Text").GetComponent<Text>();
        buyBtn=transform.Find("buyBtn").GetComponent<Button>();
        buyBtn.onClick.AddListener(OnClickBuyBtn);

	}

    private void OnClickBuyBtn()
    {
        ShopWeaponPanel.instance.BuyContainer.SetActive(true);
        ShopWeaponPanel.instance.GetBuyIDByBtn(id);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetInfo(int id)
    {
        this.id = id;
        ObjectInfo info=ObjectsInfo.instance.GetObjectInfoById(id);
        icon.sprite = Resources.Load<Sprite>("Icon/"+info.iconName);
        name.text = info.name;
        if (info.attack > 0)
        { 
            effect.text = "+攻击力"+info.attack.ToString();
        }
        else if(info.defence>0)
        {
             effect.text = "+防御力"+info.defence.ToString();
        }
        else if(info.speed>0)
        {
           effect.text = "+速度"+info.speed.ToString();
        }
        price.text = info.priceBuy.ToString();
        string str = "";
        switch (info.applyType)
        {
            case ApplyType.Magician:
                str += "法师";
                break;
            case ApplyType.SwordMan:
                str += "战士";
                break;
            case ApplyType.Common:
                str += "通用";
                break;
            default:
                break;
        }
        applyType.text = str;
    }
}
