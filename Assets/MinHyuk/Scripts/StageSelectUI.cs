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
   
   [SerializeField] private HorizontalSnapScroll selectScroll;
   [SerializeField] private Button playButton;
   [SerializeField] private RectTransform slotsParent;
   [SerializeField] private TextMeshProUGUI titleText;
   

   [Space(10f)]
   [Header("Events")]
   [SerializeField] private IntegerEventChannelSO stageStartChannel;

   private StageSelectUISlot[] slots;

   private void Awake()
   {
      slots = new StageSelectUISlot[slotsParent.childCount];

      for (int i = 0; i < slotsParent.childCount; i++)
      {;
         slots[i] = slotsParent.GetChild(i).GetComponent<StageSelectUISlot>();
         
         slots[i].Init(i<=testCleardLevel, i == testLoadSelectLevel);
      }
      
      
      selectScroll.OnBeginDragEvent += (data) => DeselectSlot();

      selectScroll.OnEndDragEvent += (data) => SelectSlot();

      selectScroll.centerIndex = testLoadSelectLevel;

      
      playButton.onClick.AddListener(() => stageStartChannel.Raise(selectScroll.centerIndex));
   }

   private void Start()
   {
      SelectSlot();
   }

   void SelectSlot()
   {
      slots[selectScroll.centerIndex].OnSelected(out string stageName);
         
      titleText.gameObject.SetActive(true);
      titleText.text = stageName;
         
      ActivePlayButton(selectScroll.centerIndex <= testCleardLevel);
   }

   void DeselectSlot()
   {
      slots[selectScroll.centerIndex].OnDeselected();
         
      titleText.gameObject.SetActive(false);
         
      ActivePlayButton(false);
   }


   void ActivePlayButton(bool isActive)
   {
      if (playButton.gameObject.activeSelf != isActive)
      {
         playButton.gameObject.SetActive(isActive);
      }
   }
}
