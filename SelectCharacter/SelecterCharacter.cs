using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelecterCharacter : MonoBehaviour {
    private Button preBtn;
    private Button nextBtn;
    private Button confirmBtn;
    private string[] prefabsPathes;
    private GameObject[] prefabsArray;
    private InputField inputField;
    private int index;
    private Image fillImage;
    private Text fillText;
    private float fillSpeed=1;
    private float target;
    private AsyncOperation asyncOperation;
	// Use this for initialization
	void Start () {
        preBtn = GameObject.Find("Canvas/preBtn").GetComponent<Button>();
        nextBtn = GameObject.Find("Canvas/nextBtn").GetComponent<Button>();
        confirmBtn = GameObject.Find("Canvas/confirmBtn").GetComponent<Button>();
        inputField=GameObject.Find("Canvas/EnterNameImage/InputField").GetComponent<InputField>();
        TextAsset textAsset = Resources.Load<TextAsset>("SelectCharacter/TextInfo/path");
        fillImage = transform.Find("bg/fill").GetComponent<Image>();
        fillText = transform.Find("bg/Text").GetComponent<Text>();
        fillText.text = "";
        prefabsPathes=textAsset.text.Split(',');
        prefabsArray=new GameObject[prefabsPathes.Length];
        for(int i=0;i<prefabsArray.Length;i++)
        {
            prefabsArray[i] = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(prefabsPathes[i]));
        }
        showCharacter(index);
        preBtn.onClick.AddListener(
            () =>
            {
                index--;
                if (index < 0)
                {
                    index = prefabsPathes.Length - 1;
                }
                showCharacter(index);
            }
            );
        nextBtn.onClick.AddListener(
            () => 
            {
                index++;
                if (index > prefabsPathes.Length-1)
                {
                    index = 0;
                }
                showCharacter(index);
            }
            );
        confirmBtn.onClick.AddListener(
            () =>
            {
                if (inputField.text != "")
                {
                    PlayerPrefs.SetInt("CharacterIndex", index);
                    PlayerPrefs.SetString("PlayerName", inputField.text);
                    
                    StartCoroutine(StartAsync());
                }
                else
                {
                    Debug.Log("你的输入为空！！！！");
                }
            }
            );
	}
	
	// Update is called once per frame
	void Update () {
        if (asyncOperation != null)
        {
            target = asyncOperation.progress;
            if (asyncOperation.progress >= 0.9f)
            {
                target = 1.0f;
            }
            if (target != fillImage.fillAmount)
            {
                fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, target, Time.deltaTime * fillSpeed);
                if (Mathf.Abs(fillImage.fillAmount - target) < 0.01f)
                {
                    fillImage.fillAmount = target;
                }
            }
            fillText.text = (fillImage.fillAmount * 100).ToString("f1")+"%";
            if (fillImage.fillAmount == 1)
            {
                asyncOperation.allowSceneActivation = true;
            }
        }

    }
    void showCharacter(int index)
    {
        prefabsArray[index].SetActive(true);
        for (int i = 0; i < prefabsArray.Length;i++)
        {
            if (i != index)
            {
                prefabsArray[i].SetActive(false);
            }
        }
    }
    IEnumerator StartAsync()
    {
        asyncOperation=SceneManager.LoadSceneAsync(2);
        asyncOperation.allowSceneActivation = false;
        yield return asyncOperation;
    }
}
