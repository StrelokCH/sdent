public class Rocket
{
    private int body = 1;
    private int wings = 1;
    private int flame = 1;

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

    public override string ToString()
    {
        return "Rocket { Body: " + body + "– Wings: " + wings + "– Flame: " + flame + " }";
    }
}