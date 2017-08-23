using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    public void DisableGivenGameObject(GameObject objectToDisable)
    {
        objectToDisable.SetActive(false);
    }
}
