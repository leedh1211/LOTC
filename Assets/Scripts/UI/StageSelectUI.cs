using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
   [SerializeField] private int testCleardLevel;
   [SerializeField] private int testLoadSelectLevel;
   
   [SerializeField] private HorizontalSnapScrollView selectScrollView;
   [SerializeField] private Button playButton;
   [SerializeField] private RectTransform slotsParent;
   [SerializeField] private TextMeshProUGUI titleText;
   [SerializeField] private RectTransform titleParent;
   [SerializeField] private AnimationCurve lerpCurve;
   

   [Space(10f)]
   [Header("Events")]
   [SerializeField] private IntegerEventChannelSO stageStartChannel;

   
   private StageSelectUISlot[] slots;
   
   private TransformAnimationCoroutine titleScaleCoroutine;
   private TransformAnimationCoroutine playBtnScaleCoroutine;


   private void Awake()
   {
      titleScaleCoroutine = new(this, titleParent);
      playBtnScaleCoroutine = new(this, playButton.transform);
      
      
      slots = new StageSelectUISlot[slotsParent.childCount];

      for (int i = 0; i < slotsParent.childCount; i++)
      {;
         slots[i] = slotsParent.GetChild(i).GetComponent<StageSelectUISlot>();
         
         slots[i].Init(i<=testCleardLevel, i == testLoadSelectLevel);
      }
      
      
      selectScrollView.OnBeginDragEvent += (data) => OnBeginDrag();

      selectScrollView.OnEndDragEvent += (data) => OnEndDrag();

      selectScrollView.snapIndex = testLoadSelectLevel;

      
      
      playButton.onClick.AddListener(() => stageStartChannel.Raise(selectScrollView.snapIndex));
   }

   private void Start()
   {
      OnEndDrag();
   }

   void OnEndDrag()
   {
      slots[selectScrollView.snapIndex].OnSelected(out string stageName);
      
      titleText.text = stageName;
      

      bool isActive = selectScrollView.snapIndex <= testCleardLevel;
      
      playBtnScaleCoroutine.PlayScale(lerpCurve, isActive ? Vector3.zero : Vector3.one, isActive ? Vector3.one : Vector3.zero, 0.05f);
      
      titleScaleCoroutine.PlayScale(lerpCurve, Vector3.zero, Vector3.one, 0.2f);
   }

   void OnBeginDrag()
   {
      slots[selectScrollView.snapIndex].OnDeselected();
      
      titleScaleCoroutine.PlayScale(lerpCurve, Vector3.one, Vector3.zero, 0.2f);
   }
}
