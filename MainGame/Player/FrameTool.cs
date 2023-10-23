using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FrameTool : MonoBehaviour {
    private float lastUpdateShowTime;//上一次更新帧率的时间
    private float updateTime = 0.05f;//更新显示帧率的时间间隔
    private int frameCount;//帧数
    private float frameDeltaTime;//帧与帧之间的间隔
    private float FPS;
    private Text fpsText;
    private Text frameDeltaTimeText;
	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 45;
        lastUpdateShowTime = Time.realtimeSinceStartup;
        fpsText = transform.Find("fps").GetComponent<Text>();
        frameDeltaTimeText=transform.Find("frameDeltaTime").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        frameCount++;
        if (Time.realtimeSinceStartup - lastUpdateShowTime>=updateTime)
        { 
            FPS=frameCount/(Time.realtimeSinceStartup-lastUpdateShowTime);
            frameDeltaTime=(Time.realtimeSinceStartup-lastUpdateShowTime)/frameCount;
            fpsText.text = FPS.ToString("f3");
            frameDeltaTimeText.text=frameDeltaTime.ToString("f3");
            frameCount = 0;
            lastUpdateShowTime = Time.realtimeSinceStartup;
        }        
	}
   
}
