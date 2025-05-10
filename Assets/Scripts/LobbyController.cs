using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
   public int testCleardLevel;
   public int testSelectStage;
   
   [SerializeField] private IntegerEventChannelSO stageStartChannel;


   private void Awake()
   {
      stageStartChannel.OnEventRaised += GameStartToStage;
   }


   void GameStartToStage(int stageLevel)
   {
      Debug.Log(stageLevel);
   }
}
