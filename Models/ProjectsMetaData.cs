using System.ComponentModel;

namespace NLMK.Models;

public class ProjectsMetaData
{
    public enum Types
    {
        Technology = 1,
        Electric = 2,
        Building = 3,
    }
    public enum Stages
    {
        OPR = 1,
        PPR = 2,
        PD = 3,
        RD = 4
    }

    public static string Localize(Stages stage)
    {
        switch (stage)
        {
            case Stages.OPR:
            {
                return "ОПР";
            }
            case Stages.PD:
            {
                return "ПД";
            }
            case Stages.PPR:
            {
                return "ППР";
            }
            case Stages.RD:
            {
                return "РД";
            }
            default:
            {
                return "Undefined";
            }
        }
    }
    public static string Localize(Types type)
    {
        switch (type)
        {
            case Types.Electric:
            {
                return "Электрика";
            }
            case Types.Building:
            {
                return "Строительство";
            }
            case Types.Technology:
            {
                return "Технология";
            }
            default:
            {
                return "Undefined";
            }
        }
    }
}