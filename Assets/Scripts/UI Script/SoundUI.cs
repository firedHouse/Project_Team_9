using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    [SerializeField] private Slider MainSound;
    [SerializeField] private Slider EffectSound;
    [SerializeField] private Slider MusicSound;
    
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManager.Instance; // SoundManager 싱글톤 인스턴스 가져오기

        MainSound.value = PlayerPrefs.GetFloat("MainVolume", 1.0f); // 저장된 볼륨들 불러오기, 기본값 1.0f
        EffectSound.value = PlayerPrefs.GetFloat("EffectVolume", 1.0f);
        MusicSound.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);


        MainSound.onValueChanged.AddListener(OnMainVolumeChanged); // 값 변경 이벤트 리스너 등록
        EffectSound.onValueChanged.AddListener(OnEffectVolumeChanged);
        MusicSound.onValueChanged.AddListener(OnMusicVolumeChanged);

    }

    private void Start()
    {
        ApplyVolumes();
    }


    private void OnDisable()
    {
        if (MainSound != null) // 메모리 누수 방지
            MainSound.onValueChanged.RemoveListener(OnMainVolumeChanged);
        if (EffectSound != null)
            EffectSound.onValueChanged.RemoveListener(OnEffectVolumeChanged);
        if (MusicSound != null)
            MusicSound.onValueChanged.RemoveListener(OnMusicVolumeChanged);
    }

    //볼륨 변경 처리
    private void OnMainVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MainVolume", value); // 변경된 값 저장
        PlayerPrefs.Save();
    }
    private void OnEffectVolumeChanged(float value)
    {
        if (soundManager != null)
        {
            soundManager.SetSFXVolume(value);
        }
        PlayerPrefs.SetFloat("EffectVolume", value);
        PlayerPrefs.Save();
    }
    private void OnMusicVolumeChanged(float value)
    {
        if (soundManager != null)
        {
            soundManager.SetBGMVolume(value);
        }
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }

    //볼륨 적용 함수
    private void ApplyVolumes()
    {
        AudioListener.volume = MainSound.value;
        soundManager.SetSFXVolume(EffectSound.value);
        soundManager.SetBGMVolume(MusicSound.value);
    }

}
