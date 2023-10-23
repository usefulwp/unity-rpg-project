using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCutPanel : MonoBehaviour {
    public static ShortCutPanel instance;
    private GameObject shortCutSlotPrefab;
    private List<ShortCutSlot> shortCutSlotList = new List<ShortCutSlot>();
	// Use this for initialization
	void Start () {
        shortCutSlotPrefab = Resources.Load<GameObject>("Prefabs/ShortCut/shortCutSlot");
        instance = this;
        InitSlot();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void InitSlot()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject go = GameObject.Instantiate(shortCutSlotPrefab, transform);
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<ShortCutSlot>().SetShortCutState(i+1);
            shortCutSlotList.Add(go.GetComponent<ShortCutSlot>());
        }
    }
    public bool JudgeShortCutSlotDuplication(int id)//判断快捷栏上是否有相同的
    {
        foreach (ShortCutSlot slot in shortCutSlotList)
        {
            if (id == slot.ID)
                return true;
        }
        return false;
    }
   
}
