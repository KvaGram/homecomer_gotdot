public struct ShipLocation
{
    private int s, m;

    public ShipLocation(int section, int module)
    {
        s = section;
        m = module;

        //roll section untill valid.
        while(s >= Homecomer.NUMBER_OF_SECTIONS)
            s-= Homecomer.NUMBER_OF_SECTIONS;
        while(s < 0)
            s+= Homecomer.NUMBER_OF_SECTIONS;
        //roll module untill valid.
        while(m >= Homecomer.MODULES_PER_SECTION)
            m-= Homecomer.MODULES_PER_SECTION;
        while(m < 0)
            m+= Homecomer.MODULES_PER_SECTION;
    }
    public ShipLocation getNextModule() {
        return new ShipLocation(s, m+1);
    }
    public ShipLocation getPrevModule() {
        return new ShipLocation(s, m-1);
    }
    public ShipLocation getNextSection() {
        return new ShipLocation(s+1, m);
    }
    public ShipLocation getPrevSection() {
        return new ShipLocation(s-1, m);
    }

    //Section location. Where along the spine a module is located.
    public int S { get => s;}
    
    //Module location. Where around the spine a module is located.
    public int M { get => m;}

    public int ToIndex()
    {
        return m + s * Homecomer.MODULES_PER_SECTION;
    }
    public static ShipLocation FromIndex(int index)
    {
        return new ShipLocation(index / Homecomer.MODULES_PER_SECTION, index % Homecomer.MODULES_PER_SECTION);
    }

    public override string ToString()
    {
        return "(S " + s + ", M " + m + ")";
    }  
    
}