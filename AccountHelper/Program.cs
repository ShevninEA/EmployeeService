namespace AccountHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(PasswordUtils.CreatePasswordHash("12345"));
            //(bl1uuFnSsU51VtqBEeWfSA==, 1ns/JVASPiHOxmW7K3ZCo6zXpeIFQfDTL01DnXluhqoNdDdZ2zuJru3RwYzOO/GPa+qUNiCVTCgxFKaz/lqHYw==)
        }
    }
}