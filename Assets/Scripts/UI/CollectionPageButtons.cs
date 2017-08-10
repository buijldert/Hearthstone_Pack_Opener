using System.Collections.Generic;
using UnityEngine;

public class CollectionPageButtons : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _pages;

    [SerializeField]
    private int _currentPage = 0;

    private Animator _pageAnimator;

	public void NextPage()
    {
        if (_currentPage != _pages.Count - 1)
        {
            _pageAnimator = _pages[_currentPage].GetComponent<Animator>();
            _pageAnimator.SetBool("IsTurningForward", true);
            _currentPage += 1;
        }   
    }

    public void PreviousPage()
    {
        if(_currentPage != 0)
        {
            _currentPage -= 1;
            _pageAnimator = _pages[_currentPage].GetComponent<Animator>();
            _pageAnimator.SetBool("IsTurningForward", false);
        }
    }
}