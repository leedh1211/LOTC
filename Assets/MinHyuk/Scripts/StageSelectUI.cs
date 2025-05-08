using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectUI : HorizontalSnapScrollRect
{
   /*[Space(10f)]
   [Header("Events")]
   [SerializeField] private IntegerEventChannelSO LoadStageLevelEvent;

   protected override void Awake()
   {
      base.Awake();

      LoadStageLevelEvent.OnEventRaised += SetSnapIndex;
   }


   private void OnDestroy()
   {
      LoadStageLevelEvent.OnEventRaised -= SetSnapIndex;
   }

   protected override void SetContentPos(int index)
   {
      base.SetContentPos(index);
      
      LoadStageLevelEvent.Raise(currentSnapIndex);
   }*/
}
