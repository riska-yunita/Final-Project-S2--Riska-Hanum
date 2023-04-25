using API.Models;
using API.ViewModels;
using API.Repository.Contracts;
using API.Context;
using API.Handlers;

namespace API.Repository.Data;

public class AccountRepository : GeneralRepository<Account, string, MyContext>, IAccountRepository
{
    private readonly IUniversityRepository _universityRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAccountRolesRepository _accountRolesRepository;
    private readonly IProfilingsRepository _profilingsRepository;
    private readonly IRoleRepository _roleRepository;


    public AccountRepository(MyContext myContext, IUniversityRepository universityRepository,
        IEducationRepository educationRepository,
        IEmployeeRepository employeeRepository,
        IAccountRolesRepository accountRolesRepository,
        IProfilingsRepository profilingsRepository,
        IRoleRepository roleRepository
        ) : base(myContext) {
        _universityRepository = universityRepository;
        _educationRepository = educationRepository;
        _employeeRepository = employeeRepository;
        _accountRolesRepository = accountRolesRepository;
        _profilingsRepository = profilingsRepository;
        _roleRepository = roleRepository;
    }  
    
    public async Task RegisterAsync(RegisterVM registerVM)
    {
        await using var transaction =  _context.Database.BeginTransaction();
        try {

            var university = await _universityRepository.InsertAsync(new University{
                Name = registerVM.UniversityName
                });

            var education = await _educationRepository.InsertAsync(new Education {
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                Gpa = registerVM.GPA,
                UniversityId = university.Id,
            });

            // Employee
            var employee = await _employeeRepository.InsertAsync (new Employee {
                Nik = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Birthdate = registerVM.BirthDate,
                Gender = registerVM.Gender,
                PhoneNumber = registerVM.PhoneNumber,
                Email = registerVM.Email,
                HiringDate = DateTime.Now
            });

            // Account
            await InsertAsync(new Account {
                EmployeeNik = registerVM.NIK,
                Password = Hashing.HashPassword(registerVM.Password)
            });

            // Profiling
            await _profilingsRepository.InsertAsync(new Profiling {
                EmployeeNik = registerVM.NIK,
                EducationId = education.Id,
            });

            // AccountRole
            await _accountRolesRepository.InsertAsync(new AccountRole {
                AccountNik = registerVM.NIK,
                RoleId = 1002
            });

            await transaction.CommitAsync();
        } catch {
            await transaction.RollbackAsync();
        }
    }

    public async Task<bool> LoginAsync(LoginVM loginVM)
    {
        var getEmployees = await _employeeRepository.GetAllAsync();
        var getAccounts = await GetAllAsync();

        var getUserData = getEmployees.Join(getAccounts,
                                            e => e.Nik,
                                            a => a.EmployeeNik,
                                            (e, a) => new LoginVM {
                                                Email = e.Email,
                                                Password = a.Password
                                            })
                                      .FirstOrDefault(ud => ud.Email == loginVM.Email);

        return getUserData is not null && loginVM.Password == getUserData.Password;
    }

}
