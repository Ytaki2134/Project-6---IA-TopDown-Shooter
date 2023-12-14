using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems; // Pour les �v�nements de la souris


public class DraggableManipulator : MouseManipulator
{
    private bool isDragging = false;
    private Vector2 startMousePosition;
    private float startElementPosition;

    public DraggableManipulator()
    {
        activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        target.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
    }

    private void OnMouseDown(MouseDownEvent e)
    {
        if (e.button == (int)MouseButton.LeftMouse)
        {
            isDragging = true;
            startMousePosition = e.mousePosition;
            startElementPosition = target.resolvedStyle.left;
            target.CaptureMouse();
            e.StopPropagation();
        }
    }

    private void OnMouseMove(MouseMoveEvent e)
    {
        if (isDragging)
        {
            float delta = e.mousePosition.x - startMousePosition.x;
            target.style.left = new Length(startElementPosition +delta); // Corrig� ici
        }
    }

    private void OnMouseUp(MouseUpEvent e)
    {
        if (e.button == (int)MouseButton.LeftMouse && isDragging)
        {
            isDragging = false;
            target.ReleaseMouse();
            e.StopPropagation();
        }

        // � la fin du drag, mettez � jour le GameObject dans la sc�ne
        if (isDragging)
        {
            // Votre logique pour placer le GameObject dans la sc�ne
            var gameObject = (target.userData as GameObject);
            if (gameObject != null)
            {
                // Mettre � jour le Transform du GameObject ici, si n�cessaire
            }

            isDragging = false;
        }
    }
}
