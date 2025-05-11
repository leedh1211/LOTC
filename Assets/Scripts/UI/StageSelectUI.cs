using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
   [SerializeField] private LobbyController lobbyController;
   
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
   [Header("Events")]
   [SerializeField] private IntegerEventChannelSO selectedStageLevel;

   
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
         selectedStageLevel.Raise(selectScrollView.snapIndex);
         Disable();
      });
      
      
      for (int i = 0; i < slotsParent.childCount; i++)
      {;
         slots[i].Init(i<=lobbyController.testCleardLevel, i == lobbyController.testSelectStage);
      }

      
      selectScrollView.OnBeginDragEvent += (data) => EnableItems(false);

      selectScrollView.OnEndDragEvent += (data) => EnableItems(true);

      selectScrollView.snapIndex = lobbyController.testSelectStage;
   }


   public void Enable()
   {
      selectScrollView.DirectUpdateItemList(lobbyController.testSelectStage);

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
            slots[selectScrollView.snapIndex].OnSelected(out string stageName);
      
            titleText.text = stageName;

            bool isCleared = selectScrollView.snapIndex <= lobbyController.testCleardLevel;

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
