using UnityEngine;
using UnityEngine.EventSystems; // N�cessaire pour les �v�nements de Drag & Drop



public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform originalParent;

    // Appel� quand le drag commence
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        originalParent = transform.parent;
        // Vous pouvez ajouter ici d'autres actions, comme changer l'opacit�, etc.
    }

    // Appel� pendant le drag
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // Ou une autre logique pour suivre le curseur
    }

    // Appel� quand le drag se termine
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition; // Replace l'objet ou utilisez une autre logique
        transform.SetParent(originalParent); // Remet l'objet dans son parent original
    }



}
