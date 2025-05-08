using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
   [SerializeField] private IntegerEventChannelSO LoadStageLevelEvent;

   [SerializeField] private int testSaveLevel;


   private void Start()
   { 
      LoadStageLevelEvent.Raise(testSaveLevel);
   }
}
