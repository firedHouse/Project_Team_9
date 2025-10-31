using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SFXClip
{
    public string clipName;
    public AudioClip AudioClip;
}

//BGM, SFX 관리 매니저
//BGM재생 및 정지, 효과음 재생, 옵션 볼륨 설정
//가능하다면 각 스크립트에서 편하게 불러올 수 있는 소리파일 저장소를 제작

public class SoundManager : Singleton<SoundManager>
{
    [Header("BGM Source")]
    [SerializeField] private AudioSource _bgmSource;

    private AudioSource _sfxSource;
    
    [Header("SFX Clips")]
    [SerializeField] private List<SFXClip> _sfxClips = new List<SFXClip>();

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float _bgmVolume = 0.5f;
    [Range(0f, 1f)]
    [SerializeField] private float _sfxVolume = 1.0f;


    //인게임에서 빠르게 검색하기 위한 효과음 클립 딕셔너리 
    private Dictionary<string, AudioClip> _sfxDictionary = new Dictionary<string, AudioClip>();

    //인스펙터에 설정한 효과음 리스트를 더 빠르게 검색하기 위해 딕셔너리 구조로 변경하는 메서드
    private void InitializeSFXDictionary()
    {
        foreach (var sfx in _sfxClips)
        {
            if (string.IsNullOrEmpty(sfx.clipName) || sfx.AudioClip == null)
            {
                Debug.Log("딕셔너리 추가되지 않은 항목 있음");
                continue;
            }
            //키값 중복체크, 중복 아닐 시 키값으로 이름, 실제 클립을 딕셔너리에 추가
            if (!_sfxDictionary.ContainsKey(sfx.clipName))
            {
                _sfxDictionary.Add(sfx.clipName, sfx.AudioClip);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        if (_bgmSource == null)
        {
            _bgmSource = gameObject.AddComponent<AudioSource>();
        }
        if (_sfxSource == null)
        {
            _sfxSource = gameObject.AddComponent<AudioSource>();
        }
        _bgmSource.loop = true;
        _bgmSource.volume = _bgmVolume;
        _sfxSource.volume = _sfxVolume;
    }




    //BGM 재생, 현재 클립과 다르면 플레이
    public void PlayBGM(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.Log("노래 없어요");
            return;
        }
        if (_bgmSource.clip != clip)
        {
            _bgmSource.clip = clip;
            _bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        if (_bgmSource.isPlaying)
        {
            StartCoroutine(FadeOutBGM(3f));
        }
    }

    //타이머 하나 만들어서 타이머가 입력받은 인자값만큼 지나면서 천천히 볼륨 감소.
    private IEnumerator FadeOutBGM(float duration)
    {

        Debug.Log("페이드아웃 이후 종료해");
        float currentVolume = _bgmSource.volume;
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            //현재 볼륨에서 0까지 부드럽게 감소
            _bgmSource.volume = Mathf.Lerp(currentVolume, 0f, timer / duration);
            yield return null;
        }
        //종료하고 원래볼륨으로 복구
        _bgmSource.Stop();
        _bgmSource.volume = _bgmVolume;
    }

    //효과음 재생 메서드
    //인자값 clip, 볼륨, PlayOneShot = 재생중인 SFX에 영향주지 않고 재생
    public void PlaySFX(AudioClip clip, float volumeScale = 1.0f)
    {
        if (clip == null)
        {
            return;
        }
        _sfxSource.PlayOneShot(clip, _sfxVolume * volumeScale);
    }

    //ESC 옵션 메뉴에 연결하기 위한 볼륨 조절 메서드
    //배경음악 볼륨 설정
    public void SetBGMVolume(float volume)
    {
        _bgmVolume = Mathf.Clamp01(volume);
        _bgmSource.volume = _bgmVolume;
    }

    //효과음 볼륨 설정
    public void SetSFXVolume(float volume)
    {
        _sfxVolume = Mathf.Clamp01(volume);
    }

}
