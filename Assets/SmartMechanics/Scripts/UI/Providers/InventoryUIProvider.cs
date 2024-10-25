
public class InventoryUIProvider
{
    private MapInventoryPage _mapInventoryPage;
    private Inventory _inventory;

    public InventoryUIProvider(MapInventoryPage mapInventoryPage, Inventory inventory)
    {
        _mapInventoryPage = mapInventoryPage;
        _inventory = inventory;

        _inventory.OnFilterChanged += SetFiltersAmount;
        _inventory.OnObjectChanged += SetInteractibleObject;
    }

    private void SetFiltersAmount(int amount)
    {
        _mapInventoryPage.SetFiltersAmount(amount);
    }

    private void SetInteractibleObject(IPickable pickable)
    {
        _mapInventoryPage.SetInteractibleObject(pickable);
    }

    ~InventoryUIProvider()
    {
        _inventory.OnFilterChanged -= SetFiltersAmount;
        _inventory.OnObjectChanged -= SetInteractibleObject;
    }


}
