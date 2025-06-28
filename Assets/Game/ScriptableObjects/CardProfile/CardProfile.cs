using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    [CreateAssetMenu(fileName = "CardProfile", menuName = "Cards/CardProfile")]
    public class CardProfile : ScriptableObject
    {
        public Sprite FaceSprite;
        public Suit Suit;
        public Rank Rank;
    }
}
