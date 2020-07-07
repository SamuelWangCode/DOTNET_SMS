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
    public class teachersController : ApiController
    {
        private SMSModel db = new SMSModel();


        public object GetAllTeachers()
        {
            return Biz.TeacherBiz.GetAllTeachers();
        }

        [HttpPost]
        public object PostTeacher(object json)
        {
            return Biz.TeacherBiz.PostTeacher(json);
        }

        // GET: api/teachers
        public IQueryable<teacher> Getteacher()
        {
            return db.teacher;
        }

        // GET: api/teachers/5
        [ResponseType(typeof(teacher))]
        public async Task<Object> Getteacher(int id)
        {
            teacher teacher = await db.teacher.FindAsync(id);
            if (teacher == null)
            {
                return Helper.JsonConverter.Error(410, "查无此人");
            }

            return Helper.JsonConverter.BuildResult(teacher);
        }

        // PUT: api/teachers/5
        [ResponseType(typeof(void))]
        public async Task<Object> Putteacher(int id, teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return Helper.JsonConverter.Error(410, "数据访问出错");
            }

            if (id != teacher.t_id)
            {
                return Helper.JsonConverter.Error(420, "用户已存在");
            }

            db.Entry(teacher).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!teacherExists(id))
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

        // DELETE: api/teachers/5
        [ResponseType(typeof(teacher))]
        public async Task<IHttpActionResult> Deleteteacher(int id)
        {
            teacher teacher = await db.teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            db.teacher.Remove(teacher);
            await db.SaveChangesAsync();

            return Ok(teacher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool teacherExists(int id)
        {
            return db.teacher.Count(e => e.t_id == id) > 0;
        }
    }
}