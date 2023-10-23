using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public int currentNum;
    public int maxNum;
    public float time;//刷新时间
    public float timer;
    public GameObject prefab;
    private TextAsset textAsset;
    private string[] assetArray;
  
    public int EnemyIndex;
	// Use this for initialization
	void Start () {
        textAsset = Resources.Load<TextAsset>("MainGame/TextInfo/EnemyPath");
        assetArray=textAsset.text.Split(',');
        ResourceRequest request = Resources.LoadAsync<GameObject>(assetArray[EnemyIndex]);//异步加载
        request.completed += Load;
       // prefab = Resources.Load<GameObject>(assetArray[i]);
	}

    private void Load(AsyncOperation obj)
    {
        prefab = (obj as ResourceRequest).asset as GameObject;
        
    }

	// Update is called once per frame
	void Update () {
        if (currentNum < maxNum)
        {
            timer += Time.deltaTime;
            if (timer > time)
            {
                Vector3 origin = transform.position;
                origin.x+=Random.Range(-5f,5f);
                origin.z+=Random.Range(-5f,5f);
                GameObject go=GameObject.Instantiate(prefab, origin, Quaternion.identity);
                go.GetComponent<Enemy>().enemySpawn = this;
                timer = 0;
                ++currentNum;
            }
        }
	}
}
