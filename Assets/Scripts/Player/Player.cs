using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private int maxHp;
    private int curHp;

    [SerializeField] private PlayerStatVariableSO permanentStat;
    public int MaxHp {  get { return maxHp+ permanentStat.RuntimeValue.MaxHp; } }
    public int CurHp { get { return curHp + permanentStat.RuntimeValue.MaxHp; } }
    public MonsterListVariableSO monsterList;

    private PlayerController playerController;

    private WeaponHandler weapon;
    [Header("OrbitSkill")]
    [SerializeField]private GameObject orbitPrefab;
    
    [SerializeField] private PlayerVisual playerVisual;

    
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerController.Init(playerVisual);

        //Weapon
        weapon = GetComponentInChildren<WeaponHandler>();
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




}


