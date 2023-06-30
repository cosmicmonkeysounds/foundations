﻿using System;
using Aarthificial.Typewriter.Blackboards;
using Player;
using UnityEngine;

namespace Interactions {
  [Serializable]
  public struct InteractionContext : IBlackboardProvider {
    public static int Initiator = -963223665;
    public static int IsLTPresent = -95002655;
    public static int IsRTPresent = -514433065;
    public static int CurrentSpeaker = -1596563589;
    public static int Listener = -1809211332;
    public static int LT = -417971899;
    public static int RT = -2078916288;
    public static int CallOther = 2014434846;
    public static int PickUp = -2028872245;

    private static int _contextScope = -1768494618;
    private static int _interactionScope = -1936302086;
    public Blackboard Context;
    [NonSerialized] public Blackboard Interaction;

    public bool TryGetBlackboard(int scope, out IBlackboard blackboard) {
      if (scope == _interactionScope) {
        blackboard = Interaction;
        return true;
      }

      if (scope == _contextScope) {
        blackboard = Context;
        return true;
      }

      blackboard = default;
      return false;
    }

    public void Setup() {
      Context.Set(CurrentSpeaker, 0);
      Context.Set(LT, LT);
      Context.Set(RT, RT);
    }

    public void SetSpeaker(int speaker) {
      Context.Set(CurrentSpeaker, speaker);
    }
  }
}