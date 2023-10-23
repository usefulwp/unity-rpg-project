using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInfo : MonoBehaviour
{
    public static SkillsInfo instance;
    private TextAsset textAsset;
    private Dictionary<int, SkillInfo> skillInfoDic = new Dictionary<int, SkillInfo>();
    void Start()
    {
        ParseInfo();
        instance = this;
    }
    void ParseInfo()
    {
        textAsset = Resources.Load<TextAsset>("MainGame/TextInfo/SkillsInfoText");
        string[] strArray = textAsset.text.Split('\n');
        foreach (string str in strArray)
        {
           string[] asset= str.Split(',');
           SkillInfo info=new SkillInfo();
           info.id=int.Parse(asset[0]);
           info.name = asset[1];
           info.iconName=asset[2];
           info.description=asset[3];
           info.impactType = (ImpactType)System.Enum.Parse(typeof(ImpactType),asset[4]);
           info.impactProperty = (ImpactProperty)System.Enum.Parse(typeof(ImpactProperty),asset[5]);
           info.impactValue = int.Parse(asset[6]);
           info.impactTime = int.Parse(asset[7]);
           info.expendMp = int.Parse(asset[8]);
           info.cd = float.Parse(asset[9]);
           info.impactHeroType = (HeroType)System.Enum.Parse(typeof(HeroType),asset[10]);
           info.needfulLevel = int.Parse(asset[11]);
           info.releaseType = (ReleaseType)System.Enum.Parse(typeof(ReleaseType),asset[12]);
           info.distance = int.Parse(asset[13]);
           info.effectName=asset[14];
           info.animationName=asset[15];
           info.animationTime=float.Parse(asset[16]);
           skillInfoDic.Add(info.id, info);
        }
    }
    public SkillInfo GetSkillInfoByID(int id)
    { 
        SkillInfo info=null;
        skillInfoDic.TryGetValue(id, out info);
        return info;
    }
    public List<int> GetSkillList(HeroType heroType)
    {
        List<int> temp=new List<int>();
        foreach (SkillInfo info in skillInfoDic.Values)
	   {
           if (info.impactHeroType == heroType)
           {
               temp.Add(info.id);
           }
	   }
       return temp;
    }
}

public enum  ImpactType
{
    Passive,//增益
    Buff,//增强
    SingleTarget,//单个
    MultiTarget//群体
}
public enum ImpactProperty
{ 
     Attack,
     Defence,
     Speed,
     AttackSpeed,
     Hp,
     Mp
}
public enum ReleaseType
{
   Self,
   Enemy,
}
public class SkillInfo
{
    public int id;
    public string name;
    public string iconName;
    public string description;
    public ImpactType impactType;
    public ImpactProperty impactProperty;
    public int impactValue;//作用值
    public int impactTime;
    public int expendMp;
    public float cd;
    public HeroType impactHeroType;
    public int needfulLevel;
    public ReleaseType releaseType;
    public int distance;
    public string effectName;
    public string animationName;
    public float animationTime;

}