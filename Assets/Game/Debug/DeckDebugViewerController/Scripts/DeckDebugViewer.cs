using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameDebug
{
    public class DeckDebugViewer : MonoBehaviour
    {
        [SerializeField] Transform cardContainer;
        [SerializeField] CardView cardViewPrefab;
        [SerializeField] float cardScale = 0.25f; 


        public void DisplayCards(List<CardProfile> cards)
        {
            if (cards == null || cards.Count == 0) 
                return;

            Sprite sprite = cards[0].FaceSprite;
            SetCellSizeBySprite(sprite, cardScale);

            Clear();

            foreach (var card in cards)
            {
                var cardView = Instantiate(cardViewPrefab, cardContainer);
                cardView.SetSprite(card.FaceSprite);
            }
        }

        public void AddCard(CardProfile card)
        {
            if (cardContainer.transform.childCount == 0)
            {
                Sprite sprite = card.FaceSprite;
                SetCellSizeBySprite(sprite, cardScale);
            }

            var cardView = Instantiate(cardViewPrefab, cardContainer);
            cardView.SetSprite(card.FaceSprite);   
        }

        void SetCellSizeBySprite(Sprite sprite, float scale)
        {
            if (sprite == null) return;

            float width = sprite.rect.width;
            float height = sprite.rect.height;
            float ppu = sprite.pixelsPerUnit;
            float unityWidth = width / ppu;
            float unityHeight = height / ppu;
            float scaledWidth = unityWidth * scale * Screen.dpi;
            float scaledHeight = unityHeight * scale * Screen.dpi;

            var cardContainerGridLayoutGroup = cardContainer.GetComponent<GridLayoutGroup>();
            if (cardContainerGridLayoutGroup == null) Debug.LogWarning("GridLayoutGroup is missing!");

            cardContainerGridLayoutGroup.cellSize = new Vector2(scaledWidth, scaledHeight);
        }

        public void Clear()
        {
            foreach (Transform child in cardContainer)
                Destroy(child.gameObject);
        }
    }
}
