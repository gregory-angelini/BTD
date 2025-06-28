using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Deck : MonoBehaviour
    {
        Randomizer randomizer;
        List<CardProfile> cards = new List<CardProfile>();

        [SerializeField] Image backImage;


        public void Initialize(Randomizer randomizer, DeckProfile deckConfig)
        {
            this.randomizer = randomizer;
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
                int randomIndex = randomizer.RandomRange(i, cards.Count);
                (cards[i], cards[randomIndex]) = (cards[randomIndex], cards[i]);
            }
        }
    }
}