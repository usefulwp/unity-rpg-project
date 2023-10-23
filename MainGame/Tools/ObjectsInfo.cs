using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInfo : MonoBehaviour {
    public static ObjectsInfo instance;
    private Dictionary<int, ObjectInfo> objectsInfoDic = new Dictionary<int, ObjectInfo>();
    private TextAsset textAsset;
	// Use this for initialization
	void Start () {
        instance = this;
        ParseTextInfo();
	}
    void ParseTextInfo()
    { 
        textAsset = Resources.Load<TextAsset>("MainGame/TextInfo/ObjectsInfoText");
        string[] strArray=textAsset.text.Split('\n');
        foreach (string str in strArray)
        {
           string[] proArray= str.Split(',');
           ObjectInfo info = new ObjectInfo();
           info.id=int.Parse(proArray[0]);
           info.name = proArray[1];
           info.iconName = proArray[2];
           info.objectType = (ObjectType)System.Enum.Parse(typeof(ObjectType),proArray[3]);
           if (info.objectType == ObjectType.Drug) 
           {
               info.hp = int.Parse(proArray[4]);
               info.mp = int.Parse(proArray[5]);
               info.priceSell = int.Parse(proArray[6]);
               info.priceBuy = int.Parse(proArray[7]);
           }
           else if (info.objectType==ObjectType.Equip)
           {
               info.attack = int.Parse(proArray[4]);
               info.defence = int.Parse(proArray[5]);
               info.speed=int.Parse(proArray[6]);
               info.dressType=(DressType)System.Enum.Parse(typeof(DressType),proArray[7]);
               info.applyType=(ApplyType)System.Enum.Parse(typeof(ApplyType),proArray[8]);
               info.priceSell = int.Parse(proArray[9]);
               info.priceBuy = int.Parse(proArray[10]);
           }
           objectsInfoDic.Add(info.id,info);
        }
    }
    public ObjectInfo GetObjectInfoById(int id)
    { 
        ObjectInfo temp=null;
        objectsInfoDic.TryGetValue(id, out temp);
        return temp;
    }
    public List<int> GetEquipList()
    {
        List<int> temp=new List<int>();
        foreach (ObjectInfo info in objectsInfoDic.Values)
        {
            if (info.objectType == ObjectType.Equip)
                temp.Add(info.id);
        }
        return temp;
    }
}
public enum ObjectType
{ 
    Drug,
    Equip,
    Material
}
public enum DressType
{ 
   Headgear,
   Armor,
   Hand,
   Weapon,
   Shoes,
   Accessory
}
public enum ApplyType
{
   Magician,
   SwordMan,
   Common
}
public class ObjectInfo
{
    public int id;
    public string name;
    public string iconName;
    public ObjectType objectType;
    public int hp;
    public int mp;
    public int attack;
    public int defence;
    public int speed;
    public DressType dressType;
    public ApplyType applyType;
    public int priceSell;
    public int priceBuy;
}