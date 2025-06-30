using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public partial class GameController
    {
        void OnEnable()
        {
            GameEvents.OnBetPlacedEvent += OnBetPlaced;
        }

        void OnDisable()
        {
            GameEvents.OnBetPlacedEvent -= OnBetPlaced;
        }

        void OnBetPlaced(BetType bet)
        {
            if (CurrentState != GameState.Await_Player_Bet)
                return;

            playerBet = bet;

            SetState(GameState.Show_Player_Card);
        }
    }
}
