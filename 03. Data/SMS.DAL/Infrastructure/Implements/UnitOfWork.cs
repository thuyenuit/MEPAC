using SMS.DAL.DbContext;
using SMS.DAL.Infrastructure.Interfaces;
using SMS.Model.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text;

namespace SMS.DAL.Infrastructure.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private SMSDbContext dbContext;

        public UnitOfWork(IDbFactory _dbFactory) {
            this.dbFactory = _dbFactory;
        }

        public SMSDbContext DbContext
        {
            get { return dbContext ?? ( dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            try
            {
                DbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string table = "";
                StringBuilder _error = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    table += eve.Entry.Entity.ToString().Replace("SMS.Model.Models.", "") + ". ";
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    _error.Append($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error. ");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"Property: \"{ve.PropertyName}\" in state \"{ve.ErrorMessage}\"");
                        _error.Append($"Property: \"{ve.PropertyName}\" in state \"{ve.ErrorMessage}\". \t");
                    }
                }
                this.SaveError(_error.ToString(), null, null, table);
            }
            catch (DbUpdateException dbEx)
            {
                //throw new Exception(dbEx.InnerException.Message);
                this.SaveError(dbEx.InnerException.Message, null, null, null);
            }
            catch (Exception ex)
            {
                // ex.StackTrace
                //reponse = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                //throw new Exception(ex.Message);
                this.SaveError(ex.Message, null, null, null);
            }         
        }

        private void SaveError(string message, string stackTrace, string method, string table)
        {
            SMSDbContext _dbContext = new SMSDbContext();
            ErrorLog _error = new ErrorLog() {
                Message = message,
                DateLog = DateTime.Now,
                StackTrace = stackTrace,
                Method = method,
                Table = table
            };
            _dbContext.Set<ErrorLog>().Add(_error);
            _dbContext.SaveChanges();
        }
    }
}
