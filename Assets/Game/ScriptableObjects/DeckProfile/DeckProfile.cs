using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    [CreateAssetMenu(fileName = "DeckProfile", menuName = "Cards/DeckProfile")]
    public class DeckProfile : ScriptableObject
    {
        public Sprite BackSprite;
        public CardProfile[] Cards;
    }
}