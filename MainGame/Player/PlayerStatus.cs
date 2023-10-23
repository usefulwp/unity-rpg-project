using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroType
{
    Magician,
    SwordMan,
}

public class PlayerStatus : MonoBehaviour {
    private GameObject upgradeEffectPrefab;
    [SerializeField]
    private string heroName;
    public string HeroName { get { return heroName; } set { heroName = value; } }

    [SerializeField]
    private HeroType herotype;
    public HeroType heroType { get { return herotype; } set { herotype = value; } }
    [SerializeField]
    private int level;
    public int Level { get { return level; } set { level = value; } }
    [SerializeField]
    private float totalHp;
    public float TotalHp { get { return totalHp; } set { totalHp = value; } }
    [SerializeField]
    private float currentHp;
    public float CurrentHp { get { return currentHp; } set { currentHp = value; } }
    [SerializeField]
    private float totalMp;
    public float TotalMp { get { return totalMp; } set { totalMp = value; } }
    [SerializeField]
    private float currentMp;
    public float CurrentMp { get { return currentMp; } set { currentMp = value; } }
    [SerializeField]
    private float totalExp;
    public float TotalExp { get { return totalExp; } set { totalExp = value; } }
    [SerializeField]
    private float currentExp;
    public float CurrentExp { get { return currentExp; } set { currentExp = value; } }
    [SerializeField]
    private float goldNum;
    public float GoldNum { get { return goldNum; } set { goldNum = value; } }
    [SerializeField]
    private float totalAttack;
    public float TotalAttack { get { return totalAttack; } set { totalAttack = value; } }
    [SerializeField]
    private float originalAttack;
    public float OriginalAttack { get { return originalAttack; } set { originalAttack = value; } }
    [SerializeField]
    private float equipAttack;
    public float EquipAttack { get { return equipAttack; } set { equipAttack = value; } }
    [SerializeField]
    private float plusAttack;
    public float PlusAttack { get { return plusAttack; } set { plusAttack = value; } }
    [SerializeField]
    private float totalDefence;
    public float TotalDefence { get { return totalDefence; } set { totalDefence = value; } }
    [SerializeField]
    private float originalDefence;
    public float OriginalDefence { get { return originalDefence; } set { originalDefence = value; } }
    [SerializeField]
    private float equipDefence;
    public float EquipDefence { get { return equipDefence; } set { equipDefence = value; } }
    [SerializeField]
    private float plusDefence;
    public float PlusDefence { get { return plusDefence; } set { plusDefence = value; } }
    [SerializeField]
    private float totalSpeed;
    public float TotalSpeed { get { return totalSpeed; } set { totalSpeed = value; } }
    [SerializeField]
    private float originalSpeed;
    public float OriginalSpeed { get { return originalSpeed; } set { originalSpeed = value; } }
    [SerializeField]
    private float equipSpeed;
    public float EquipSpeed { get; set; }
    [SerializeField]
    private float plusSpeed;
    public float PlusSpeed { get { return plusSpeed; } set { plusSpeed = value; } }
    [SerializeField]
    private float remainPoint;
    public float RemainPoint { get { return remainPoint; } set { remainPoint = value; } }
	// Use this for initialization
	void Awake () {
       // Init();
        upgradeEffectPrefab = Resources.Load<GameObject>("Effects/Efx_LvUp");
        GetTotalAttack();
        GetTotalDefence();
        GetTotalSpeed();
        
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
    void Init()
    {
        Level = 1;
        CurrentHp = 100;
        CurrentMp = 100;
        CurrentExp = 0;
        GoldNum = 1000;
        OriginalAttack = 10;
        EquipAttack = 0;
        PlusAttack = 0;
        TotalAttack = OriginalAttack + EquipAttack + PlusAttack;
        

        OriginalDefence = 10;
        EquipDefence = 0;
        PlusDefence = 0;
        TotalDefence = OriginalDefence + EquipDefence + PlusDefence;
       

        OriginalSpeed= 10;
        EquipSpeed = 0;
        PlusSpeed = 0;
        TotalSpeed = OriginalSpeed + EquipSpeed + PlusSpeed;
       

        RemainPoint = 5;
        heroType = HeroType.Magician;
    }
   public  float GetTotalAttack()
    {
        TotalAttack = OriginalAttack + EquipAttack + PlusAttack;
        return TotalAttack;
    }
   public  float GetTotalDefence()
    {
        TotalDefence = OriginalDefence + EquipDefence + PlusDefence;
        return TotalDefence;
    }
    public float GetTotalSpeed()
   {
        TotalSpeed = OriginalSpeed + EquipSpeed + PlusSpeed;
        return TotalSpeed;
   }
    public bool AddPoint(int point = 1)
    { 
       if(RemainPoint>0)
       {
           RemainPoint -= point;
           return true;
       }
        return false;
    }

    public void GainExp(float exp)
    {
        CurrentExp += exp;
        TotalExp=Level * 80 + 100;
        while (TotalExp <= CurrentExp)
        { 
            Level++;
            GameObject.Instantiate(upgradeEffectPrefab, transform.position,Quaternion.identity);
            CurrentExp -= TotalExp;      
            TotalExp = Level * 80 + 100;  
        }
       
        HeadPanel.instance.UpdateShow();
    }
    public bool IsUseDrug(DrugType type)
    {
        switch (type)
        {
            case DrugType.MP:
                if (TotalMp - CurrentMp > 0)
                    return true;
                break;
            case DrugType.HP:
                if (TotalHp - CurrentHp > 0)
                    return true;
                break;

        }
        return false;
    }
    public void GetDrug(int hp,int mp)//使用药品
    {
        CurrentHp += hp;
        CurrentMp += mp;
        if (CurrentHp > TotalHp)
        {
            CurrentHp = TotalHp;
        }
        if (CurrentMp > TotalMp)
        {
            CurrentMp = TotalMp;
        }
        HeadPanel.instance.UpdateShow();
    }
    public bool TakeMp(float count)
    {
        if (CurrentMp >= count)
        {
            CurrentMp -= count;
            HeadPanel.instance.UpdateShow();
            return true;
        }
        return false;
    }
}
