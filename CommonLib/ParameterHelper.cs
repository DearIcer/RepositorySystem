namespace CommonLib
{
    public class ParameterHelper
    {
        public static bool ValidateParameter(string parameter, string paramName, out string msg)
        {
            if (string.IsNullOrWhiteSpace(parameter))
            {
                msg = "参数错误，" + paramName + "为空";
                return false;
            }

            msg = null;
            return true;
        }
    }
}
