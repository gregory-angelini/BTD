using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Game
{
    public partial class GameController
    {
        void OnEnable()
        {
            GameEvents.OnBetPlacedEvent += OnBetPlaced;
            GameEvents.OnTryAgainEvent += OnTryAgain;
            GameEvents.OnStartEvent += OnStart;
        }

        void OnDisable()
        {
            GameEvents.OnBetPlacedEvent -= OnBetPlaced;
            GameEvents.OnTryAgainEvent -= OnTryAgain;
            GameEvents.OnStartEvent -= OnStart;
        }

        public void OnStart()
        {
            TransitionToState(GameState.Start);
        }

        void OnBetPlaced(BetType bet)
        {
            if (CurrentState != GameState.Await_Player_Bet)
                return;

            playerBet = bet;

            TransitionToState(GameState.Show_Player_Card);
        }

        void OnTryAgain()
        {
            TransitionToState(GameState.New_Round);
        }
    }
}
