using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionPageButtons : MonoBehaviour
{
    public List<GameObject> _pages = new List<GameObject>();

    [SerializeField]
    private GameObject _nextButton;
    private Animator _nextButtonAnimator;
    [SerializeField]
    private GameObject _previousButton;
    private Animator _previousButtonAnimator;

    [SerializeField]
    private int _currentPage = 0;

    private RotatePage rotatePage;

    private bool _isJustStarted = true;

    private void Start()
    {
        PoolCollection.OnPoolCollection += PoolPages;
        _nextButtonAnimator = _nextButton.GetComponent<Animator>();
        _previousButtonAnimator = _previousButton.GetComponent<Animator>();
        _isJustStarted = true;
        _previousButton.SetActive(false);
        PageChange();
    }

    public void NextPage()
    {
        if (_currentPage != _pages.Count - 1)
        {
            rotatePage = _pages[_currentPage].GetComponent<RotatePage>();
            rotatePage.FlipPage();
            _currentPage += 1;
            if(!_previousButton.activeSelf)
            {
                _previousButton.SetActive(true);
            }
            PageChange();
        }
    }

    public void PreviousPage()
    {
        if(_currentPage != 0)
        {
            _currentPage -= 1;
            rotatePage = _pages[_currentPage].GetComponent<RotatePage>();
            rotatePage.FlipPageBack();
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

    private void PoolPages()
    {
        StartCoroutine(DelayPooling());
    }

    private IEnumerator DelayPooling()
    {
        yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < _pages.Count; i++)
        {
            ObjectPool.Instance.PoolObject(_pages[i]);
        }
    }

    private void OnDisable()
    {
        PoolCollection.OnPoolCollection -= PoolPages;
    }
}