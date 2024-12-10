using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class HitPopUp : MonoBehaviour
{
    public RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.DOAnchorPos(new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 120), 0.25f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            GetComponent<TMP_Text>().DOFade(0, 0.25f);
            rectTransform.DOScale(Vector2.zero, 1.5f).OnComplete(() => { Destroy(gameObject); });
        });
    }
}
