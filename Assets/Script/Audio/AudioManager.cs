using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton
    private bool isMuted;
    public AudioSource bgmSource;  // Nhạc nền
    //public AudioSource sfxSource;  // Hiệu ứng âm thanh

    public AudioClip backgroundMusic;  // Nhạc nền mặc định
    public AudioClip buttonClickSound; // Âm thanh khi bấm nút

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayBGM(backgroundMusic); // Chạy nhạc nền khi game bắt đầu
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return; // Tránh phát lại cùng một bài
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    //public void PlaySFX(AudioClip clip)
    //{
    //    sfxSource.PlayOneShot(clip);
    //}

    public void SetVolume(float bgmVolume, float sfxVolume)
    {
        bgmSource.volume = bgmVolume;
       // sfxSource.volume = sfxVolume;
    }

    public void Mute(bool isMuted)
    {
        bgmSource.mute = isMuted;
        //sfxSource.mute = isMuted;
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        ApplyMute();
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0); // Lưu trạng thái
        PlayerPrefs.Save();
    }

    private void ApplyMute()
    {
        bgmSource.mute = isMuted;
       
    }

    public bool IsMuted()
    {
        return isMuted;
    }
}
