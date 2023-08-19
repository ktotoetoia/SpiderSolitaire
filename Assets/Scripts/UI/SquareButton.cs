using UnityEngine.UIElements;

public class SquareButton : Button
{
    [UnityEngine.Scripting.Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        private readonly UxmlFloatAttributeDescription scale =
            new() { name = "scale", defaultValue = 1f };
        private readonly UxmlEnumAttributeDescription<SliderDirection> keepAspectDirection =
            new() { name = "keep-direction", defaultValue = SliderDirection.Horizontal };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);

            SquareButton element = ve as SquareButton;
            
            element.Scale = scale.GetValueFromBag(bag, cc);
            element.KeepDirection = keepAspectDirection.GetValueFromBag(bag, cc);

            element.FitToParent();
        }
    }

    [UnityEngine.Scripting.Preserve]
    public class SaveAspectButtonFactory : UxmlFactory<SquareButton, UxmlTraits>
    {

    }

    public float Scale { get; private set; } = 10;
    public SliderDirection KeepDirection { get; set; }

    public SquareButton()
    {
        RegisterCallback<AttachToPanelEvent>(OnAttachToPanelEvent);
    }

    private void OnAttachToPanelEvent(AttachToPanelEvent e)
    {
        parent?.RegisterCallback<GeometryChangedEvent>(OnGeometryChangedEvent);
        FitToParent();
    }

    private void OnGeometryChangedEvent(GeometryChangedEvent e)
    {
        FitToParent();
    }

    private void FitToParent()
    {
        VisualElement element = parent;

        if (element == null)
        {
            return;
        }

        FitToElement(element);
    }

    private void FitToElement(VisualElement element)
    {
        float aspect = KeepDirection == SliderDirection.Horizontal ? element.resolvedStyle.width : element.resolvedStyle.height;
        float size = aspect * Scale / 100;

        style.width = size;
        style.height = size;
    }
}