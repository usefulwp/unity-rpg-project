using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniMapPanel : MonoBehaviour {
    private Button plusBtn;
    private Button minusBtn;
    private Camera miniCamera;
    private RawImage rawImage;
	// Use this for initialization
	void Start () {
        plusBtn = transform.Find("plusBtn").GetComponent<Button>();
        minusBtn = transform.Find("minusBtn").GetComponent<Button>();
        miniCamera = GameObject.FindGameObjectWithTag(Tags.miniMapCamera).GetComponent<Camera>();
        plusBtn.onClick.AddListener(OnClickPlusBtn);
        minusBtn.onClick.AddListener(OnClickMinusBtn);
        rawImage = transform.Find("mask/RawImage").GetComponent<RawImage>();
        rawImage.texture = Resources.Load<Texture>("miniMap" + PlayerPrefs.GetInt("CharacterIndex"));
	}

    private void OnClickMinusBtn()
    {
        miniCamera.orthographicSize += 0.5f;

    }

    private void OnClickPlusBtn()
    {
        miniCamera.orthographicSize-=0.5f;
        if (miniCamera.orthographicSize < 0)
        { 
            miniCamera.orthographicSize = 0;
        } 
    }
	

}
