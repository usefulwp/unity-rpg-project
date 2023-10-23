using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillItem : MonoBehaviour {
    private Image icon;
    private new Text name;
    private Text des;
    private Text type;
    private Text mp;
    private int id;
    public int ID { get { return id; } set { id = value; } }
    private Image mask;
    private SkillInfo info;
	// Use this for initialization
	void Awake () {
		icon=transform.Find("icon").GetComponent<Image>();
        name = transform.Find("name").GetComponent<Text>();
        des=transform.Find("des").GetComponent<Text>();
        type=transform.Find("type").GetComponent<Text>();
        mp=transform.Find("mp").GetComponent<Text>();
        mask=transform.Find("mask").GetComponent<Image>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetInfo(int id)
    {
        info = SkillsInfo.instance.GetSkillInfoByID(id);
        this.id = info.id;
        icon.sprite=Resources.Load<Sprite>("Icon/"+info.iconName);
        name.text=info.name;
        des.text=info.description;
        string str = "";
        switch (info.impactType)
        {
            case ImpactType.Passive:
                str = "增益";
                break;
            case ImpactType.Buff:
                str = "增强";
                break;
            case ImpactType.SingleTarget:
                str = "单个目标";
                break;
            case ImpactType.MultiTarget:
                str = "群体目标";
                break;
            default:
                break;
        }
        type.text = str;
        mp.text= info.expendMp.ToString()+"Mp";
    }
    public void SetSkillState(int level)
    {
        if (level >= info.needfulLevel)
        {
            mask.gameObject.SetActive(false);
        }
    }
}
