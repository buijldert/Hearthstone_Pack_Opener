using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableLoadingAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftBG, _rightBG;
	// Use this for initialization
	void Start ()
    {
        _leftBG.SetActive(true);
        _rightBG.SetActive(true);
	}
}
