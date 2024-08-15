using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using API.Common.Helper;
using API.DataAccess.Context;
using API.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class DepartmentService : IDepartment
    {
        private readonly EFDataContext _context;
        private readonly ApplicationSettings _appSettings;

        public DepartmentService(EFDataContext context, ApplicationSettings applicationSettings)
        {
            _context = context;
            _appSettings = applicationSettings;
        }

        public async Task<List<Department>> GetAll()
        {
            try
            {
                var departments = await (from dep in _context.Departments
                                         select new Department
                                         {
                                             DepartmentId = dep.DepartmentId,
                                             DepartmentName = dep.DepartmentName
                                         }).OrderByDescending(x => x.DepartmentId).ToListAsync();
                return departments;
            }
            catch (Exception ex)
            {
                // Consider adding logging here
                throw new Exception(ex.Message);
            }
        }

        public async Task<Department> GetById(int departmentId)
        {
            try
            {
                var department = await (from dep in _context.Departments
                                        where dep.DepartmentId == departmentId
                                        select new Department
                                        {
                                            DepartmentId = dep.DepartmentId,
                                            DepartmentName = dep.DepartmentName
                                        }).FirstOrDefaultAsync();
                return department;
            }
            catch (Exception ex)
            {
                // Consider adding logging here
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response> Add(Department department)
        {
            try
            {
                if (department == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Department data is null."
                    };
                }

                var newDepartment = new API.DataAccess.Entity.Department
                {
                    DepartmentName = department.DepartmentName
                };

                _context.Departments.Add(newDepartment);
                await _context.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = "Department created successfully.",
                    DepartmentId = newDepartment.DepartmentId
                };
            }
            catch (Exception ex)
            {
                // Consider adding logging here
                return new Response
                {
                    IsSuccess = false,
                    Message = "Failed to create department. " + ex.Message
                };
            }
        }

        public async Task<Response> Delete(int departmentId)
        {
            try
            {
                var department = await _context.Departments
                    .FirstOrDefaultAsync(dep => dep.DepartmentId == departmentId);

                if (department == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Department not found."
                    };
                }

                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = "Department deleted successfully."
                };
            }
            catch (Exception ex)
            {
                // Consider adding logging here
                return new Response
                {
                    IsSuccess = false,
                    Message = "Failed to delete department. " + ex.Message
                };
            }
        }
    }
}
