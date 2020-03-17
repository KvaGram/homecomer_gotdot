public enum ModuleType
{
    empty, //An empty module, providing nothing, demanding nothing.
    habitat, //The habitat provides housing for the crew,  requires nursing job to function for toddlers and infants.
    atrium, //The Atrium provides relaxation for the crew, requires gardening job to function
    hydroponics, //The hydroponics produce food for the crew, requires farming job to function
    morge, //The Morge is a more respectable place to store the dead, redusing morale penalties from death
    clinic, //Provides healthcare for the crew, redusing morale penalties and chance of death (!) from injuries
    rec_area, //Provides stress outlet / relaxation for crew
    soc_area, //Provides a place to eat and gather for news and entertainment. Provides relaxation, requires cook  job to function
    cargo_area, //Provides a place to store cargo.
    workshop, //Provides a place to fabricate parts and organize construction and repairs
    armoury, //Provides a place to store and train with hand-held weaponry for use in combat
    laboratory, //Provides a place to study and learn the mysteries of space
    library, //Provides learning resources for the crew.
    hangar, //Provides space to hold shuttlecrafts to use in mining and foraging missions
}