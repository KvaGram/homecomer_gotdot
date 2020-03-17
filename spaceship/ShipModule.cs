using System.Collections.Generic;
public class ShipModule
{
    private ShipLocation loc;
    private ModuleType moduleType;

    private ModuleJob[] jobs;
    private ModuleFeature[] features;
    private ModuleService[] services;

    public ShipModule(ShipLocation loc, ModuleType empty)
    {
        this.loc = loc;
        this.moduleType = empty;
        //TODO: setup module type
    }
    public ShipLocation Loc { get => loc;}
    public ModuleType ModuleType { get => moduleType;}
    
    public ModuleJob[] Jobs { get => jobs;}
    public ModuleFeature[] Features { get => features;}
    public ModuleService[] Services { get => services;}

    //Searches for jobs selected crew is assignd to, and appends them to list.
    //optional filter for job type (null to get any type)
    public void getAllJobs(int crewID, ref List<ModuleJob> list, Jobtype? type)
    {
        foreach(ModuleJob j in jobs)
            if(j.isAssigned(crewID) && (!type.HasValue || j.type == type.Value))
                list.Add(j);
    }
    //Removes selected crewman from jobs.
    //optional filter for job type (null to remove from all types)
    public void removeFromJobs(int crewID, Jobtype? type)
    {
        foreach(ModuleJob j in jobs)
            if(!type.HasValue || j.type == type.Value)
                j.removeCrewman(crewID);
    }

    //Searches for services selected crew is assignd to, and appends them to list.
    //optional filter for service type (null to get any type)
    public void getAllServices(int crewID, ref List<ModuleService> list, Servicetype? type)
    {
        foreach(ModuleService s in services)
            if(s.isAssigned(crewID) && (!type.HasValue || s.type == type.Value))
                list.Add(s);
    }
    //Removes selected crewman from services.
    //optional filter for service type (null to remove from all types)
    public void removeFromServices(int crewID, Servicetype? type)
    {
        foreach(ModuleService s in services)
            if(!type.HasValue || s.type == type.Value)
                s.removeCrewman(crewID);
    }
}