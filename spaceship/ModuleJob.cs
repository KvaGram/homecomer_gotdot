using System.Collections.Generic;
public class ModuleJob
{
    private readonly ShipModule parent;
    public readonly Jobtype type;

    public List<JobAssignment> assignments;
    private readonly int numSlots;

    public ModuleJob(ShipModule parent, Jobtype type, int numSlots)
    {
        this.numSlots = numSlots;
        this.parent = parent;
        this.type = type;
        assignments = new List<JobAssignment>(numSlots);
    }

    public bool canAssign(int crewID, out string message)
    {
        if (isAssigned(crewID))
        {
            message = "This crewman is already assigned here";
            return false;
        }
        if (assignments.Count + 1 >= numSlots)
        {
            message = "The workplace is full";
            return false;
        }

        message = "The crewman can be assigned.";
        return true;
    }
    public bool tryAssign(int crewID, int worksharePoints, out string message) {
        if(!canAssign(crewID, out message))
            return false;
        assignments.Add(new JobAssignment(crewID, worksharePoints));
        message = "Crewman was added!";
        return true;
    }

    public bool isAssigned(int crewID)
    {
        foreach (JobAssignment j in assignments)
            if (j.crewID == crewID)
                return true;
        return false;
    }
    public JobAssignment? getAssignment(int crewID)
    {
        foreach (JobAssignment j in assignments)
            if (j.crewID == crewID)
                return j;
        return null;
    }

    //Remove crewman from job, if assigned.
    public void removeCrewman(int crewID)
    {
        foreach (JobAssignment j in assignments)
        {
            if (j.crewID == crewID)
            {
                assignments.Remove(j);
                return;
            }
        }
    }
}
