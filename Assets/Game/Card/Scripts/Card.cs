using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class Card : MonoBehaviour
    {
        [SerializeField] CardView view;
        public CardView View { get => view; }
        CardProfile cardProfile;
        public CardProfile CardProfile { get => cardProfile; }

        bool isFaceUp = false;
        public bool IsFaceUp 
        { 
            get
            {
                return isFaceUp;
            }
            set
            {
                isFaceUp = value;

                if (isFaceUp)
                    View.SetSprite(CardProfile.FaceSprite);
                else 
                    View.SetSprite(View.BackSprite);
            }
        }
        public Suit Suit { get => cardProfile.Suit; }
        public Rank Rank { get => cardProfile.Rank; }


        public void Setup(CardProfile cardProfile, Sprite backSprite, bool isFaceUp)
        {
            this.cardProfile = cardProfile;

            view.SetFaceSprite(cardProfile.FaceSprite);
            view.SetBackSprite(backSprite);

            IsFaceUp = isFaceUp;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
        }

        public void SetPosition(Vector2 pos)
        {
            transform.localPosition = new Vector3(pos.x, pos.y, transform.localPosition.z);
        }

        public Vector2 GetPosition()
        {
            return transform.localPosition;
        }

        public void SetScale(float scale)
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }
        
        public float GetScale()
        {
            return transform.localScale.x;
        }

        public void Flip(bool animate)
        {
            if (!IsFaceUp)
                View.Flip(animate, CardProfile.FaceSprite);
            else
                View.Flip(animate, View.BackSprite);

            isFaceUp = !IsFaceUp;
        }

        [ContextMenu("Flip Test")]
        public void FlipTest()
        {
            Flip(animate: true);
        }
    }
}