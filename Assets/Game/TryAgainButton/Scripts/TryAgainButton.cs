using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class TryAgainButton : MonoBehaviour
    {
        [SerializeField] Button button;

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
            button.interactable = state == GameState.Round_Ended;
        }

        public void OnClick()
        {
            GameEvents.EmitTryAgainEvent();
        }
    }
}