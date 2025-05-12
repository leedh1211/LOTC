using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
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

        SkillManager.Instance.ApplySkill(effect,context);


    
    }

    


    
}


