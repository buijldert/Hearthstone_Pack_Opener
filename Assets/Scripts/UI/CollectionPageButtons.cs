using System.Collections.Generic;
using UnityEngine;

public class CollectionPageButtons : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _pages;

    [SerializeField]
    private GameObject _nextButton;
    private Animator _nextButtonAnimator;
    [SerializeField]
    private GameObject _previousButton;
    private Animator _previousButtonAnimator;

    [SerializeField]
    private int _currentPage = 0;

    private Animator _pageAnimator;

    private void Start()
    {
        _nextButtonAnimator = _nextButton.GetComponent<Animator>();
        _previousButtonAnimator = _previousButton.GetComponent<Animator>();
        PageChange();
    }

    public void NextPage()
    {
        if (_currentPage != _pages.Count - 1)
        {
            _pageAnimator = _pages[_currentPage].GetComponent<Animator>();
            _pageAnimator.SetBool("IsTurningForward", true);
            _currentPage += 1;
            PageChange();
        }
    }

    public void PreviousPage()
    {
        if(_currentPage != 0)
        {
            _currentPage -= 1;
            _pageAnimator = _pages[_currentPage].GetComponent<Animator>();
            _pageAnimator.SetBool("IsTurningForward", false);
            PageChange();
        }
    }

    private void PageChange()
    {
        if (_currentPage < _pages.Count - 1)
        {

            _nextButtonAnimator.SetBool("FadeIn", true);
        }
        else
        {
            _nextButtonAnimator.SetBool("FadeIn", false);
        }

        if (_currentPage > 0)
        {
            _previousButtonAnimator.SetBool("FadeIn", true);
        }
        else
        {
            _previousButtonAnimator.SetBool("FadeIn", false);
        }
    }
}