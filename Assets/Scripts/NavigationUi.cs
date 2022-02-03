using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NavigationUi : MonoBehaviour
{
    [SerializeField]
    private RectTransform currentView;
    [SerializeField]
    private Button buttonPrev;

    private CanvasGroup canvasGroup;
    private Stack<RectTransform> stackViews;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        stackViews = new Stack<RectTransform>();
        buttonPrev.onClick.AddListener(Pop);
    }

    public void Push(RectTransform newView)
    {
        canvasGroup.blocksRaycasts = false;
        RectTransform previousView = currentView;
        previousView.gameObject.SetActive(false);
        stackViews.Push(previousView);

        currentView = newView;
        currentView.gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;

    }

    public void Pop()
    {
        if(stackViews.Count < 1)
        {
            return;
        }

        canvasGroup.blocksRaycasts = false;

        RectTransform previousView = currentView;
        previousView.gameObject.SetActive(false);

        currentView = stackViews.Pop();
        currentView.gameObject.SetActive(true);

        canvasGroup.blocksRaycasts = true;

    }

}
