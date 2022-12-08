using EmployeeService.Data;
using EmployeeService.Models.Requests;
using EmployeeServiceProto;
using Grpc.Core;
using static EmployeeServiceProto.DictionariesService;

namespace EmployeeService.Services.Impl
{
    public class DictionariesService : DictionariesServiceBase
    {
        private readonly IEmployeeTypeReposytory _employeeTypeReposytory;
        public DictionariesService(IEmployeeTypeReposytory employeeTypeReposytory)
        {
            _employeeTypeReposytory = employeeTypeReposytory;
        }

        public override Task<CreateEmployeeTypeResponse> CreateEmployeeType(CreateEmployeeTypeRequest request, ServerCallContext context)
        {
            var id = _employeeTypeReposytory.Create(new Data.EmployeeType
            {
                Description = request.Description
            });
            CreateEmployeeTypeResponse response = new CreateEmployeeTypeResponse();
            response.Id = id;
            return Task.FromResult(response);
        }

        public override Task<DeleteEmployeeTypeResponse> DeleteEmployeeType(DeleteEmployeeTypeRequest request, ServerCallContext context)
        {
            _employeeTypeReposytory.Delete(request.Id);
            return Task.FromResult(new DeleteEmployeeTypeResponse());
        }

        public override Task<GetAllEmployeeTypeResponse> GetAllEmployeeType(GetAllEmployeeTypeRequest request, ServerCallContext context)
        {
            GetAllEmployeeTypeResponse response = new GetAllEmployeeTypeResponse();
            response.EmployeeType.AddRange(_employeeTypeReposytory.GetAll().Select(et =>
                new EmployeeServiceProto.EmployeeType
                {
                    Id = et.Id,
                    Description = et.Description
                }
                ).ToList());
            return Task.FromResult(response);
        }
    }
}
