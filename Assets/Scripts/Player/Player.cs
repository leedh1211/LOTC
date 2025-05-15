using System;
using System.Collections.Generic;
using Monster.Skill;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public PlayerVisual PlayerVisual => playerVisual;
    public Animator Animator => animator;
    
    private float maxHp = 100;
    private float curHp = 100;

    [SerializeField] private PlayerStatVariableSO permanentStat;
    public float MaxHp {  get { return maxHp + permanentStat.RuntimeValue.MaxHp*10; } }
    public float CurHp { get { return curHp + permanentStat.RuntimeValue.MaxHp*10; } }
    
    public MonsterListVariableSO monsterList;

    private PlayerController playerController;

    private WeaponHandler weapon;
    
    [Header("OrbitSkill")]
    [SerializeField]private GameObject orbitPrefab;
    
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private Animator animator;
    
    [SerializeField] private Image healthBar;

    
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerController.Init(playerVisual);
        
        //Weapon
        weapon = GetComponentInChildren<WeaponHandler>();
    }

    private void Update()
    {
        healthBar.fillAmount = CurHp / MaxHp;
    }


    public void SetSkillData(SkillData data)
    {
        object effect = SkillEffectFactory.CreateEffect(data.type);
        if (effect == null)
        {
            return;
        }

        var context = new SkillApplyContext
        {
            player = this,
            weaponHandler = weapon
            //직접 인스턴스화 시켜서 사용함. 
        };
        
        SkillManager.Instance.LearnSkill(data, context); //스킬 적용
    }

    public MonsterController GetNearestEnemy()
    {
        float Nearest = Mathf.Infinity;
        float dis;
        
        MonsterController NearestMonster = null;


        for (int i = 0; i < monsterList.RuntimeValue.Count; i++)
        {
            //
            dis = (transform.position - monsterList.RuntimeValue[i].transform.position).sqrMagnitude;

            if (dis < Nearest)
            {
                NearestMonster = monsterList.RuntimeValue[i];
                Nearest = dis;

            }
           
        }
        if (monsterList.RuntimeValue.Count > 0)
        {
            return NearestMonster;
        }
        else
        {
            return null;
        }
    }


    public void SetOrbitSkill()
    { 
        GameObject orbit =  Instantiate(orbitPrefab);
        orbit.transform.SetParent(this.transform);

    }

    [SerializeField] private HitFeedbackPannel feedbackPannel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ProjectileController projectileController))
        {
            TakeDamage(projectileController.Damage);
        }
    }

    public void TakeDamage(float damage)
    {
        feedbackPannel.enabled = true;

        curHp -= (int)damage;
    }
}


