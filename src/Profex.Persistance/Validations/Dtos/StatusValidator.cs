namespace Profex.Persistance.Validations.Dtos
{
    public class StatusValidator
    {
        public static bool isValid(bool status)
        {
            if (status ==false) return false;
            else return true;
        }
    }
}
