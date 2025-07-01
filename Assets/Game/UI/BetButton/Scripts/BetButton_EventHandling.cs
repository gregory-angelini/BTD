using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public partial class BetButton
    {
        void OnEnable()
        {
            GameEvents.OnGameStateChangedEvent += OnGameStateChanged;
            GameEvents.OnBetPlacedEvent += OnBetPlaced;
        }

        void OnDisable()
        {
            GameEvents.OnGameStateChangedEvent -= OnGameStateChanged;
            GameEvents.OnBetPlacedEvent -= OnBetPlaced;
        }

        void OnGameStateChanged(GameState state)
        {
            button.interactable = state == GameState.Await_Player_Bet;

            switch (state)
            {
                case GameState.Initialize:
                case GameState.Start:
                case GameState.New_Round:
                    ResetButton();
                    break;
            }
        }

        public void OnClick()
        {
            if (isClicked)
                return;

            isClicked = true;

            GameEvents.EmitBetPlacedEvent(betType);
        }
    }
}
