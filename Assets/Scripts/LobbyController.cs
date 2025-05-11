using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
   public int testCleardLevel;
   public int testSelectStage;
   
   [SerializeField] private IntegerEventChannelSO selectedStageLevel;


   private void Start()
   {
      selectedStageLevel.OnEventRaised += SetStage;
   }

   private void OnDestroy()
   {
      selectedStageLevel.OnEventRaised -= SetStage;
   }

   void SetStage(int level) => testSelectStage = level;

}
