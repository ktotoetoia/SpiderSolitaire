using UnityEngine.UIElements;

public class SquareButton : Button
{
    [UnityEngine.Scripting.Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        readonly UxmlFloatAttributeDescription scale =
            new() { name = "scale", defaultValue = 1f };
        readonly UxmlEnumAttributeDescription<SliderDirection> keepAspectDirection =
            new() { name = "keep-direction", defaultValue = SliderDirection.Horizontal };
        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var element = ve as SquareButton;

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

    void OnAttachToPanelEvent(AttachToPanelEvent e)
    {
        parent?.RegisterCallback<GeometryChangedEvent>(OnGeometryChangedEvent);
        FitToParent();
    }

    void OnGeometryChangedEvent(GeometryChangedEvent e)
    {
        FitToParent();
    }

    void FitToParent()
    {
        if (parent == null)
        {
            return;
        }

        var aspect = KeepDirection == SliderDirection.Horizontal ? parent.resolvedStyle.width : parent.resolvedStyle.height;

        float size = aspect * Scale / 100;

        style.width = size;
        style.height = size;
    }
}