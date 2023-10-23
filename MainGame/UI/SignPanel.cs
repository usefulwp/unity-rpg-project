using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SignPanel : BasePanel {
    public static SignPanel instance;
    public const string SignNumPrefs = "SignNum";//领取次数的字符串
    public const string SignDataPrefs = "lastDay";//上次领取的时间字符串
    int signNum;//签到次数  默认是0

    DateTime today;//今日日期
    DateTime lastDay;//上次领取日期
    TimeSpan Interval;//间隔时间

    Button reviceButton;//领取按钮
    Text reviceText;//领取和时间Text

    bool isShowTime;//是否显示时间

    private void Awake()
    {
        reviceButton = transform.Find("bottomButton").GetComponent<Button>();
        reviceButton.onClick.AddListener(OnSignClick);
        reviceText = reviceButton.transform.GetChild(0).GetComponent<Text>();

    }


    private void OnEnable()
    {
        today = DateTime.Now;
        signNum = PlayerPrefs.GetInt(SignNumPrefs, 0);
        lastDay = DateTime.Parse(PlayerPrefs.GetString(SignDataPrefs, DateTime.MinValue.ToString()));
        if (IsOneDay())//今天日期是否大于领取日期  可以领取
        {
            Debug.Log("可以领取！");
            if (signNum >= 6)//重新计算签到
            {
                PlayerPrefs.DeleteKey(SignNumPrefs);
                //TODO：把奖励物品重置
            }
            //TODO：把按钮text变成领取
            reviceText.fontSize = 25;
            reviceText.text = "领取";
            reviceButton.interactable = true;

        }
        else //签到日期未到
        {
            isShowTime = true;
            reviceButton.interactable = false;
            reviceText.fontSize = 25;
        }
    }
    public override void Start()
    {
        base.Start();
        instance = this;
    }
    public override void TransformState()
    {
        base.TransformState();
    }
    private void Update()
    {
        //TimeSpan time=DateTime.Now.AddDays(1).Date - DateTime.Now; //用后一天的时间减掉当前的时间，得到剩下的时分秒
        //Debug.Log(string.Format("{0:D2}:{1:D2}:{2:D2}s",time.Hours,time.Minutes,time.Seconds));
        if (isShowTime)
        {
            Interval= lastDay.AddDays(1).Date - DateTime.Now;
            reviceText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",Interval.Hours,Interval.Minutes,Interval.Seconds);
        }
    }

    //签到领取奖励Button
    public void OnSignClick()
    {
        isShowTime = true;
        reviceButton.interactable = false;
        reviceText.fontSize = 25;
        signNum++;//领取次数
        lastDay = today;
        PlayerPrefs.SetString(SignDataPrefs, today.ToString());
        PlayerPrefs.SetInt(SignNumPrefs, signNum);

        //TODO：奖励
       
    }

    //判断是否可以签到
    private bool IsOneDay()
    {
        if (lastDay.Year == today.Year && lastDay.Month == today.Month && lastDay.Day == today.Day)
        {
            return false;
        }
        if (DateTime.Compare(lastDay, today) < 0)//DateTime.Compare(t1,t2) 返回<0  t1<t2   等于0  t1=t2   返回>0 t1>t2
        {
            return true;
        }
        return false;
    }

}
