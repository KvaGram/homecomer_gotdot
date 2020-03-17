
abstract public class ModuleFeature
{
    private readonly ShipModule parent;

    public ModuleFeature(ShipModule parent)
    {
        this.parent = parent;
    }

    //called from ShipModule::processTurn(), called every turn.
    public abstract void processTurn();

    public ShipModule Parent => parent;
}
