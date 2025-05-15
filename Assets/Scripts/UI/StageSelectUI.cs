using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
   [SerializeField] private LobbyMainUI mainUI;
   
   [Space(10f)]
   [SerializeField] private CanvasGroup canvasGroup;
   [SerializeField] private HorizontalSnapScrollView selectScrollView;
   
   [Space(10f)]
   [SerializeField] private RectTransform slotsParent;
   [SerializeField] private TextMeshProUGUI titleText;
   [SerializeField] private RectTransform titleParent;
   [SerializeField] private Button selectButton;

   
   [Space(10f)]
   [SerializeField] private AnimationCurve  easeOutCurve;
   [SerializeField] private AnimationCurve  easeInCurve;

   [Space(10f)]
   [SerializeField] private IntegerVariableSO selectedStageLevel;
   [SerializeField] private IntegerVariableSO cleardStageLevel;
   
   [SerializeField] private StageDataTableSO stageDataTable;

   
   private StageSelectUISlot[] slots;
   
   private ProgressTweener testTitlePopTweener;
   private ProgressTweener canvasGroupTweener;
   

   private void Awake()
   {
      testTitlePopTweener = new(this);

      canvasGroupTweener = new(this);

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
         selectedStageLevel.RuntimeValue = selectScrollView.snapIndex;
         
         mainUI.SetStageInfo(selectedStageLevel.RuntimeValue);
         
         Disable();
      });

      for (int i = 0; i < slotsParent.childCount; i++)
      {;
         slots[i].Init(i<= cleardStageLevel.RuntimeValue, i == selectedStageLevel.RuntimeValue);
      }

      
      selectScrollView.OnBeginDragEvent += (data) => EnableItems(false);

      selectScrollView.OnEndDragEvent += (data) => EnableItems(true);

      selectScrollView.snapIndex = selectedStageLevel.RuntimeValue;
   }


   public void Enable()
   {
      selectScrollView.DirectUpdateItemList(selectedStageLevel.RuntimeValue);

      EnableItems(true);
      
      canvasGroup.blocksRaycasts = true;
      canvasGroupTweener.Play((ratio) => canvasGroup.alpha = Mathf.Lerp(0, 1, ratio), 0.1f).SetCurve(easeOutCurve);
   }

   public void Disable()
   {
      canvasGroup.blocksRaycasts = false;
      canvasGroupTweener.Play((ratio) => canvasGroup.alpha = Mathf.Lerp(1, 0, ratio), 0.1f).SetCurve(easeInCurve);
   }

   

   void EnableItems(bool enable)
   {
         if (enable)
         {
            int snapIndex = selectScrollView.snapIndex;
            
            titleText.text = stageDataTable.Datas[snapIndex].StageName;
            

            bool isCleared = snapIndex <= cleardStageLevel.RuntimeValue;

            if (selectButton.gameObject.activeSelf != isCleared)
            {
               selectButton.gameObject.SetActive(isCleared);
            }
            
            testTitlePopTweener.Play((ratio) => titleParent.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, ratio), 0.1f).SetCurve(easeOutCurve);
         }
         else
         {
            testTitlePopTweener.Play((ratio) => titleParent.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, ratio), 0.1f).SetCurve(easeInCurve);
         }
   }
}
