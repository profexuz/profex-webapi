namespace Profex.Persistance.Validations.Dtos;

public class LongitudeValidator
{
    public static bool IsValid(double longitude)
    {
        if (longitude < 0) return false;
        else return true;
    }
}
