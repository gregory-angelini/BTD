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
            GameEvents.OnRestartEvent += OnRestart;
        }

        void OnDisable()
        {
            GameEvents.OnBetPlacedEvent -= OnBetPlaced;
            GameEvents.OnTryAgainEvent -= OnTryAgain;
            GameEvents.OnStartEvent -= OnStart;
            GameEvents.OnRestartEvent -= OnRestart;
        }

        public void OnStart()
        {
            if (CurrentState != GameState.Initialize)
                return;

            TransitionToState(GameState.Start);
        }

        public void OnRestart()
        {
            if (CurrentState != GameState.Deck_Empty)
                return;

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
            if (CurrentState != GameState.Round_Ended)
                return;

            TransitionToState(GameState.New_Round);
        }
    }
}
