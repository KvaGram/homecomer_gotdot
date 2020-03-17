public struct JobAssignment
{
    //Refrence to the crewman assigned. This variable will be queried by search functions.
    public readonly int crewID;
    //How much work of the workload this crewman is to contribute with.
    public int worksharePoints;

    public JobAssignment(int crewID, int worksharePoints)
    {
        this.crewID = crewID;
        this.worksharePoints = worksharePoints;
    }
}