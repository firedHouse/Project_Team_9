using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private Text coolTimeText;
    [SerializeField] private Image coolTimeImage;

    [SerializeField] private KeyCode skillKey;
    [SerializeField] private Skill skills;

    private float _cooldownTime;
    private float _currentCoolTime;
    private bool _isCoolTime;

    private void Awake()
    {
        if (skills == null)
            skills = GetComponent<Skill>();

        _cooldownTime = skills.SkillCooldown;

        coolTimeImage.fillAmount = 0f;
        coolTimeText.text = "";
    }

    private void Update()
    {
        // 테스트용: Q를 누르면 쿨타임 시작
        if (Input.GetKeyDown(skillKey) && !_isCoolTime)
        {
            Debug.Log("스킬 사용!");
            StartCoolDown();
        }
    }

    public void StartCoolDown()
    {
        if (!_isCoolTime)
        {
            StartCoroutine(CoolTimeRoutine());
        }
    }

    private IEnumerator CoolTimeRoutine()
    {
        _isCoolTime = true;
        _currentCoolTime = _cooldownTime;
        coolTimeImage.fillAmount = 1f;

        // 쿨타임이 줄어드는 동안 반복
        while (_currentCoolTime > 0)
        {
            _currentCoolTime -= Time.deltaTime;
            coolTimeImage.fillAmount = _currentCoolTime / _cooldownTime;
            coolTimeText.text = $"{_currentCoolTime:F1}";
            yield return null;
        }

        // 쿨타임 끝
        coolTimeImage.fillAmount = 0f;
        coolTimeText.text = "";
        _isCoolTime = false;

        yield break; // 코루틴 종료
    }
}