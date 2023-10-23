using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeadPanel : MonoBehaviour {
    public static HeadPanel instance;
    private Text levelLabel;
    private Image hpFill;
    private Image mpFill;
    private Image expFil;
    private Text hpText;
    private Text mpText;
    private Text expText;
    private PlayerStatus playerStatus;
    private RawImage rawImage;
	// Use this for initialization
	void Start () {
        instance = this;
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        levelLabel = transform.Find("nameBg/Text").GetComponent<Text>();
        hpFill = transform.Find("managerFrame/hpframe/hpfill").GetComponent<Image>();
        mpFill=transform.Find("managerFrame/mpframe/mpfill").GetComponent<Image>();
        expFil = transform.Find("managerFrame/expframe/expfill").GetComponent<Image>();

        hpText=transform.Find("managerFrame/hpframe/Text").GetComponent<Text>();
        mpText = transform.Find("managerFrame/mpframe/Text").GetComponent<Text>();
        expText = transform.Find("managerFrame/expframe/Text").GetComponent<Text>();
        rawImage = transform.Find("mask/RawImage").GetComponent<RawImage>();
        rawImage.texture = Resources.Load<Texture>("head" + PlayerPrefs.GetInt("CharacterIndex"));
        UpdateShow();
      
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateShow()
    {
        levelLabel.text = "Lv."+playerStatus.Level+"    "+playerStatus.HeroName;
        float hpPercentage,mpPrecentage,expPrecentage;
        hpPercentage=playerStatus.CurrentHp / playerStatus.TotalHp;
        mpPrecentage = playerStatus.CurrentMp / playerStatus.TotalMp;
        expPrecentage = playerStatus.CurrentExp / playerStatus.TotalExp;
        hpFill.fillAmount = hpPercentage;
        mpFill.fillAmount = mpPrecentage;
        expFil.fillAmount = expPrecentage;
        hpText.text = (hpPercentage * 100).ToString("f1") + "%";
        mpText.text = (mpPrecentage * 100).ToString("f1") + "%";
        expText.text = (expPrecentage * 100).ToString("f1") + "%";
        
     
    }
    public void UpdateExpValue()
    {
        expText.text=((playerStatus.CurrentExp / playerStatus.TotalExp)*100).ToString("f1")+"%";
        expFil.fillAmount = playerStatus.CurrentExp / playerStatus.TotalExp;
    }
}
