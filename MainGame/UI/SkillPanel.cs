using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : BasePanel {
    public static SkillPanel instance;
    private Transform content;
    private GameObject skillItemPrefab;
    private List<int> IDList = new List<int>();
    private PlayerStatus playerStatus;
    public override void Start()
    {
        base.Start();
        instance = this;
        content = transform.Find("Scroll View/Viewport/Content");
        skillItemPrefab = Resources.Load<GameObject>("Prefabs/Skill/skillItem");
        playerStatus=GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        IDList = SkillsInfo.instance.GetSkillList(playerStatus.heroType);
        Init();    
    }
    public override void TransformState()
    {
        base.TransformState();
        UpdateShow();
    }
    public void Init()
    {
        foreach (int id in IDList)
        {
            GameObject go= GameObject.Instantiate(skillItemPrefab,content);
            go.transform.localPosition = Vector3.zero;
            SkillItem skillItem=go.GetComponentInChildren<SkillItem>();
            skillItem.SetInfo(id);
        }
       
    }
    public void UpdateShow()
    {
        SkillItem[] array=this.GetComponentsInChildren<SkillItem>();
        foreach (SkillItem item in array)
        {
            item.SetSkillState(playerStatus.Level);
        }
     
    }
}
