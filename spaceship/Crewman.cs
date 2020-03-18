using System;
using System.Linq;

public class Crewman
{
    private const int NUM_CREW_SKILL_TYPES = 6;
    private static int ID_GENERATOR = Int32.MinValue;
    public const int SKILL_CAP = 100;
    protected static int NEXT_ID {get{return ID_GENERATOR++;}}

    public readonly int ID;
    private string name;
    private byte[] skillPoints;
    private int birthdate;

    private CrewGender gender;
    private CrewAge crewAge;

    public Crewman(string name, CrewGender gender, int birthdate, CrewAge crewAge)
    {
        this.ID = NEXT_ID;

        this.name = name;
        this.gender = gender;
        this.birthdate = birthdate;
        this.crewAge = crewAge;
        skillPoints = new byte[NUM_CREW_SKILL_TYPES];
        for(int i = 0; i < skillPoints.Length; i++)
            skillPoints[i] = 50;
    }
    #region cunstructor addendums

    //These functions is meant to function as optional addendums to the constructor.
    //They are designed to be used as part of a chain, and always returns this.

    public Crewman setSkills(byte[] skillPoints){
        //NOTE: We trust and assume the byte[] is of length NUM_CREW_SKILL_TYPES.
        this.skillPoints = skillPoints;
        return this;
    }
    //generate skills randomize a skill set based on some parameters.
    public Crewman generateSkills(int r, int average, int tolerance, int fudge, int maxBiasBoon, CrewSkill[] bias)
    {
        return generateSkills(new Random(r), average, tolerance, fudge, maxBiasBoon, bias);
    }
    public Crewman generateSkills(Random r, int average, int tolerance, int fudge, int maxBiasBoon, CrewSkill[] bias)
    {
        
        if(average < 0 || average >= SKILL_CAP || tolerance < 0 || fudge < 0 || fudge > SKILL_CAP || maxBiasBoon < 0 || maxBiasBoon > SKILL_CAP)
            throw new Exception("Illegal skillset generator arguments!!");
        
        //convert skill bias to something more machine readable.
        int[] intBias = new int[bias.Length];
        for (int b = 0; b < bias.Length; b++)
            intBias[b] = (int)bias[b];

        int targetMinimalTotalPoints = (average - tolerance) * NUM_CREW_SKILL_TYPES;
        int targetMaximumTotalPoints = (average + tolerance) * NUM_CREW_SKILL_TYPES;

        int total = 0;
        for(int s = 0; s < NUM_CREW_SKILL_TYPES; s++)
        {
            if(intBias.Contains(s))
            {
                skillPoints[s] = (byte)(average + r.Next(maxBiasBoon));
            }
            else
            {
                skillPoints[s] = (byte)(average - fudge + r.Next(fudge * 2));
            }
            total += skillPoints[s];
        }
        double toAdjust = 0;
        if(total > targetMaximumTotalPoints)
            toAdjust = targetMaximumTotalPoints - total;
        else if (total < targetMinimalTotalPoints)
            toAdjust = targetMinimalTotalPoints - total;

        for (int s = 0; s < NUM_CREW_SKILL_TYPES; s++)
        {
            byte a = (byte)Math.Floor( (toAdjust / (NUM_CREW_SKILL_TYPES - s)));
            skillPoints[s] += a;
            toAdjust -= a;
        }
        return this;
    }
    #endregion

    public string Name { get => name; }
    public int Birthdate { get => birthdate;}
    public int AgeMonths { get {return Homecomer.GameMonth - birthdate;}}
    public float AgeYears{get {return AgeMonths / 12f;}}

    internal CrewAge CrewAge { get => crewAge;}
}

public enum CrewSkill
{
    socialization,//The ability to come to agreement with other, be they fellow crew, or aliens.
    navigation,//The ability to steer and navigate spacecraft, be it in sublight or warp speeds.
    weaponry,//The proficiency of using tools of destruction, to blow stuff up! Be it for mining or combat.
    combat,//The skill of fighting in a combat with hostiles. Be it close or at range with handheld weapons.
    artifice,//The skill of understanding tools, devices and other technology.
    doctoring,//The skill of healing any biological creature, be it human, alien or plants
}

//For sake of future-proofing and full on LGBT+ compadebility if needed, gender is defined with a bit-flag.
//With 2 bits to determine who can make a baby with who, and up to 30 more bits to determine any special gender flavor.
//In early prototype mechanics, the game will naively only check for for the male bit or the female bit for all uses.
public enum CrewGender {
    neuter = 0, //Biologically gender-less, cannot breed
    male = 1 << 0, //Biological male, may father children
    female = 1 << 1, //Biological female, may birth children

    //...

    //Last possible bitflag
    placeholder = 1 << 31,
    
}

//bitflags used to simulate genes.
//Gene bits are generated with half the genes of the mother, and half the genes of the father,
// or either partly or fully randomized if one or both parents are unknown.
// Then slightly randomized a bit more to allow for mutation.
// Some combinations of gene bits results in genetic traits that may be good, or very VERY bad.
//NOTE: might change to 64 bit by extending long
public enum CrewGenes {
    placeholder = 0x1,
}

public enum CrewAge {
    child = 1 << 5, //Do jobs badly, learn quickly
    adult = 1 << 10, //Default state
    dead = 1 << 31, //Crewman is dead, and exists in memory only
}