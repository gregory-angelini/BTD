using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameDebug
{
    public class DebugModeButton : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] Image background;
        [SerializeField] Color normalColor = Color.white;
        [SerializeField] Color activeColor = Color.green;

        [Header("State")]
        [SerializeField] bool isDebugEnabled = false;

        [SerializeField] DebugDrawAllCardsButton debugDrawAllCardsButtonPrefab;
        DebugDrawAllCardsButton debugDrawAllCardsButton;
        [SerializeField] DebugQuickRoundButton debugQuickRoundButtonPrefab;
        DebugQuickRoundButton debugQuickRoundButton;

        GameController gameController;


        void Start()
        {
            gameController = FindObjectOfType<GameController>();

            if (gameController == null)
            {
                Debug.LogError("Game Controller not found!");
            }
        }

        public void OnClick()
        {
            ToggleDebugMode();
        }
        
        void ToggleDebugMode()
        {
            isDebugEnabled = !isDebugEnabled;
            UpdateView();

            EnableDeckDebug(isDebugEnabled);
            EnableDebugDrawAllCardsButton(isDebugEnabled);
            EnableDebugQuickRoundButton(isDebugEnabled);
            EnableQuickAnims(isDebugEnabled);
        }

        void EnableQuickAnims(bool enable)
        {
            gameController.SkipAnims = enable;
        }

        void UpdateView()
        {
            if (background != null)
                background.color = isDebugEnabled ? activeColor : normalColor;
        }

        void EnableDebugQuickRoundButton(bool enable)
        {
            if (enable)
            {
                var deck = FindObjectOfType<Deck>();
                if (deck == null) return;

                debugQuickRoundButton = Instantiate(debugQuickRoundButtonPrefab);
                debugQuickRoundButton.transform.SetParent(deck.transform, false);
                debugQuickRoundButton.transform.localScale = new Vector3(5f, 5f, 5f);

                var debugQuickRoundButtonRectTransform = debugQuickRoundButton.GetComponent<RectTransform>();
                debugQuickRoundButtonRectTransform.anchorMin = new Vector2(0.5f, 0f);
                debugQuickRoundButtonRectTransform.anchorMax = new Vector2(0.5f, 0f);
                debugQuickRoundButtonRectTransform.pivot = new Vector2(0.5f, 1f);
                debugQuickRoundButtonRectTransform.anchoredPosition = new Vector2(0f, -220f);
            }
            else
            {
                Destroy(debugQuickRoundButton.gameObject);
            }
        }

        void EnableDebugDrawAllCardsButton(bool enable)
        {
            if (enable)
            {
                var deck = FindObjectOfType<Deck>();
                if (deck == null) return;

                debugDrawAllCardsButton = Instantiate(debugDrawAllCardsButtonPrefab);
                debugDrawAllCardsButton.transform.SetParent(deck.transform, false);
                debugDrawAllCardsButton.transform.localScale = new Vector3(5f, 5f, 5f);

                var debugDrawAllCardsButtonRectTransform = debugDrawAllCardsButton.GetComponent<RectTransform>();
                debugDrawAllCardsButtonRectTransform.anchorMin = new Vector2(0.5f, 0f);
                debugDrawAllCardsButtonRectTransform.anchorMax = new Vector2(0.5f, 0f);
                debugDrawAllCardsButtonRectTransform.pivot = new Vector2(0.5f, 1f);
                debugDrawAllCardsButtonRectTransform.anchoredPosition = new Vector2(0f, -40f);
            }
            else
            {
                Destroy(debugDrawAllCardsButton.gameObject);
            }
        }

        void EnableDeckDebug(bool enable)
        {
            if (enable)
            {
                var deck = FindObjectOfType<Deck>();
                if (deck == null) return;

                if (deck.GetComponent<DeckDebug>() == null)
                    deck.gameObject.AddComponent<DeckDebug>();
            }
            else
            {
                var deck = FindObjectOfType<Deck>();
                if (deck == null) return;

                var deckDebug = deck.GetComponent<DeckDebug>();
                if (deckDebug == null) Debug.LogWarning("DeckDebug is missing!");

                if (deckDebug != null)
                    Destroy(deckDebug);
            }
        }
    }
}
