using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
   [SerializeField] private LobbyController lobbyController;
   
   [SerializeField] private HorizontalSnapScrollView selectScrollView;
   [SerializeField] private Button selectButton;
   [SerializeField] private RectTransform slotsParent;
   [SerializeField] private TextMeshProUGUI titleText;
   [SerializeField] private RectTransform titleParent;
   
   [Space(10f)]
   [SerializeField] private AnimationCurve  easeOutCurve;

   [Space(10f)]
   [Header("Events")]
   [SerializeField] private IntegerEventChannelSO selectStageChannel;

   
   private StageSelectUISlot[] slots;
   
   private ProgressTweener testTitleTweener;


   private void Awake()
   {
      testTitleTweener = new(this);
      
      slots = new StageSelectUISlot[slotsParent.childCount];

      for (int i = 0; i < slotsParent.childCount; i++)
      {;
         slots[i] = slotsParent.GetChild(i).GetComponent<StageSelectUISlot>();
      }
      
   }

   private void Start()
   {
      selectButton.onClick.AddListener(() =>
      {
         selectStageChannel.Raise(selectScrollView.snapIndex);
      });
      
      
      for (int i = 0; i < slotsParent.childCount; i++)
      {;
         slots[i].Init(i<=lobbyController.testCleardLevel, i == lobbyController.testSelectStage);
      }

      
      selectScrollView.OnBeginDragEvent += (data) => EnableItems(false);

      selectScrollView.OnEndDragEvent += (data) => EnableItems(true);

      selectScrollView.snapIndex = lobbyController.testSelectStage;
   }

   

   void EnableItems(bool enable)
   {
         if (enable)
         {
            slots[selectScrollView.snapIndex].OnSelected(out string stageName);
      
            titleText.text = stageName;

            bool isCleared = selectScrollView.snapIndex <= lobbyController.testCleardLevel;

            if (selectButton.gameObject.activeSelf != isCleared)
            {
               selectButton.gameObject.SetActive(isCleared);
            }
            
            testTitleTweener.Play((ratio) => titleParent.localScale = Vector3.one * ratio, 0.2f).SetCurve(easeOutCurve);
         }
         else
         {
            slots[selectScrollView.snapIndex].OnDeselected();
      
            testTitleTweener.Play((ratio) => titleParent.localScale = Vector3.one *(1f - ratio), 0.2f).SetCurve(easeOutCurve);
         }
   }
}
