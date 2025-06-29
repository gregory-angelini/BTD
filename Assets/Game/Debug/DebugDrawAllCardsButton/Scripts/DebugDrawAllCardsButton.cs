using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameDebug
{
    public class DebugDrawAllCardsButton : MonoBehaviour
    {
        [SerializeField] DeckDebugViewer deckViewerPrefab;
        Canvas canvas;
        Deck deck;
        DeckDebugViewer deckViewer;


        void Start()
        {
            deck = FindObjectOfType<Deck>();

            if (deck == null)
            {
                Debug.LogWarning("Deck not found on scene!");
            }

            canvas = FindObjectOfType<Canvas>();

            if (canvas == null)
            {
                Debug.LogError("Canvas not found!");
            }
        }

        void OnDestroy()
        {
            if (deckViewer != null)
                Destroy(deckViewer.gameObject);
        }

        public void OnClick()
        {
            if (deck.IsEmpty())
            {
                Debug.LogWarning("Deck is empty!");
                return;
            }

            deckViewer = Instantiate(deckViewerPrefab);
            deckViewer.transform.SetParent(canvas.transform, false);

            StartCoroutine(DrawCard(0.05f));
        }

        IEnumerator DrawCard(float nextCardDelay)
        {
            while (!deck.IsEmpty())
            {
                var card = deck.DrawCard();
                deckViewer.AddCard(card);

                yield return new WaitForSeconds(nextCardDelay);
            }
        }
    }
}
