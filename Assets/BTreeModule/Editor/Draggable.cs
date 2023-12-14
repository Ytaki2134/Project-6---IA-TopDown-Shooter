using UnityEngine;
using UnityEngine.EventSystems; // Nécessaire pour les événements de Drag & Drop



public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform originalParent;

    // Appelé quand le drag commence
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        originalParent = transform.parent;
        // Vous pouvez ajouter ici d'autres actions, comme changer l'opacité, etc.
    }

    // Appelé pendant le drag
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // Ou une autre logique pour suivre le curseur
    }

    // Appelé quand le drag se termine
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition; // Replace l'objet ou utilisez une autre logique
        transform.SetParent(originalParent); // Remet l'objet dans son parent original
    }



}
