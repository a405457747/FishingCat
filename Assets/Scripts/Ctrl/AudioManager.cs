using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public string ResourceDir = "";

    AudioSource m_bgSound;
    AudioSource m_effectSound;
    private Ctrl ctrl;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        ctrl = GetComponent<Ctrl>();
        m_bgSound = this.gameObject.AddComponent<AudioSource>();
        m_bgSound.playOnAwake = false;
        m_bgSound.loop = true;
        m_effectSound = this.gameObject.AddComponent<AudioSource>();
    }

    //音乐大小

    public float BgVolume
    {
        get { return m_bgSound.volume; }
        set { m_bgSound.volume = value; }
    }

    //音效大小

    public float EffectVolume
    {
        get { return m_effectSound.volume; }
        set { m_effectSound.volume = value; }
    }

    //播放音乐

    public void PlayBg(string audioName)
    {
        //当前正在播放的音乐文件
        string oldName;
        if (m_bgSound.clip == null)
            oldName = "";
        else
            oldName = m_bgSound.clip.name;

        if (oldName != audioName)
        {
            //音乐文件的路径
            string path;
            if (string.IsNullOrEmpty(ResourceDir))
            {
                path = "";
            }
            else
            {
                path = ResourceDir + "/" + audioName;
            }
            //加载音乐
            AudioClip clip = Resources.Load<AudioClip>(path);
            //播放
            if (clip != null)
            {
                m_bgSound.clip = clip;
                m_bgSound.Play();
            }
        }
    }

    //停止音乐

    public void StopBg()
    {
        m_bgSound.Stop();
        m_bgSound.clip = null;
    }

    //播放特效

    public void PlayEffect(string audioName)
    {
        if (ctrl.model.mysaveData.isMute == true) return;

        //路径
        string path;
        if (string.IsNullOrEmpty(ResourceDir))
        {
            path = "";
        }
        else
        {
            path = ResourceDir + "/" + audioName;
        }
        //音频
        AudioClip clip = Resources.Load<AudioClip>(path);

        m_effectSound.PlayOneShot(clip);
    }
}
