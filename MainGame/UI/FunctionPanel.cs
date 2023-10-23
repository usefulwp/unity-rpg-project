using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FunctionPanel : MonoBehaviour {
    private Button statusBtn;
    private Button bagBtn;
    private Button equipBtn;
    private Button settingBtn;
    private Button skillBtn;

	// Use this for initialization
	void Start () {

        statusBtn = transform.Find("statusBtn").GetComponent<Button>();
        bagBtn = transform.Find("bagBtn").GetComponent<Button>();
        equipBtn = transform.Find("equipBtn").GetComponent<Button>();
        settingBtn=transform.Find("settingBtn").GetComponent<Button>();
        skillBtn = transform.Find("skillBtn").GetComponent<Button>();

        statusBtn.onClick.AddListener(OnClickStatusBtn);
        bagBtn.onClick.AddListener(OnClickBagBtn);
        equipBtn.onClick.AddListener(OnClickEquipBtn);
        settingBtn.onClick.AddListener(OnClickSettingBtn);
        skillBtn.onClick.AddListener(OnClickSkillBtn);

        statusBtn.gameObject.AddComponent<AudioSource>().clip=Resources.Load<AudioClip>("Sound/click");//一键添加音频组件,设置clip点击音效
        bagBtn.gameObject.AddComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/click");
        equipBtn.gameObject.AddComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/click");
        settingBtn.gameObject.AddComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/click");
        skillBtn.gameObject.AddComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/click");
	}

    private void OnClickSkillBtn()
    {

        skillBtn.gameObject.GetComponent<AudioSource>().Play();
        SkillPanel.instance.TransformState();
    }

    private void OnClickSettingBtn()
    {
        settingBtn.gameObject.GetComponent<AudioSource>().Play();
        SettingPanel.instance.TransformState();
    }

    private void OnClickEquipBtn()
    {
        equipBtn.gameObject.GetComponent<AudioSource>().Play();
        EquipPanel.instance.TransformState();
    }

    private void OnClickBagBtn()
    {
        bagBtn.gameObject.GetComponent<AudioSource>().Play();
        InventoryPanel.instance.TransformState();
    }

    private void OnClickStatusBtn()
    {
        statusBtn.gameObject.GetComponent<AudioSource>().Play();
        StatusPanel.instance.TransformState();
    }
    public void MuteUISound()
    {
        statusBtn.gameObject.GetComponent<AudioSource>().mute = true;
        bagBtn.gameObject.GetComponent<AudioSource>().mute = true;
        equipBtn.gameObject.GetComponent<AudioSource>().mute = true;
        settingBtn.gameObject.GetComponent<AudioSource>().mute = true;
        skillBtn.gameObject.GetComponent<AudioSource>().mute = true;
    }
    public void OpenUISound()
    {
        statusBtn.gameObject.GetComponent<AudioSource>().mute = false;
        bagBtn.gameObject.GetComponent<AudioSource>().mute = false;
        equipBtn.gameObject.GetComponent<AudioSource>().mute = false;
        settingBtn.gameObject.GetComponent<AudioSource>().mute = false;
        skillBtn.gameObject.GetComponent<AudioSource>().mute = false;
    }
    public void Adjust(float value)
    {
        statusBtn.gameObject.GetComponent<AudioSource>().volume = value;
        bagBtn.gameObject.GetComponent<AudioSource>().volume = value;
        equipBtn.gameObject.GetComponent<AudioSource>().volume = value;
        settingBtn.gameObject.GetComponent<AudioSource>().volume = value;
        skillBtn.gameObject.GetComponent<AudioSource>().volume = value;
    }
}
