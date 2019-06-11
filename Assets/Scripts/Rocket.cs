public class Rocket
{
    private int body;
    private int wings;
    private int flame;

    public int Body
    {
        get => body;
        set => body = value;
    }

    public int Wings
    {
        get => wings;
        set => wings = value;
    }

    public int Flame
    {
        get => flame;
        set => flame = value;
    }
    public float ThrustFactor
    {
        get
        {
            switch (flame)
            {
                case 0:
                    return 1f;
                case 1:
                    return .9f;
                case 2:
                    return 1.1f;
                default:
                    goto case 0;
            }
        }
    }

    public float RotSpeedFactor
    {
        get
        {
            switch (flame)
            {
                case 0:
                    return 1f;
                case 1:
                    return 1.1f;
                case 2:
                    return .9f;
                default:
                    goto case 0;
            }
        }
    }

    public float MassFactor
    {
        get
        {
            switch (flame)
            {
                case 0:
                    return 1f;
                case 1:
                    return 1.1f;
                case 2:
                    return .9f;
                default:
                    goto case 0;
            }
        }
    }

    public override string ToString()
    {
        return "Rocket { Body: " + body + "– Wings: " + wings + "– Flame: " + flame + " }";
    }
}