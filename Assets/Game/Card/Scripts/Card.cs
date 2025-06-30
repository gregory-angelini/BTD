using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Card : MonoBehaviour
    {
        [SerializeField] CardView view;
        [SerializeField] float speedUnitsPerSecond = 10f;
        CardProfile cardProfile;
        Tween moveTween;

        public Suit Suit { get => cardProfile.Suit; }
        public Rank Rank { get => cardProfile.Rank; }


        public void Setup(CardProfile cardProfile)
        {
            this.cardProfile = cardProfile;

            view.SetSprite(cardProfile.FaceSprite);
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

        public void Move(Vector2 target, bool animate)
        {
            if (animate)
            {
                moveTween?.Kill();

                float distance = Vector3.Distance(GetPosition(), target);
                float duration = distance / speedUnitsPerSecond;

                moveTween = transform
                    .DOLocalMove(target, duration)
                    .SetEase(Ease.OutQuad);
            }
            else
            {
                transform.localPosition = target;
            }
        }
    }
}