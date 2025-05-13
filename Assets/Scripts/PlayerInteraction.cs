using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerInput))]
public class PlayerInteraction : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform head;
    
    [Header("Interaction")]
    [SerializeField] private float distance = 10f;

    private IInteractable _interactionTarget;

    private void OnInteract(InputValue value)
    {
        _interactionTarget?.Interact();
    }

    private void Update()
    {
        Debug.DrawRay(head.position, head.forward * distance, Color.red, 0.1f);
    }
    
    private void FixedUpdate()
    {
        if (Physics.Raycast(head.position, head.forward, out var hit, distance))
        {
            var interactable = hit.collider.GetComponentInParent<IInteractable>();
            
            if (interactable != null)
            {
                if (_interactionTarget == interactable) return;
                _interactionTarget?.OnFocusLost();
                _interactionTarget = interactable;
                _interactionTarget.OnFocus();
                
                return;
            }
        }

        if (_interactionTarget == null) return;
        
        _interactionTarget.OnFocusLost();
        _interactionTarget = null;
    }
}