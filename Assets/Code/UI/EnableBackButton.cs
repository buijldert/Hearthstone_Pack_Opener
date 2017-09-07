using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBackButton : MonoBehaviour
{
    private BackButton _backButton;

    private void Start()
    {
        _backButton = GameObject.FindWithTag(Tags.SINGLETONTAG).GetComponent<BackButton>();
    }

    public void EnableBButton()
    {
        _backButton.enabled = true;
    }
}
