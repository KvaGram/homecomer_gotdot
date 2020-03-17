public struct ServiceAssignment
{
    //Refrence to the crewman assigned. This variable will be queried by search functions.
    public readonly int crewID;

    public ServiceAssignment(int crewID)
    {
        this.crewID = crewID;
    }
}