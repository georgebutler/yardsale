using UnityEngine;

[DisallowMultipleComponent]
public class WorldItem : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interact");
    }

    public void OnFocus()
    {
        Debug.Log("OnFocus");
    }

    public void OnFocusLost()
    {
        Debug.Log("OnFocusLost");
    }
}
