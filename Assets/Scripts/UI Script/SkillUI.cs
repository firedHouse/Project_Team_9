using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private Text coolTimeText;
    [SerializeField] private Image coolTimeImage;
    [SerializeField] private Skill skills;
    private float _cooldownTime = skills.SkillCooldown;
    private float _currentCoolTime;
    

    void Awake()
    {
        skills = GetComponent<Skill>();

    }



}
