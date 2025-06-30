using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] Image cardImage;
        [SerializeField] VisualSettingsProfile visualSettings;
        Tween moveTween;
        public Sprite FaceSprite { get; private set; }
        public Sprite BackSprite { get; private set; }


        public void SetFaceSprite(Sprite faceSprite)
        {
            FaceSprite = faceSprite;
        }

        public void SetBackSprite(Sprite backSprite)
        {
            BackSprite = backSprite;
        }
            
        public void SetSprite(Sprite sprite)
        {
            cardImage.sprite = sprite;
            cardImage.SetNativeSize();
        }

        public void Move(Vector2 target, bool animate, Action onComplete)
        {
            if (animate)
            {
                if (moveTween != null)
                {
                    if (moveTween.IsActive() && !moveTween.IsComplete())
                    {
                        moveTween.Complete();
                    }
                }

                float distance = Vector3.Distance(
                    transform.localPosition, 
                    new Vector3(target.x, target.y, transform.localPosition.z));

                float duration = distance / visualSettings.CardMoveSpeed;

                moveTween = transform
                    .DOLocalMove(target, duration)
                    .SetEase(visualSettings.CardMoveEase)
                    .OnComplete(() => onComplete?.Invoke());
            }
            else
            {
                transform.localPosition = target;
                onComplete?.Invoke();
            }
        }
        
        public void Flip(bool animate, Sprite sprite)
        {
            animate = false;

            if (animate)
            {
                float halfDuration = visualSettings.CardFlipDuration / 2f;

                transform.DOLocalRotate(new Vector3(0f, 90f, 0f), halfDuration)
                    .SetEase(visualSettings.CardFlipEase)
                    .OnComplete(() =>
                    {
                        cardImage.sprite = sprite;

                        transform.DOLocalRotate(new Vector3(0f, 0f, 0f), halfDuration)
                            .From(new Vector3(0f, 270f, 0f))
                            .SetEase(visualSettings.CardFlipEase);
                    });
            }
            else
            {
                cardImage.sprite = sprite;

                transform.localRotation *= Quaternion.Euler(0f, 180f, 0f);
            }
        }
    }
}
