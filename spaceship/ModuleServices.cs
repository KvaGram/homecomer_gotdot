using System.Collections.Generic;
public class ModuleService
{
    private readonly ShipModule parent;
    public readonly Servicetype type;

    public List<ServiceAssignment> assignments;
    private readonly int numSlots;

    public ModuleService(ShipModule parent, Servicetype type, int numSlots)
    {
        this.numSlots = numSlots;
        this.parent = parent;
        this.type = type;
        assignments = new List<ServiceAssignment>(numSlots);
    }

    public bool isAssigned(int crewID)
    {
        foreach (ServiceAssignment s in assignments)
            if (s.crewID == crewID)
                return true;
        return false;
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
            message = "The service is full";
            return false;
        }

        message = "The crewman can be assigned.";
        return true;
    }
    public bool tryAssign(int crewID, out string message)
    {
        if (!canAssign(crewID, out message))
            return false;
        assignments.Add(new ServiceAssignment(crewID));
        message = "Crewman was added!";
        return true;
    }

    public ServiceAssignment? getAssignment(int crewID)
    {
        foreach (ServiceAssignment s in assignments)
            if (s.crewID == crewID)
                return s;
        return null;
    }
    //Remove crewman from service, if assigned.
    public void removeCrewman(int crewID)
    {
        foreach (ServiceAssignment s in assignments)
        {
            if (s.crewID == crewID)
            {
                assignments.Remove(s);
                return;
            }
        }
    }
}
