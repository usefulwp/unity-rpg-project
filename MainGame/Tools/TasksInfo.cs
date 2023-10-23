using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksInfo : MonoBehaviour
{
    public static TasksInfo instance;
    private TextAsset textAsset;
    private Dictionary<int, TaskInfo> taskDic = new Dictionary<int, TaskInfo>();
    // Use this for initialization
    void Start()
    {
        instance = this;
        ParseText();
    }
    void ParseText()
    {
        textAsset = Resources.Load<TextAsset>("MainGame/TextInfo/TasksInfoText");
        string[] taskArray = textAsset.text.Split('\n');
        foreach (string item in taskArray)
        {
            TaskInfo taskInfo = new TaskInfo();
            string[] infoStr = item.Split(',');
            taskInfo.id = int.Parse(infoStr[0]);
            taskInfo.content = infoStr[1];
            taskInfo.killCount=int.Parse(infoStr[2]);
            taskInfo.reward = infoStr[3];
            taskDic.Add(taskInfo.id, taskInfo);
        }

    }
    public TaskInfo GetTaskInfo(int id)
    {
        TaskInfo taskinfo = null;
        taskDic.TryGetValue(id, out taskinfo);
        return taskinfo;
    }
}
public class TaskInfo
{
    public int id;
    public string content;
    public int killCount;
    public string reward;
}