using ProjectClientServer.Contexts;
using ProjectClientServer.Handler;
using ProjectClientServer.Models;
using ProjectClientServer.Repositories.Contract;
using ProjectClientServer.ViewModel;

namespace ProjectClientServer.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, string, MyContext>, IAccountRepository
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProfilingRepository _profilingRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        public AccountRepository(
            MyContext context, 
            IUniversityRepository universityRepository, 
            IEducationRepository educationRepository, 
            IEmployeeRepository employeeRepository, 
            IProfilingRepository profilingRepository, 
            IAccountRoleRepository accountRoleRepository) : base(context)
        {
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _employeeRepository = employeeRepository;
            _profilingRepository = profilingRepository;
            _accountRoleRepository = accountRoleRepository;
        }

        public async Task<bool> LoginAsync(LoginVM loginVM)
        {
            var getEmployees = await _employeeRepository.GetAllAsync();
            var getAccounts = await GetAllAsync();

            var getUserData = getEmployees.Join(getAccounts,
                                                e => e.Nik,
                                                a => a.EmployeeNik,
                                                (e, a) => new LoginVM
                                                {
                                                    Email = e.Email,
                                                    Password = a.Password
                                                })
                                          .FirstOrDefault(ud => ud.Email == loginVM.Email);

            return getUserData is not null && Hashing.ValidatePassword(loginVM.Password, getUserData.Password);
        }

        public async Task RegisterAsync(RegisterVM registerVM)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var university = new University
                {
                    Name = registerVM.UniversityName
                };
                //await _universityRepository.InsertAsync(university);

                if (await _universityRepository.IsNameExistAsync(registerVM.UniversityName))
                {

                }
                else
                {
                    await _universityRepository.InsertAsync(university);
                }

                var education = new Education
                {
                    Major = registerVM.Major,
                    Degree = registerVM.Degree,
                    Gpa = registerVM.Gpa,
                    UniversityId = university.Id,
                };
                await _educationRepository.InsertAsync(education);

                var employee = new Employee
                {
                    Nik = registerVM.Nik,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Birthdate = registerVM.BirthDate,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                    Gender = registerVM.Gender,
                    HiringDate = DateTime.Now,
                };
                await _employeeRepository.InsertAsync(employee);

                var account = new Account
                {
                    EmployeeNik = employee!.Nik,
                    Password = Hashing.HashPassword(registerVM.Password),
                };
                await InsertAsync(account);

                var profiling = new Profiling
                {
                    EmployeeNik = employee.Nik,
                    EducationId = education!.Id,
                };
                await _profilingRepository.InsertAsync(profiling);

                var accountRole = new AccountRole
                {
                    AccountNik = registerVM.Nik,
                    RoleId = 2,
                };
                await _accountRoleRepository.InsertAsync(accountRole);

                await transaction.CommitAsync();
            } catch{
                await transaction.RollbackAsync();
            }
        }
    }
}
