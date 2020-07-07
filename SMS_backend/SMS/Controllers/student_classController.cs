using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SMS.Models;

namespace SMS.Controllers
{
    public class student_classController : ApiController
    {
        private SMSModel db = new SMSModel();

        // GET: api/student_class
        public IQueryable<student_class> Getstudent_class()
        {
            return db.student_class;
        }

        // GET: api/student_class/5
        [ResponseType(typeof(student_class))]
        public async Task<IHttpActionResult> Getstudent_class(int id)
        {
            student_class student_class = await db.student_class.FindAsync(id);
            if (student_class == null)
            {
                return NotFound();
            }

            return Ok(student_class);
        }

        // PUT: api/student_class/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putstudent_class(int id, student_class student_class)
        {
            if (!ModelState.IsValid)
            {
                return (IHttpActionResult)Helper.JsonConverter.Error(410, "数据访问出错");
            }

            if (id != student_class.course_id)
            {
                return (IHttpActionResult)Helper.JsonConverter.Error(420, "查无此班级");
            }

            db.Entry(student_class).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!student_classExists(id))
                {
                    return (IHttpActionResult)Helper.JsonConverter.Error(420, "查无此人");
                }
                else
                {
                    throw;
                }
            }

            return (IHttpActionResult)Helper.JsonConverter.BuildResult("成功");
        }

        // POST: api/student_class
        [ResponseType(typeof(student_class))]
        public async Task<IHttpActionResult> Poststudent_class(student_class student_class)
        {
            if (!ModelState.IsValid)
            {
                return (IHttpActionResult)Helper.JsonConverter.Error(410, "数据访问出错");
            }

            db.student_class.Add(student_class);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (student_classExists(student_class.course_id))
                {
                    return (IHttpActionResult)Helper.JsonConverter.Error(420, "用户已存在");
                }
                else
                {
                    throw;
                }
            }

            return (IHttpActionResult)Helper.JsonConverter.BuildResult("成功");
        }

        // DELETE: api/student_class/5
        [ResponseType(typeof(student_class))]
        public async Task<IHttpActionResult> Deletestudent_class(int id)
        {
            student_class student_class = await db.student_class.FindAsync(id);
            if (student_class == null)
            {
                return NotFound();
            }

            db.student_class.Remove(student_class);
            await db.SaveChangesAsync();

            return Ok(student_class);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool student_classExists(int id)
        {
            return db.student_class.Count(e => e.course_id == id) > 0;
        }
    }
}