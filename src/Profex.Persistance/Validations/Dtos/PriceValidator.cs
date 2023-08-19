namespace Profex.Persistance.Validations.Dtos;

public class PriceValidator
{
    public static bool IsValid(double price)
    {
        if (price < 0)
        {
            if (price.ToString().Length > 20) return false;
            else return true;
        }
        else return true;
    }
}
