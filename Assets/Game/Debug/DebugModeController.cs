using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameDebug
{
    public class DebugModeController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] Image background;
        [SerializeField] Color normalColor = Color.white;
        [SerializeField] Color activeColor = Color.green;

        [Header("State")]
        [SerializeField] bool isDebugEnabled = false;

       
        public void ToggleDebugMode()
        {
            isDebugEnabled = !isDebugEnabled;
            UpdateView();

            EnableDeckDebug(isDebugEnabled);
        }

        void UpdateView()
        {
            if (background != null)
                background.color = isDebugEnabled ? activeColor : normalColor;
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
               
                if (deckDebug != null)
                    Destroy(deckDebug);
            }
        }
    }
}
