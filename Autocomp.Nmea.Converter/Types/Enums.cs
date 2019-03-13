namespace Autocomp.Nmea.Converter.Types
{
    public enum Header
    {
        GLL,
        MWV
    }

    public enum Talker
    {
        AG,
        AP,
        AI,
        CD,
        CR,
        CS,
        CT,
        CV,
        CX,
        DE,
        DF,
        EC,
        EI,
        EP,
        ER,
        GL,
        GN,
        GP,
        HC,
        HE,
        HN,
        II,
        IN,
        LC,
        P,
        RA,
        SD,
        SN,
        SS,
        TI,
        VD,
        VM,
        VW,
        VR,
        YX,
        ZA,
        ZC,
        ZQ,
        ZV,
        WI
    }

    public enum DataValid
    {
        Valid='A',
        Invalid='V'
    }

    public enum ModeIndicator
    {
        Autonomous = 'A',
        Differential = 'D',
        Estimated='E',
        ManualInput='M',
        Simulator='S',
        DataNotValid='N'
    }

    public enum LongitudeDirection
    {
        E,
        W
    }

    public enum LatitudeDirection
    {
        N,
        S
    }

    public enum WindReference
    {
        Relative='R',
        Theoretical='T'
    }

    public enum WindSpeedUnits
    {
        KmPerHour='K',
        MetersPerSecond='M',
        Knots='N',
        MilesPerHour='S'
    }
}
