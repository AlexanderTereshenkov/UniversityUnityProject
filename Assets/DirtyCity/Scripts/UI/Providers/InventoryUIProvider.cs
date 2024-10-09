
public class InventoryUIProvider
{
    private MapInventoryPage _mapInventoryPage;
    private Inventory _inventory;

    public InventoryUIProvider(MapInventoryPage mapInventoryPage, Inventory inventory)
    {
        _mapInventoryPage = mapInventoryPage;
        _inventory = inventory;

        _inventory.OnFilterChanged += SetFiltersAmount;
        _inventory.OnObjectPicked += SetInteractibleObject;
    }

    private void SetFiltersAmount(int amount)
    {
        _mapInventoryPage.SetFiltersAmount(amount);
    }

    private void SetInteractibleObject(IInteractible interactible)
    {
        _mapInventoryPage.SetInteractibleObject(interactible);
    }

    ~InventoryUIProvider()
    {
        _inventory.OnFilterChanged -= SetFiltersAmount;
        _inventory.OnObjectPicked -= SetInteractibleObject;
    }


}
