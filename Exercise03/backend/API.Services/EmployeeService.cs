using Microsoft.EntityFrameworkCore;
using System.Net;
using TranAnhDung.API.Common.Helper;
using TranAnhDung.API.DataAccess.Context;
using TranAnhDung.API.Domain;
using TranAnhDung.API.Services.Interface;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
namespace TranAnhDung.API.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly EFDataContext _context;
        private readonly ApplicationSettings _appSettings;

        public EmployeeService(EFDataContext context, ApplicationSettings applicationSettings)
        {
            _context = context;
            _appSettings = applicationSettings;
        }

        // public async Task<Response> Add(Employee emp)
        // {
        //     try
        //     {
        //         var employee = new API.DataAccess.Entity.Employee()
        //         {
        //             FirstName = emp.FirstName,
        //             LastName = emp.LastName,
        //             Mobile = emp.Mobile,
        //             IsPermanent = emp.IsPermanent,
        //             DepartmentId = emp.DepartmentId,
        //             Gender = emp.Gender,
        //             DateOfBirth = emp.DateOfBirth,
        //             Email = emp.Email,
        //             imageUrl=emp.imageUrl,
        //         };

        //         await _context.Employees.AddAsync(employee);
        //         await _context.SaveChangesAsync();

        //         return new Response
        //         {
        //             EmployeeId = employee.EmployeeId,
        //             IsSuccess = true,
        //             Message = _appSettings.GetConfigurationValue("EmployeeMessages", "CreateEmployeeSuccess"),
        //             HttpStatusCode = HttpStatusCode.OK,
        //         };
        //     }
        //     catch (Exception ex)
        //     {
        //         return new Response
        //         {
        //             IsSuccess = false,
        //             Message = _appSettings.GetConfigurationValue("EmployeeMessages", "CreateEmployeeFailure") + " " + ex.Message,
        //             HttpStatusCode = HttpStatusCode.BadRequest,
        //         };
        //     }
        // }

        public async Task<Response> Add(Employee empDto, IFormFile file)
        {
            try
            {
                // Kiểm tra nếu file ảnh tồn tại
                if (file != null && file.Length > 0)
                {
                    // Lưu file ảnh vào thư mục wwwroot/images
                    var filePath = Path.Combine("wwwroot/images", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Lưu tên file vào employeeDto
                    empDto.imageUrl = file.FileName;
                }

                // Tạo đối tượng Employee từ EmployeeDto
                var employee = new API.DataAccess.Entity.Employee()
                {
                    FirstName = empDto.FirstName,
                    LastName = empDto.LastName,
                    Mobile = empDto.Mobile,
                    IsPermanent = empDto.IsPermanent,
                    DepartmentId = empDto.DepartmentId,
                    Gender = empDto.Gender,
                    DateOfBirth = empDto.DateOfBirth,
                    Email = empDto.Email,
                    imageUrl = empDto.imageUrl, // Gán đường dẫn ảnh
                };

                // Thêm nhân viên vào cơ sở dữ liệu
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();

                return new Response
                {
                    EmployeeId = employee.EmployeeId,
                    IsSuccess = true,
                    Message = _appSettings.GetConfigurationValue("EmployeeMessages", "CreateEmployeeSuccess"),
                    HttpStatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = _appSettings.GetConfigurationValue("EmployeeMessages", "CreateEmployeeFailure") + " " + ex.Message,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                };
            }
        }

        public async Task<Response> Update(Employee emp)
        {
            try
            {
                if (emp.EmployeeId <= 0)
                {
                    return new Response
                    {
                        EmployeeId = emp.EmployeeId,
                        IsSuccess = false,
                        Message = _appSettings.GetConfigurationValue("EmployeeMessages", "EmployeeNotFound"),
                        HttpStatusCode = HttpStatusCode.BadRequest,
                    };
                }

                var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == emp.EmployeeId);
                if (employee == null)
                {
                    return new Response
                    {
                        EmployeeId = emp.EmployeeId,
                        IsSuccess = false,
                        Message = _appSettings.GetConfigurationValue("EmployeeMessages", "EmployeeNotFound"),
                        HttpStatusCode = HttpStatusCode.BadRequest,
                    };
                }

                employee.FirstName = emp.FirstName;
                employee.LastName = emp.LastName;
                employee.Mobile = emp.Mobile;
                employee.IsPermanent = emp.IsPermanent;
                employee.Gender = emp.Gender;
                employee.DateOfBirth = emp.DateOfBirth;
                employee.DepartmentId = emp.DepartmentId;
                employee.imageUrl = emp.imageUrl;
                await _context.SaveChangesAsync();

                return new Response
                {
                    EmployeeId = employee.EmployeeId,
                    IsSuccess = true,
                    Message = _appSettings.GetConfigurationValue("EmployeeMessages", "UpdateEmployeeSuccess"),
                    HttpStatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = _appSettings.GetConfigurationValue("EmployeeMessages", "UpdateEmployeeFailure") + " " + ex.Message,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                };
            }
        }

        public async Task<Response> Delete(int employeeId)
        {
            try
            {
                if (employeeId <= 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = _appSettings.GetConfigurationValue("EmployeeMessages", "EmployeeNotFound"),
                        HttpStatusCode = HttpStatusCode.BadRequest,
                    };
                }

                var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == employeeId);
                if (employee == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = _appSettings.GetConfigurationValue("EmployeeMessages", "EmployeeNotFound"),
                        HttpStatusCode = HttpStatusCode.BadRequest,
                    };
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    EmployeeId = employeeId,
                    Message = _appSettings.GetConfigurationValue("EmployeeMessages", "DeleteEmployeeSuccess"),
                    HttpStatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = _appSettings.GetConfigurationValue("EmployeeMessages", "DeleteEmployeeFailure") + " " + ex.Message,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                };
            }
        }

        public async Task<List<Employee>> GetAll()
        {
            try
            {
                var employees = await (from emp in _context.Employees
                                       join dep in _context.Departments on emp.DepartmentId equals dep.DepartmentId
                                       select new Employee
                                       {
                                           EmployeeId = emp.EmployeeId,
                                           FirstName = emp.FirstName,
                                           LastName = emp.LastName,
                                           Mobile = emp.Mobile,
                                           Email = emp.Email,
                                           DepartmentId = dep.DepartmentId,
                                           Department = dep.DepartmentName,
                                           IsPermanent = emp.IsPermanent,
                                           DateOfBirth = emp.DateOfBirth,
                                           imageUrl = emp.imageUrl,
                                           Gender = emp.Gender
                                       })
                                       .OrderByDescending(x => x.EmployeeId)
                                       .ToListAsync();
                return employees;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Employee> GetById(int employeeId)
        {
            try
            {
                var employee = await (from emp in _context.Employees
                                      join dep in _context.Departments on emp.DepartmentId equals dep.DepartmentId
                                      where emp.EmployeeId == employeeId
                                      select new Employee
                                      {
                                          EmployeeId = emp.EmployeeId,
                                          FirstName = emp.FirstName,
                                          LastName = emp.LastName,
                                          Mobile = emp.Mobile,
                                          Email = emp.Email,
                                          Department = dep.DepartmentName,
                                          IsPermanent = emp.IsPermanent,
                                          DateOfBirth = emp.DateOfBirth,
                                          imageUrl = emp.imageUrl,
                                          Gender = emp.Gender
                                      }).FirstOrDefaultAsync();
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
