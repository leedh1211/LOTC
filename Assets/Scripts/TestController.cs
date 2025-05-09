using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
   [SerializeField] private IntegerEventChannelSO stageStartChannel;

   [SerializeField] private Transform testTransform;


   private void Awake()
   {
      stageStartChannel.OnEventRaised += GameStartToStage;
   }

   private void Start()
   {
   }



   void GameStartToStage(int stageLevel)
   {
      Debug.Log(stageLevel);
   }
}
