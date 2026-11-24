using UnityEngine;

public struct InteractionContext
{
    public IInteractable Target;
    public FieldData TargetField;
    public HotbarItemSO EquippedItem;
    public Vector3 HitPoint;
    public Vector3 HitNormal;

    public InteractionContext(IInteractable target, FieldData targetField, HotbarItemSO equippedItem, Vector3 hitPoint, Vector3 hitNormal)
    {
        Target = target;
        TargetField = targetField;
        EquippedItem = equippedItem;
        HitPoint = hitPoint;
        HitNormal = hitNormal;
    }
}

public interface IInteractable
{
    string TargetName { get; }
    FieldData ParentField { get; }
    bool CanInteract(HotbarItemSO equippedItem);
    string GetPrompt(HotbarItemSO equippedItem);
    void Interact(InteractionContext context);
}