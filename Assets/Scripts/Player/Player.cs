using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public struct PlayerState
    {
        public int maxHp;
        public int curHp;
        public int Speed;

    }
    private PlayerController playerController;
    [SerializeField] private PlayerVisual playerVisual;

    [Header("WeaPon")]
    private WeaponHandler weapon;
    
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

    /*
     * 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SkillData skill = Resources.Load<SkillData>("ScriptableObejcts/SKillData/MultiShot");
            if (skill != null)
            {
                SetSkillData(skill);
            }
            else
            {
                Debug.Log("스킬이 없습니다.");
                return;
            }
        }
    }

    */


}


