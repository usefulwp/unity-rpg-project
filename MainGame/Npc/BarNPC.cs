using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class BarNPC : NPC {
    public static BarNPC instance;
    private RectTransform taskPanel;
    private Text taskDes;
    private Text taskReward;
    private Button acceptBtn;
    private Button cancelBtn;
    private Button confirmBtn;
    private Button closeBtn;
    private bool isInTasking;
    public int sheKillCount;//水蛇数   
    public int dunKillCount;//带盾侍卫数
    public int daoKillCount;//带刀侍卫数
    private PlayerStatus playerStatus;
    [Range(6001,6003)]
    public int taskID;
	// Use this for initialization
	void Start () {
        instance = this;
        taskPanel = GameObject.Find("Canvas/TaskPanel").transform as RectTransform;
        Tweener tweener=taskPanel.DOLocalMove(Vector3.zero, 1f);
        tweener.SetAutoKill(false);
        tweener.Pause();

        taskDes = GameObject.Find("Canvas/TaskPanel/taskDes").GetComponent<Text>();
        taskReward = GameObject.Find("Canvas/TaskPanel/taskReward").GetComponent<Text>();
        acceptBtn = GameObject.Find("Canvas/TaskPanel/acceptBtn").GetComponent<Button>();
        cancelBtn = GameObject.Find("Canvas/TaskPanel/cancelBtn").GetComponent<Button>();
        confirmBtn = GameObject.Find("Canvas/TaskPanel/confirmBtn").GetComponent<Button>();
        closeBtn = GameObject.Find("Canvas/TaskPanel/closeBtn").GetComponent<Button>();

 
        acceptBtn.onClick.AddListener(OnClickAcceptBtn);
        closeBtn.onClick.AddListener(OnClickCloseBtn);
        cancelBtn.onClick.AddListener(OnClickCancelBtn);
        confirmBtn.onClick.AddListener(onClickConfirmBtn);

        playerStatus=GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
	}
    
    private void onClickConfirmBtn()
    {
        switch (taskID)
        {
            case 6001:
                if (sheKillCount >= TasksInfo.instance.GetTaskInfo(taskID).killCount)
                {
                    sheKillCount = 0;

                    //加钱
                    playerStatus.GoldNum += float.Parse(TasksInfo.instance.GetTaskInfo(taskID).reward);
                    ShowTaskDes();
                }
                else
                {
                    Debug.Log("当前任务未完成！");
                };
                break;
            case 6002:
                if (dunKillCount >= TasksInfo.instance.GetTaskInfo(taskID).killCount)
                {
                    dunKillCount = 0;

                    //加钱
                    playerStatus.GoldNum += float.Parse(TasksInfo.instance.GetTaskInfo(taskID).reward);
                    ShowTaskDes();
                }
                else
                {
                    Debug.Log("当前任务未完成！");
                }
                break;
            case 6003:
                if (daoKillCount >= TasksInfo.instance.GetTaskInfo(taskID).killCount)
                {
                    daoKillCount = 0;

                    //加钱
                    playerStatus.GoldNum += float.Parse(TasksInfo.instance.GetTaskInfo(taskID).reward);
                    ShowTaskDes();
                }
                else
                {
                    Debug.Log("当前任务未完成！");
                }
                break;
        }
    }
    void OnClickCancelBtn()
    {
       
    }
    private void OnClickCloseBtn()
    {
        HideTaskPanel();
    }
    void OnClickAcceptBtn()
    {
        isInTasking = true;
        ShowTaskProgress();
    }
    void ShowTaskDes()
    {
        taskDes.text = "任务：\n" + TasksInfo.instance.GetTaskInfo(taskID).content;
        taskReward.text = "奖励：\n" + TasksInfo.instance.GetTaskInfo(taskID).reward + "金币";
        acceptBtn.gameObject.SetActive(true);
        cancelBtn.gameObject.SetActive(true);
        confirmBtn.gameObject.SetActive(false);
    }
    void ShowTaskProgress()
    {
        switch (taskID)
        {   
            case 6001:
                taskDes.text = "任务：\n你已经杀死了" + sheKillCount + "\\" + TasksInfo.instance.GetTaskInfo(taskID).killCount + "只水蛇\n";
                break;
            case 6002:
                taskDes.text = "任务：\n你已经杀死了" + dunKillCount + "\\" + TasksInfo.instance.GetTaskInfo(taskID).killCount + "个带盾侍卫\n";
                break;
            case 6003:
                taskDes.text = "任务：\n你已经杀死了" + daoKillCount + "\\" + TasksInfo.instance.GetTaskInfo(taskID).killCount + "个带刀侍卫\n";
                break;
        } 

   
        taskReward.text = "奖励：\n" + TasksInfo.instance.GetTaskInfo(taskID).reward + "金币";
        acceptBtn.gameObject.SetActive(false);
        cancelBtn.gameObject.SetActive(false);
        confirmBtn.gameObject.SetActive(true);
    }
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isInTasking)
            {
                ShowTaskProgress();
            }
            else
            {
                ShowTaskDes();
            }
            ShowTaskPanel();
            
        }
    }
    void ShowTaskPanel()
    {
        taskPanel.DOPlayForward();
    }
    void HideTaskPanel()
    {
        taskPanel.DOPlayBackwards();
    }
 
}
