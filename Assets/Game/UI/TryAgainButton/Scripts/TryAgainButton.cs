using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class TryAgainButton : MonoBehaviour
    {
        [SerializeField] Button button;
        bool isClicked = false;


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
            if (state == GameState.Round_Ended)
            {
                button.interactable = true;
                ResetButton();
            }
            else
            {
                button.interactable = false;
            }
        }

        void ResetButton()
        {
            isClicked = false;
        }

        public void OnClick()
        {
            if (isClicked)
                return;

            isClicked = true;

            GameEvents.EmitTryAgainEvent();
        }
    }
}