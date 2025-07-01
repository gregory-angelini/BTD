using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public partial class StartButton : MonoBehaviour
    {
        public enum StartButtonState
        {
            None,
            Start,
            Restart,
            Try_Again
        }

        [SerializeField] TextMeshProUGUI label;
        [SerializeField] Button button;
        [SerializeField] CanvasGroup canvasGroup;
        bool isClicked = false;

        StartButtonState buttonState = StartButtonState.None;
        public StartButtonState ButtonState
        {
            get => buttonState;
            set
            {
                switch (value)
                {
                    case StartButtonState.Start:
                        label.text = "START";
                        ResetButton();
                        Show();
                        break;

                    case StartButtonState.Restart:
                        label.text = "RESTART";
                        ResetButton();
                        Show();
                        break;

                    case StartButtonState.Try_Again:
                        label.text = "TRY AGAIN";
                        ResetButton();
                        Show();
                        break;

                    case StartButtonState.None:
                        button.interactable = false;
                        Hide();
                        break;
                }

                buttonState = value;
            }
        }
  
        void Show()
        {
            canvasGroup.alpha = 1f;
        }

        void Hide()
        {
            canvasGroup.alpha = 0f;
        }

        void ResetButton()
        {
            button.interactable = true;
            isClicked = false;
        }
    }
}