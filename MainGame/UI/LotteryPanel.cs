using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LotteryPanel : BasePanel {
    public static LotteryPanel instance;
    private GameObject prefab;
    private Transform lotteryManager;
    private Button startBtn;
    private Transform HaloImgTransform;
    private Transform[] rewardTransArry;
   
    // 默认展示状态
    private bool isInitState;
    // 抽奖结束 -- 结束状态，光环不转
    private bool drawEnd;
    // 中奖
    private bool drawWinning;
    
    // 展示状态时间 --> 控制光环转动速度
    private float rewardTime = 0.8f;
    private float rewardTiming = 0;

    // 当前光环所在奖励的索引
    private int haloIndex = 0;
    // 本次中奖ID
    private int rewardIndex = 0;
    
    // 点了抽奖按钮正在抽奖
    private bool isOnClickPlaying;
   
    public override void Start()
    {
        base.Start();
        instance = this;
        prefab=Resources.Load<GameObject>("Prefabs/Lottery/slot");
        lotteryManager=transform.Find("lotteryManager");
        startBtn = transform.Find("startBtn").GetComponent<Button>();
        HaloImgTransform = transform.Find("halo");
        startBtn.onClick.AddListener(OnClickDrawFun);
        rewardTransArry = new Transform[lotteryManager.childCount];
        for (int i = 0; i < lotteryManager.childCount; i++)
        {
            rewardTransArry[i] = lotteryManager.GetChild(i).Find("Image");
        }

        //默认展示时间
        rewardTime = 0.6f;
        rewardTiming = 0;

        drawEnd = false;
        drawWinning = false;
        isOnClickPlaying = false;

    }

    private void OnClickDrawFun()
    {
        if (!isOnClickPlaying)
        {
            // 随机抽中ID
            rewardIndex =  Random.Range(0, rewardTransArry.Length);
            Debug.Log("开始抽奖，本次抽奖随机到的ID是：" + rewardIndex);
            //将物品放入背包
            InventoryPanel.instance.GetID(2001 + rewardIndex);
            isOnClickPlaying = true;
            drawEnd = false;
            drawWinning = false;
            StartCoroutine(StartDrawAni());
        }

    }
    public override void TransformState()
    {
        base.TransformState();
        
    }
    void Update()
    {

        if (drawEnd) return;

        // 抽奖展示
        rewardTiming += Time.deltaTime;
        if (rewardTiming >= rewardTime)
        {
            rewardTiming = 0;
            haloIndex++;
            if (haloIndex >= rewardTransArry.Length)
            {
                haloIndex = 0;
            }
            SetHaloPos(haloIndex);
        }
    }
    // 设置光环显示位置
    void SetHaloPos(int index)
    {
        HaloImgTransform.position = rewardTransArry[index].position;

        // 中奖 && 此ID == 中奖ID
        if (drawWinning && index == rewardIndex)
        {
            isOnClickPlaying = false;
            drawEnd = true;
            //todo...展示中奖物品，维护数据 --> 注意: index是索引
            Debug.Log("恭喜您中奖，中奖物品索引是：" + index + "号");
        }
    }
    
    IEnumerator StartDrawAni()
    {
        rewardTime = 0.8f;
        // 加速
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.1f);
            rewardTime -= 0.1f;
        }

        yield return new WaitForSeconds(2f);
        // 减速
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            rewardTime += 0.1f;
        }
        
        yield return new WaitForSeconds(1f);
        drawWinning = true;
    }

}
