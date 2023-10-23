using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LotteryBtn : MonoBehaviour {
    private bool hide = false;
    private Button btn;
	// Use this for initialization
	void Start () {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickBtn);
	}

    private void OnClickBtn()
    {
        LotteryPanel.instance.TransformState();
    }
	

}
