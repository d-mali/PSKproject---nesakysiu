using EventDomain.Contracts.Responses;

namespace Frontas
{
    public class GlobalParameters
    {
        public static string apiUrl = "https://localhost:7084/api";

        public static EmployeeResponse? UserInfoGlobal { get; private set; }

        public static void SetUserInfoGlobal(EmployeeResponse userInfo)
        {
            UserInfoGlobal = userInfo;
        }
    }
}
