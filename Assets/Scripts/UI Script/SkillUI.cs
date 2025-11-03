using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private Text coolTimeText;
    [SerializeField] private Image coolTimeImage;
    [SerializeField] private KeyCode skillKey;
    
    private Player player;

    private float _cooldownTime;
    private float _currentCoolTime;
    private bool _isCoolTime;

    private void Awake()
    {


        if (player == null)
        {
            player = GetComponent<Player>();
        }
        StartCoroutine(WaitForPlayer());

        coolTimeImage.fillAmount = 0f;
        coolTimeText.text = "";
    }

    private IEnumerator WaitForPlayer()
    {
        while (Player.Instance == null)
        {
            yield return new WaitForSeconds(0.1f);
        }

        player = Player.Instance;

    }

    private void Update()
    {
        // 테스트용: skillKey를 누르면 쿨타임 시작
        if (Input.GetKeyDown(skillKey) && !_isCoolTime)
        {
            Debug.Log($"{skillKey}스킬 사용!");
            SetCoolTime();
            StartCoroutine(CoolTimeRoutine());
        }
    }

    public void SetCoolTime() //쿨타임일 때 스킬 발동 X
    {
        switch (skillKey)
        {
            case KeyCode.Q:
                _cooldownTime = player.Cooltime;
                break;
            case KeyCode.W:
                _cooldownTime = player.WSkillCooltime;
                break;
            case KeyCode.E:
                _cooldownTime = player.ESkillCooltime;
                break;
            default:
                _cooldownTime = 1f; // 쿨타임 기본값
                break;
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