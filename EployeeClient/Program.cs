using Grpc.Net.Client;
using static EmployeeServiceProto.DictionariesService;

namespace EployeeClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            DictionariesServiceClient client = new DictionariesServiceClient(channel);

            Console.WriteLine("Укажите тип сотрудника: ");

            var response = client.CreateEmployeeType(new EmployeeServiceProto.CreateEmployeeTypeRequest 
            {
                Description = Console.ReadLine()
            });

            if (response != null)
            {
                Console.WriteLine($"тип сотрудника успешно добавлен #{response.Id}");
            }

            var getAll = client.GetAllEmployeeType(new EmployeeServiceProto.GetAllEmployeeTypeRequest());
            foreach (var employeeType in getAll.EmployeeType)
            {
                Console.WriteLine($"#{employeeType.Id} / {employeeType.Description}");
            }

            Console.ReadKey(true);
        }
    }
}