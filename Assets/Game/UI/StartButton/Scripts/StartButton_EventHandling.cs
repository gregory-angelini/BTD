using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public partial class StartButton : MonoBehaviour
    {
        void OnEnable()
        {
            GameEvents.OnGameStateChangedEvent += OnGameStateChanged;
        }

        void OnDisable()
        {
            GameEvents.OnGameStateChangedEvent -= OnGameStateChanged;
        }

        void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Initialize:
                    ButtonState = StartButtonState.Start;
                    break;

                case GameState.Round_Ended:
                    ButtonState = StartButtonState.Try_Again;
                    break;

                case GameState.Deck_Empty:
                    ButtonState = StartButtonState.Restart;
                    break;

                default:
                    ButtonState = StartButtonState.None;
                    break;
            }
        }

        public void OnClick()
        {
            if (isClicked)
                return;

            switch (buttonState)
            {
                case StartButtonState.Start:
                    GameEvents.EmitStartEvent();
                    isClicked = true;
                    break;

                case StartButtonState.Restart:
                    GameEvents.EmitRestartEvent();
                    isClicked = true;
                    break;

                case StartButtonState.Try_Again:
                    GameEvents.EmitTryAgainEvent();
                    isClicked = true;
                    break;
            }
        }
    }
}
