using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Data.Repositories
{
    public class CalendarRepository
    {
        public IUnitOfWork UnitOfWork => Db;
        protected readonly SolucaoContext Db;
        protected readonly DbSet<Calendar> DbSet;
        public CalendarRepository(SolucaoContext _context)
        {
            Db = _context;
            DbSet = Db.Set<Calendar>();
        }

        public async Task<IEnumerable<Calendar>> GetAll(DateTime date)
        {
            var notIn = new List<string> { "3", "4" };

            return await Db.Calendars.Include(x => x.Equipament)
                                         .Include(x => x.Client.City)
                                         .Include(x => x.Client)
                                         .Include(x => x.Driver)
                                         .Include(x => x.Technique)
                                         .Include(x => x.User)
                                         .Include(x => x.CalendarSpecifications)
                                         .Where(x => x.Date.Date == date && x.Active && !notIn.Contains(x.Status))
                                         .OrderBy(x => x.Status).ToListAsync();

        }

        public async Task<Calendar> GetById(Guid id)
        {
            return await Db.Calendars.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ValidationResult> Add(Calendar calendar)
        {
            try
            {

                Db.Calendars.Add(calendar);
                await Db.SaveChangesAsync();
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<ValidationResult> Update(Calendar calendar)
        {
            try
            {
                DbSet.Update(calendar);
                await Db.SaveChangesAsync();
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }

        }

        public async Task<IEnumerable<Calendar>> ValidateEquipament(DateTime date, Guid clientId, Guid equipamentId)
        {
            
            var sql = $"select * from Calendars where date >= '{date.ToString("yyyy-MM-dd")}' and equipamentId = '{equipamentId}' and ClientId != '{clientId}' and status not in ('3','4')";
            return await Db.Calendars.FromSqlRaw(sql).ToListAsync();

        }

        public async Task<IEnumerable<Calendar>> GetCalendarBySpecificationsAndDate(List<CalendarSpecifications> list, DateTime date, DateTime startTime)
        {
            var _in = In(list.Select(x => x.SpecificationId).ToList());

            var sql = $"select distinct c.* from Calendars as c left join CalendarSpecifications as cs on " +
                                "c.Id = cs.CalendarId " +
                                $"where CONVERT(varchar, c.date, 112) >= '{date.ToString("yyyyMMdd")}' and ";
            if (list.Any())
                sql += $"cs.SpecificationId in ({_in}) and ";

            sql += $"'{startTime.ToString("HH:mm:ss")}' >= CONVERT(varchar, c.StartTime, 108) and " +
                                $"'{startTime.ToString("HH:mm:ss")}' <= CONVERT(varchar, c.EndTime, 108) ";

            return await Db.Calendars.FromSqlRaw(sql).ToListAsync();
        }

        public async Task<int> SpecCounterBySpec(Guid specificationId, DateTime date, DateTime startTime)
        {
            var sql = $"select count(cs.Id) as amount from Calendars as c left join CalendarSpecifications as cs on " +
                                "c.Id = cs.CalendarId " +
                                $"where CONVERT(varchar, c.date, 112) >= '{date.ToString("yyyyMMdd")}' and " +
                                $"cs.SpecificationId = '{specificationId}' and " +
                                $"'{startTime.ToString("HH:mm:ss")}' >= CONVERT(varchar, c.StartTime, 108) and " +
                                $"'{startTime.ToString("HH:mm:ss")}' <= CONVERT(varchar, c.EndTime, 108) ";
            return await Db.Calendars.FromSqlRaw(sql).CountAsync();
        }


        public async Task<int> SingleSpecCounter(Guid specificationId, DateTime date)
        {
            var sql = $"select count(cs.Id) as amount from Calendars as c left join CalendarSpecifications as cs on " +
                                "c.Id = cs.CalendarId " +
                                $"where CONVERT(varchar, c.date, 112) >= '{date.ToString("yyyyMMdd")}' and " +
                                $"cs.SpecificationId = '{specificationId}'";
            return await Db.Calendars.FromSqlRaw(sql).CountAsync();
        }


        public async Task<IEnumerable<Calendar>> Availability(DateTime startDate, DateTime endDate,  Guid? clientId, Guid? equipamentId)
        {

            if (clientId.HasValue)
            {
                return await Db.Calendars.Include(x => x.Equipament)
                                          .Include(x => x.Client)
                                          .Include(x => x.Driver)
                                          .Include(x => x.Technique)
                                          .Include(x => x.User)
                                          .Include(x => x.CalendarSpecifications)
                                          .Where(x => x.Date.Date >= startDate && x.Date.Date <= endDate && 
                                                      x.ClientId == clientId.Value && 
                                                      x.Active).OrderBy(x => x.Date).ToListAsync();
            }

            if (equipamentId.HasValue)
            {
                return await Db.Calendars.Include(x => x.Equipament)
                                          .Include(x => x.Client)
                                          .Include(x => x.Driver)
                                          .Include(x => x.Technique)
                                          .Include(x => x.User)
                                          .Include(x => x.CalendarSpecifications)
                                          .Where(x => x.Date.Date >= startDate && x.Date.Date <= endDate && 
                                                      x.EquipamentId == equipamentId.Value && 
                                                      x.Active).OrderBy(x => x.Date).ToListAsync();
            }

            if (equipamentId.HasValue && clientId.HasValue)
            {
                return await Db.Calendars.Include(x => x.Equipament)
                                          .Include(x => x.Client)
                                          .Include(x => x.Driver)
                                          .Include(x => x.Technique)
                                          .Include(x => x.User)
                                          .Include(x => x.CalendarSpecifications)
                                          .Where(x => x.Date.Date >= startDate && x.Date.Date <= endDate && 
                                                      x.EquipamentId == equipamentId.Value && 
                                                      x.ClientId == clientId.Value &&
                                                      x.Active).OrderBy(x => x.Date).ToListAsync();
            }
            return await Db.Calendars.Include(x => x.Equipament)
                                          .Include(x => x.Client)
                                          .Include(x => x.Driver)
                                          .Include(x => x.Technique)
                                          .Include(x => x.User)
                                          .Include(x => x.CalendarSpecifications)
                                          .Where(x => x.Date.Date >= startDate && x.Date.Date <= endDate && 
                                                      x.Active).OrderBy(x => x.Date).ToListAsync();

        }

        private string In(List<Guid> list)
        {
            var join = new List<string>();
            foreach (var item in list)
            {
                join.Add("'" + item + "'");
            }
            return string.Join(",",join);
        }

    }
}
