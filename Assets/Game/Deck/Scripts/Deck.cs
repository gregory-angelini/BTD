using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Game
{
    public class Deck : MonoBehaviour
    {
        public event Action<int> OnDeckChanged;

        WeightedRandomSelector weightedRandomSelector;
        List<ItemChance<CardProfile>> cardChances = new();
        int weightScaleFactor;

        #region references
        [SerializeField] Image backImage;
        #endregion

        public int Size => cardChances.Count;


        public void Initialize(int seed, int weightScaleFactor, List<ItemChance<CardProfile>> cardChances, DeckProfile deckConfig)
        {
            weightedRandomSelector = new WeightedRandomSelector(seed);
           
            this.weightScaleFactor = weightScaleFactor;
            this.cardChances = new List<ItemChance<CardProfile>>(cardChances);
       
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
            for (int i = 0; i < cardChances.Count; i++)
            {
                int randomIndex = weightedRandomSelector.Randomizer.RandomRange(i, cardChances.Count);
                (cardChances[i], cardChances[randomIndex]) = (cardChances[randomIndex], cardChances[i]);
            }
        }

        void NotifyDeckChanged()
        {
            OnDeckChanged?.Invoke(Size);
        }

        public bool IsEmpty()
        {
            return cardChances.Count == 0;
        }

        public CardProfile DrawCard()
        {
            if (IsEmpty())
            {
                Debug.LogWarning("Deck is empty!");
                return null;
            }

            Func<double, bool> validateChance = chance => chance > 0 && chance <= 100;

            var weightedCards = WeightUtils.ConvertChancesToWeights(
                cardChances,
                validateChance,
                weightScaleFactor);

            var drawnCard = weightedRandomSelector.PickRandom(weightedCards);
            Debug.Log($"Drawn card: {drawnCard.Item}");

            #region remove drawn card from deck
            var foundCard = cardChances.Find(cardChance => cardChance.Item == drawnCard.Item);

            Assert.IsNotNull(foundCard, $"foundCard is null");
            cardChances.Remove(foundCard);
            #endregion

            NotifyDeckChanged();
            return drawnCard.Item;
        }
    }
}