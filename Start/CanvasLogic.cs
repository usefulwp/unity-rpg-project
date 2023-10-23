using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasLogic : MonoBehaviour {
    private CanvasGroup canvasGroup;
    public float speed = 0.5f;

    private Image pressAnyKey;
    private InputField nameField;
    private InputField passwordField;
    private Button loginBtn;
    private Button registedBtn;
    private bool isShow;
    private MySqlConnection mycon;
    // Use this for initialization
    void Start() {
        string constr = "server=localhost;Database=wp;User Id=root;password=1234;Port=3306;charset=utf8mb4";
        mycon = new MySqlConnection(constr);
        mycon.Open();

        canvasGroup = transform.Find("WhiteScreen").GetComponent<CanvasGroup>();
        pressAnyKey = transform.Find("PressAnyKey").GetComponent<Image>();
        nameField = transform.Find("Login/UserName/Name").GetComponent<InputField>();
        passwordField = transform.Find("Login/UserPassWord/PassWord").GetComponent<InputField>();
        loginBtn = transform.Find("Login/LoginBtn").GetComponent<Button>();
        registedBtn = transform.Find("Login/registeredBtn").GetComponent<Button>();
        loginBtn.onClick.AddListener(OnClickLoginBtn);
        registedBtn.onClick.AddListener(OnClickRigistedBtn);

    }
    bool SelectMySQL(string newname) //判断是否有该用户
    {
        string selstr = "select *from info ";
        MySqlCommand mySqlCommand = new MySqlCommand(selstr, mycon);
        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
        DataSet ds = new DataSet();
        mySqlDataAdapter.Fill(ds);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
                if (newname.Equals(ds.Tables[0].Rows[i][0]))
                {
                    return true;
                }
        }
        return false;
    }
    private void OnClickRigistedBtn()
    {
        string newname = nameField.text;
        string newpassword = passwordField.text;
        if (SelectMySQL(newname))
        {
            Debug.LogWarning("已经存在该用户,请输入另外的名字");
            return;
        }           
        string insertStr =string.Format( "insert into info(name,password) values({0},{1})",newname,newpassword);
        MySqlCommand mySqlCommand = new MySqlCommand(insertStr, mycon);
        if (mySqlCommand.ExecuteNonQuery() > 0) 
        {
            Debug.Log("注册成功!!!");
        }
        else
        {
            Debug.LogWarning("注册失败!!!");
        }
    }

    private void OnClickLoginBtn()
    {
        string selstr = "select *from info ";
        MySqlCommand mySqlCommand = new MySqlCommand(selstr, mycon);
        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
        DataSet ds = new DataSet();
        mySqlDataAdapter.Fill(ds);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < ds.Tables[0].Columns.Count-1; j++)
            {
                if (nameField.text.Equals(ds.Tables[0].Rows[i][j]) && passwordField.text.Equals(ds.Tables[0].Rows[i][j + 1]))
                {
                    Debug.Log("登录成功，进入游戏");
                    SceneManager.LoadScene(1);
                    /*
                     * 测试用
                     *  StartCoroutine(DelayLoadScene(5));
                     */

                }
                else if(nameField.text.Equals(ds.Tables[0].Rows[i][j]) && !passwordField.text.Equals(ds.Tables[0].Rows[i][j + 1]))
                {
                    Debug.LogWarning("密码错误!!!");
                    return;
                    
                }
            }
        }
    }

    IEnumerator  DelayLoadScene(int time) {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }
 
    // Update is called once per frame
    void Update () {
        if (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha,0,speed*Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - 0) < 0.01f)
            {
                canvasGroup.alpha = 0;
            }
        }
        if (Input.anyKeyDown&&!isShow)
        {
            pressAnyKey.gameObject.SetActive(false);
           
            isShow = true;
        }
        pressAnyKey.color = Color.Lerp(Color.blue,Color.green,Mathf.PingPong(Time.time,1)/1);

      

    }
    void OnDisable()
    {
        mycon.Close();
    }

}
