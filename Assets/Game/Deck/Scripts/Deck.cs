using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Deck : MonoBehaviour
    {
        WeightedRandomSelector weightedRandomSelector;
        List<CardProfile> cards = new();
        List<ItemChance<CardProfile>> chances = new();

        #region settings
        [SerializeField] int precision = 100;
        #endregion

        #region references
        [SerializeField] Image backImage;
        #endregion

        public int AmountOfCards => cards.Count;


        public void Initialize(int seed, List<ItemChance<CardProfile>> chances, DeckProfile deckConfig)
        {
            weightedRandomSelector = new WeightedRandomSelector(seed);

            this.chances = chances;
            cards = new List<CardProfile>(deckConfig.Cards);

            SetCardBack(deckConfig.BackSprite);

            Shuffle();
        }

        
        void SetCardBack(Sprite backSprite)
        {
            backImage.sprite = backSprite;
            backImage.SetNativeSize();
        }

        public void Shuffle()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                int randomIndex = weightedRandomSelector.Randomizer.RandomRange(i, cards.Count);
                (cards[i], cards[randomIndex]) = (cards[randomIndex], cards[i]);
            }
        }

        public bool IsEmpty()
        {
            return cards.Count == 0;
        }

        public CardProfile DrawCard()
        {
            if (cards.Count == 0)
            {
                Debug.LogWarning("Deck is empty!");
                return null;
            }

            Func<double, bool> validateChance = chance => chance > 0 && chance <= 100;

            var weightedCards = WeightUtils.ConvertChancesToWeights(
                chances,
                validateChance,
                precision);

            var drawnCard = weightedRandomSelector.PickRandom(weightedCards);
            Debug.Log($"Drawn card: {drawnCard.Item}");
            return drawnCard.Item;
        }
    }
}