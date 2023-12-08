using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vagtplan.Data;
using Vagtplan.Models;
using Microsoft.Office.Interop.Excel;
using System.Drawing.Text;
using OfficeOpenXml;
using Vagtplan.Dto;
using Vagtplan.Models.Dto;
using Vagtplan.Interfaces.Services;
using System.Runtime.InteropServices;

namespace Vagtplan.Controllers
{
    public static class EpPlusExtensions
    {
        public static void LoadFromCollectionHorizontal<T>(this ExcelWorksheet worksheet, IEnumerable<T> data, int startRow, int startColumn)
        {
            for (var i = 0; i < data.Count(); i++)
            {
                worksheet.Cells[startRow, startColumn + i].Value = data.ElementAt(i);
            }
        }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        private readonly IScheduleService _scheduleService;
        private readonly IEmployeeService _employeeService;


        public ScheduleController(IScheduleService scheduleService, IEmployeeService employeeService)
        {

            _scheduleService = scheduleService;
            _employeeService = employeeService;
        }

      
        

         


        [HttpPost]
        public ActionResult CreateSchedule(CreateScheduleDto scheduleDto)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            // Validation: Check if StartTime is older than today
            if (scheduleDto.StartTime < today)
            {
                return BadRequest(new { error = "Start date cannot be older than today." });
            }

            // Validation: Check if StartTime is later than EndTime
            if (scheduleDto.StartTime > scheduleDto.EndTime)
            {
                return BadRequest(new { error = "Start date cannot be later than end date." });
            }

            return Ok(_scheduleService.CreateSchedule(scheduleDto));
        }

        [HttpGet]
        public async Task<List<Schedule>> GetSchedules()
        {

            return await _scheduleService.GetSchedules();
        }



        [HttpPut("UpdateAvailableEmployees")]
        public bool UpdateAvailableEmployees(UpdateAvailableDaysDto UpdateDays) {

            _scheduleService.UpdateAvailableEmployees(UpdateDays);
            return true;

        }

        [HttpPut("UpdateShiftDraft")]
        public async Task<ActionResult<Schedule>> GenerateShiftDraft(int scheduleid)
        {
            var schedule =  _scheduleService.GetSchedule(scheduleid);

            foreach (Day day in schedule.Days) {
                foreach (Shift shift in day.Shifts)
                {
                    shift.Employee = GetEmployeeWithLeastWorkload(day.AvailableEmployees);
                    day.AvailableEmployees.Remove(shift.Employee);

                }         
            }
            return schedule;
        }

        static Employee GetEmployeeWithLeastWorkload(List<Employee> employees)
        {
            return employees.OrderBy(e => e.Shifts.Count).FirstOrDefault();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            var schedule = _scheduleService.GetSchedule(id);

            if (schedule == null)
            {
                return NotFound();
            }

            return schedule;
        }


        [HttpGet("ExportSchedule/{id}")]
        public async Task<ActionResult<Schedule>> ExportSchedule(int id)
        {
            var schedule = _scheduleService.GetSchedule(id);


            if (schedule == null) { return NotFound(); }


            var file = new FileInfo($@"C:\excelsheets\{id}.xlsx");

            await SaveExcelFile(schedule.Days.Cast<Day>().ToList(), file);


            return schedule;
        }


        private static async Task SaveExcelFile(List<Day> days, FileInfo file) {
            DeleteIfExsts(file);

            using (var package = new ExcelPackage(file))
            {
                


                var ws = package.Workbook.Worksheets.Add(file.ToString());

                List<String> dates = new List<String>();
                List<String> employees = new List<String>();
                List<Employee> employeesButTheActualModel = new List<Employee>();

                foreach (Day day in days)
                {
                    dates.Add(day.DayDate.ToString());

                }


                ws.LoadFromCollectionHorizontal(dates,1, 2);
                


                foreach (Day day in days)
                {
                    foreach (Shift shift in day.Shifts)
                    {
                        if(shift.Employee.Name != null)
                        {
                            if (!employees.Contains(shift.Employee.Name))
                            {
                                employees.Add(shift.Employee.Name);
                                employeesButTheActualModel.Add(shift.Employee);
                            }
                        }
                    }

                }
                
                ws.Cells["A2"].LoadFromCollection(employees, false);
                int i = 2;
                foreach (Employee employee in employeesButTheActualModel) {
                    List<String> tempShifts = new List<String>();
                    foreach(Day day in days)
                    {
                        Shift shift = day.Shifts.FirstOrDefault(shift => shift.Employee.FirebaseId ==  employee.FirebaseId);
                        if (shift != null)
                        {
                            tempShifts.Add(shift.StartTime.ToString("HH:mm") + "-" + shift.EndTime.ToString("HH:mm"));
                        }
                        else
                        {
                            tempShifts.Add(" ");
                        }
                    }
                    ws.LoadFromCollectionHorizontal(tempShifts, i, 2);
                    i++;
                }
                ws.Columns.AutoFit();



                await package.SaveAsync();


            }

        }

        private static void DeleteIfExsts(FileInfo file){ if(file.Exists) { file.Delete(); }

      

    }

    }


}
