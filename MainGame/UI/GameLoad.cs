using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad : MonoBehaviour {
    
	// Use this for initialization
	void Awake () {
       TextAsset textAsset= Resources.Load<TextAsset>("SelectCharacter/TextInfo/GameHeroPath");
       string[] heroPath=textAsset.text.Split(',');
       int index=PlayerPrefs.GetInt("CharacterIndex");
       GameObject player=GameObject.Instantiate(Resources.Load<GameObject>(heroPath[index]), transform.position, Quaternion.identity);
       player.GetComponent<PlayerStatus>().HeroName = PlayerPrefs.GetString("PlayerName");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
