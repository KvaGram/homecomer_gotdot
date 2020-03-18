using Godot;
using System;
using System.Collections.Generic;
/*
    Homecomer is the main class of the Homecomer game, and holds all main data of the game.
    It must always be loaded first, as any gameplay component relies on this being accessable.
*/
public class Homecomer : Godot.Node
{
    private static Homecomer instance;
    public const int MODULES_PER_SECTION = 6;
    public const int NUMBER_OF_SECTIONS = 5;

    public const int FULL_SHIP_SIZE = MODULES_PER_SECTION * NUMBER_OF_SECTIONS;

    //List of modules on the ship
    public ShipModule[] shipModules;
    //List of crewmen onboard the ship
    //NOTE: Should be sorted first by CrewAge then by ID.
    public List<Crewman> shipCrew;

    private int gameMonth;


    public Node cScene {get; set;}

    public override void _Ready()
    {
        Viewport root = GetTree().Root;
        cScene = root.GetChild(root.GetChildCount() - 1);
        GD.Print("Hello Homecomer!!!"); //printout to confirm node is active.

        //set c# singleton instance access (more direct than Godot singleton system)
        instance = this;

        gameMonth = 0;

        shipModules = new ShipModule[FULL_SHIP_SIZE];
        for(int i = 0; i < FULL_SHIP_SIZE; i++)
        {
            //the module value is properly corrected in the constructor. No need to calculate and correct it twice.
            ShipLocation loc = new ShipLocation(i, i / MODULES_PER_SECTION);
            shipModules[i] = new ShipModule(loc, ModuleType.empty);
        }

        shipCrew = new List<Crewman>();

        Random r = new Random(0);

        //Adding initial crew. Note birthdate is in months before game start. A birthdate of -360 equals means age 30 years at game turn 0.
        shipCrew.Add(new Crewman("Hugh Frost", CrewGender.male, -381, CrewAge.adult)
        .generateSkills(r, 60, 20, 10, 50, new CrewSkill[]{CrewSkill.socialization, CrewSkill.navigation})
        );
        
        shipCrew.Add(new Crewman("Zach Frost", CrewGender.male, -360, CrewAge.adult)
        .generateSkills(r, 50, 20, 10, 50, new CrewSkill[]{CrewSkill.artifice, CrewSkill.doctoring})
        );

        shipCrew.Add(new Crewman("Cole Rowe", CrewGender.male, -570, CrewAge.adult)
        .generateSkills(r, 60, 20, 10, 30, new CrewSkill[]{CrewSkill.combat, CrewSkill.weaponry})
        );

        shipCrew.Add(new Crewman("George Hawkins", CrewGender.male, -312, CrewAge.adult)
        .generateSkills(r, 60, 20, 10, 50, new CrewSkill[]{CrewSkill.socialization, CrewSkill.doctoring})
        );

        shipCrew.Add(new Crewman("Eden Day", CrewGender.female, -408, CrewAge.adult)
        .generateSkills(r, 60, 20, 10, 50, new CrewSkill[]{CrewSkill.artifice})
        );

        shipCrew.Add(new Crewman("Rosie Connor", CrewGender.male, -308, CrewAge.adult)
        .generateSkills(r, 60, 20, 10, 50, new CrewSkill[]{CrewSkill.navigation})
        );

        shipCrew.Add(new Crewman("Jessie Marshall", CrewGender.female, -400, CrewAge.adult)
        .generateSkills(r, 60, 20, 10, 30, new CrewSkill[]{CrewSkill.navigation})
        );

        shipCrew.Add(new Crewman("Ciara Palmer", CrewGender.female, -302, CrewAge.adult)
        .generateSkills(r, 60, 20, 10, 50, new CrewSkill[]{CrewSkill.artifice})
        );

        shipCrew.Add(new Crewman("Alicia Hatcher", CrewGender.female, -320, CrewAge.adult)
        .generateSkills(r, 60, 20, 10, 50, new CrewSkill[]{CrewSkill.weaponry})
        );
        
        shipCrew.Add(new Crewman("Norma López", CrewGender.female, -432, CrewAge.adult)
        .generateSkills(r, 60, 20, 10, 50, new CrewSkill[]{CrewSkill.socialization, CrewSkill.combat})
        );

        int[] foo = {1<<0, 1<<1, 1<<2, 1<<3, 1<<4, 1<<5};
        foreach(int f in foo)
            GD.Print(f);

    }

    public void GotoScene(string path)
    {
        // This function will usually be called from a signal callback,
        // or some other function from the current scene.
        // Deleting the current scene at this point is
        // a bad idea, because it may still be executing code.
        // This will result in a crash or unexpected behavior.

        // The solution is to defer the load to a later time, when
        // we can be sure that no code from the current scene is running:

        CallDeferred(nameof(DeferredGotoScene), path);
    }

    public void DeferredGotoScene(string path)
    {
        // It is now safe to remove the current scene
        cScene.Free();

        // Load a new scene.
        var nextScene = (PackedScene)GD.Load(path);

        // Instance the new scene.
        cScene = nextScene.Instance();

        // Add it to the active scene, as child of root.
        GetTree().Root.AddChild(cScene);

        // Optionally, to make it compatible with the SceneTree.change_scene() API.
        GetTree().CurrentScene = cScene;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public static Homecomer Instance { get => instance;}
    public static int GameMonth { get => Instance.gameMonth;}

    public static List<ModuleJob> getAllJobs(int crewID, Jobtype? type)
    {
        List<ModuleJob> list = new List<ModuleJob>(5); //initiating list with a best-guess of up to 5 hits
        foreach (ShipModule m in instance.shipModules)
            m.getAllJobs(crewID, ref list, type);
        return list;
    }
    public static List<ModuleService> getAllServices(int crewID, Servicetype? type)
    {
        List<ModuleService> list = new List<ModuleService>(5); //initiating list with a best-guess of up to 5 hits
        foreach (ShipModule m in instance.shipModules)
            m.getAllServices(crewID, ref list, type);
        return list;
    }

    public static void removeFromJobs(int crewID, Jobtype? type)
    {
        foreach (ShipModule m in instance.shipModules)
            m.removeFromJobs(crewID, type);
    }

    public static void removeFromServices(int crewID, Servicetype? type)
    {
        foreach (ShipModule m in instance.shipModules)
            m.removeFromServices(crewID, type);
    }
    internal static ShipModule getModule(int sectionLocation, int moduleLocation)
    {
        int index = sectionLocation * MODULES_PER_SECTION + moduleLocation;
        return instance.shipModules[index];
    }

    /*
    GetAllJobs(crewID) //gets all job assignments this crewman has
    GetAllJobs(crewID, jobType)//above, but filters for selected jobType

    GetAllServices(crewID) //gets all service assignments this crewman has
    GetAllServices(crewID, serviceType)//above, but filters for selected serviceType

    RemoveFromJobs(crewID) //Unassignes crewman from every job
    RemoveFromJobs(crewID, jobType) //Unassigns crewman from every job of selected type

    RemoveFromServices(crewID) //Removes crewman from every services (NOTE: this also makes them homeless!)
    RemoveFromServices(crewID, ServiceType) //Removes crewman from all services of type.
    */

}
