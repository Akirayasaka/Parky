namespace ParkyWeb
{
    /// <summary>
    /// 對應API Project, API Controller Route
    /// </summary>
    public static class SD
    {
        public static string APIBaseUrl = "https://localhost:7063/";
        public static string NationalParkAPIPath = APIBaseUrl + "api/v1/nationalparks/";
        public static string TrailAPIPath = APIBaseUrl + "api/v1/trails/";
        public static string AccountAPIPath = APIBaseUrl + "api/v1/Users/";
    }
}
