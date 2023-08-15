namespace Profex.Persistance.Validations.Dtos;

public class LatiduteValidator
{
    public static bool IsValid(double latitude)
    {
        if (latitude < 0) return false;
        else return true;
    }
}
