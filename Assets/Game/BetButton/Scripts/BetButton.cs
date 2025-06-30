using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BetButton : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] BetType betType;
        [SerializeField] Color defaultColor = Color.white;
        [SerializeField] Color selectedColor = Color.green;
        [SerializeField] Image background;

        
        void ResetButton()
        {
            background.color = defaultColor;
        }

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
            GameEvents.EmitBetPlacedEvent(betType);
        }

        void OnBetPlaced(BetType selectedBet)
        {
            background.color = selectedBet == betType ? selectedColor : defaultColor;
        }
    }
}
