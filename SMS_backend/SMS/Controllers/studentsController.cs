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
    public class studentsController : ApiController
    {
        private SMSModel db = new SMSModel();

        public object GetAllStudents()
        {
            return Biz.StudentBiz.GetAllStudents();
        }

        [HttpPost]
        public object PostStudent(object json)
        {
            return Biz.StudentBiz.PostStudent(json);
        }

        // GET: api/students
        public IQueryable<student> Getstudent()
        {
            return db.student;
        }

        // GET: api/students/5
        [ResponseType(typeof(student))]
        public async Task<Object> Getstudent(int id)
        {
            student student = await db.student.FindAsync(id);
            if (student == null)
            {
                return Helper.JsonConverter.Error(420, "查无此人");
            }

            return Helper.JsonConverter.BuildResult(student);
        }

        // PUT: api/students/5
        [ResponseType(typeof(void))]
        public async Task<Object> Putstudent(int id, student student)
        {
            if (!ModelState.IsValid)
            {
                return Helper.JsonConverter.Error(410, "数据访问失败");
            }

            if (id != student.s_id)
            {
                return Helper.JsonConverter.Error(420, "用户已存在");
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!studentExists(id))
                {
                    return Helper.JsonConverter.Error(420, "查无此人");
                }
                else
                {
                    throw;
                }
            }

            return Helper.JsonConverter.BuildResult("成功");
        }


        // DELETE: api/students/5
        [ResponseType(typeof(student))]
        public async Task<IHttpActionResult> Deletestudent(int id)
        {
            student student = await db.student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            db.student.Remove(student);
            await db.SaveChangesAsync();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool studentExists(int id)
        {
            return db.student.Count(e => e.s_id == id) > 0;
        }
    }
}