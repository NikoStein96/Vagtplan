using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vagtplan.Data;
using Vagtplan.Models;
using Vagtplan.Models.Dto;
using Microsoft.Office.Interop.Excel;
using System.Drawing.Text;
using OfficeOpenXml;
using Vagtplan.Migrations;

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

        private readonly ShiftPlannerContext _context;

        public ScheduleController(ShiftPlannerContext dbContext)
        {

            _context = dbContext;

        }


        [HttpPost]
        public ActionResult CreateSchedule(ScheduleDto scheduleDto)
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

            Schedule schedule = new Schedule();
            schedule.StartTime = scheduleDto.StartTime;
            schedule.EndTime = scheduleDto.EndTime;
            for (DateOnly date = scheduleDto.StartTime; date <= scheduleDto.EndTime; date = date.AddDays(1))
            {

                Day day = new Day();
                day.Schedule = schedule;
                day.DayDate = date;
                schedule.Days.Add(day);

            }

            _context.Schedules.Add(schedule);
            _context.SaveChanges();

            return Ok(schedule);
        }

        [HttpGet]
        public async Task<ActionResult<List<Schedule>>> GetSchedules()
        {

            var Schedules = await _context.Schedules.Include(schedule => schedule.Days).ThenInclude(days => days.Shifts).ToListAsync();
            return Schedules;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            var schedule = await _context.Schedules
                                        .Include(schedule => schedule.Days)
                                        .FirstOrDefaultAsync(schedule => schedule.Id == id);

            if (schedule == null)
            {
                return NotFound();
            }

            return schedule;
        }


        [HttpGet("ExportSchedule/{id}")]
        public async Task<ActionResult<Schedule>> ExportSchedule(int id)
        {
            var schedule = await _context.Schedules
                                        .Include(schedule => schedule.Days).ThenInclude(day => day.Shifts).ThenInclude(shift => shift.Employee)
                                        .FirstOrDefaultAsync(schedule => schedule.Id == id);


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
