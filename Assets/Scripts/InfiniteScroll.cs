//Libraries..
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * This script is responsible for infinite scroll mechanism of production menu.
 * 
 */

public class InfiniteScroll : MonoBehaviour
{
    private ScrollRect _scrollRect;
    private ContentSizeFitter _contentSizeFitter;
    private VerticalLayoutGroup _verticalLayoutGroup;
    protected bool _isVertical = false;
    private float _disableMarginY = 0;
    private bool _hasDisabledGridComponents = false;
    protected List<RectTransform> items = new List<RectTransform>();
    private Vector2 _newAnchoredPosition = Vector2.zero;

    //TO DISABLE FLICKERING OBJECT WHEN SCROLL VIEW IS IDLE IN BETWEEN OBJECTS
    private float _threshold = 100f;

    private int _itemCount = 0;
    private float _recordOffsetY = 0;

    private void Start()
    {
        Init();
    }

    private void SetItems()
    {
        for (int i = 0; i < _scrollRect.content.childCount; i++)
        {
            items.Add(_scrollRect.content.GetChild(i).GetComponent<RectTransform>());
        }

        _itemCount = _scrollRect.content.childCount;
    }

    private void Init()
    {
        if (GetComponent<ScrollRect>() != null)
        {
            _scrollRect = GetComponent<ScrollRect>();
            _scrollRect.onValueChanged.AddListener(OnScroll);
            _scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

            if (_scrollRect.content.GetComponent<VerticalLayoutGroup>() != null)
            {
                _verticalLayoutGroup = _scrollRect.content.GetComponent<VerticalLayoutGroup>();
            }

            if (_scrollRect.content.GetComponent<ContentSizeFitter>() != null)
            {
                _contentSizeFitter = _scrollRect.content.GetComponent<ContentSizeFitter>();
            }

            _isVertical = _scrollRect.vertical;

            SetItems();
        }
    }

    void DisableGridComponents()
    {
        if (_isVertical)
        {
            _recordOffsetY = items[1].GetComponent<RectTransform>().anchoredPosition.y - items[0].GetComponent<RectTransform>().anchoredPosition.y;
            if (_recordOffsetY < 0)
            {
                _recordOffsetY *= -1;
            }
            _disableMarginY = _recordOffsetY * _itemCount / 2;
        }

        if (_verticalLayoutGroup)
        {
            _verticalLayoutGroup.enabled = false;
        }
        if (_contentSizeFitter)
        {
            _contentSizeFitter.enabled = false;
        }
        _hasDisabledGridComponents = true;
    }

    public void OnScroll(Vector2 pos)
    {
        if (!_hasDisabledGridComponents)
            DisableGridComponents();

        for (int i = 0; i < items.Count; i++)
        {
            if (_isVertical)
            {
                if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).y > _disableMarginY + _threshold)
                {
                    _newAnchoredPosition = items[i].anchoredPosition;
                    _newAnchoredPosition.y -= _itemCount * _recordOffsetY;
                    items[i].anchoredPosition = _newAnchoredPosition;
                    _scrollRect.content.GetChild(_itemCount - 1).transform.SetAsFirstSibling();
                }
                else if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).y < -_disableMarginY)
                {
                    _newAnchoredPosition = items[i].anchoredPosition;
                    _newAnchoredPosition.y += _itemCount * _recordOffsetY;
                    items[i].anchoredPosition = _newAnchoredPosition;
                    _scrollRect.content.GetChild(0).transform.SetAsLastSibling();
                }
            }
        }
    }
}