using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingPanel : BasePanel {
    public static SettingPanel instance;
    private Button musicOpenBtn;
    private Button musicCloseBtn;
    private Button soundOpenBtn;
    private Button soundCloseBtn;
    private FunctionPanel functionPanel;
    private Scrollbar musicScrollbar;
    private Text musicPercentageText;
    private Scrollbar soundScrollbar;
    private Text soundPercentageText;
    private AudioSource musicAudioSource;
    public override void Start()
    {
        base.Start();
        instance = this;
        musicOpenBtn = transform.Find("music/musicOpen").GetComponent<Button>();
        musicCloseBtn = transform.Find("music/musicClose").GetComponent<Button>();
        soundOpenBtn = transform.Find("soundEffect/soundOpen").GetComponent<Button>();
        soundCloseBtn = transform.Find("soundEffect/soundClose").GetComponent<Button>();
        functionPanel = GameObject.Find("FunctionPanel").GetComponent<FunctionPanel>();

        musicScrollbar = transform.Find("music/musicScrollbar").GetComponent<Scrollbar>();
        musicPercentageText = musicScrollbar.transform.Find("musicPercentage").GetComponent<Text>();
        musicPercentageText.text = "";

        soundScrollbar = transform.Find("soundEffect/soundScrollbar").GetComponent<Scrollbar>();
        soundPercentageText = soundScrollbar.transform.Find("soundPercentage").GetComponent<Text>();
        soundPercentageText.text = "";

        musicAudioSource = GetComponent<AudioSource>();

        musicOpenBtn.onClick.AddListener(OnClickMusicOpenBtn);
        musicCloseBtn.onClick.AddListener(OnClickMusicCloseBtn);
        soundOpenBtn.onClick.AddListener(OnClickSoundOpenBtn);
        soundCloseBtn.onClick.AddListener(OnClickSoundCloseBtn);
    }

    private void OnClickSoundCloseBtn()
    {
        functionPanel.MuteUISound();
    }

    private void OnClickSoundOpenBtn()
    {
        functionPanel.OpenUISound();
    }

    private void OnClickMusicCloseBtn()
    {
        MuteMusic();
    }

    private void OnClickMusicOpenBtn()
    {
        OpenMusic();
    }
    public override void TransformState()
    {
        base.TransformState();
    }
    void OpenMusic()
    {
        GetComponent<AudioSource>().mute = false;
    }
    void MuteMusic()
    {
        GetComponent<AudioSource>().mute=true;
    }
    void Update()
    {
        musicPercentageText.text = musicScrollbar.value.ToString();
        musicAudioSource.volume = musicScrollbar.value;
        soundPercentageText.text = soundScrollbar.value.ToString();
        functionPanel.Adjust(soundScrollbar.value);
    }
}
