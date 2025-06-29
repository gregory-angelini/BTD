using Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace GameDebug
{
    [RequireComponent(typeof(Deck))]
    public class DeckDebug : MonoBehaviour
    {
        Deck deck;
        TextMeshProUGUI deckSizeLabel;
        

        void Start()
        {
            deck = GetComponent<Deck>();

            CreateLabel();
            OnDeckSizeChanged(deck.Size);

            deck.OnDeckChanged += OnDeckSizeChanged;
        }

        void OnDestroy()
        {
            if (deck != null)
                deck.OnDeckChanged -= OnDeckSizeChanged;

            if (deckSizeLabel != null)
                Destroy(deckSizeLabel.gameObject);
        }

        void CreateLabel()
        {
            GameObject labelObject = new GameObject("DeckDebugLabel");
            RectTransform rectTransform = labelObject.AddComponent<RectTransform>();

            rectTransform.SetParent(deck.transform, false);

            rectTransform.anchorMin = new Vector2(1, 0);
            rectTransform.anchorMax = new Vector2(1, 0);
            rectTransform.pivot = new Vector2(1, 0);
            rectTransform.anchoredPosition = new Vector2(-10, 70); 

            deckSizeLabel = labelObject.AddComponent<TextMeshProUGUI>();
            deckSizeLabel.fontSize = 120;
            deckSizeLabel.alignment = TextAlignmentOptions.Center;
            deckSizeLabel.color = Color.yellow;
            deckSizeLabel.text = "0";
        }

        void OnDeckSizeChanged(int newSize)
        {
            if (deckSizeLabel != null)
                deckSizeLabel.text = $"{newSize}";
        }
    }
}
