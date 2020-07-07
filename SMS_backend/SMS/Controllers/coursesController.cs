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
using Microsoft.Ajax.Utilities;
using SMS.Models;

namespace SMS.Controllers
{
    public class coursesController : ApiController
    {
        private SMSModel db = new SMSModel();

        public object GetAllClasses()
        {
            return Biz.ClassBiz.GetAllClasses();
        }

        [HttpPost]
        public object FindClassesById(object json)
        {
            return Biz.ClassBiz.FindClassesById(json);
        }

        [HttpPost]
        public object ChangeBroadCast(object json)
        {
            return Biz.ClassBiz.ChangeBroadCast(json);
        }

        [HttpPost]
        public object GetStudentsByCourse(object json)
        {
            return Biz.ClassBiz.GetStudentsByCourse(json);
        }

        [HttpPost]
        public object GiveScore(object json)
        {
            return Biz.ClassBiz.GiveScore(json);
        }

        [HttpPost]
        public object PostClass(object json)
        {
            return Biz.ClassBiz.PostClass(json);
        }

        [HttpPost]
        public object PostStudentClass(object json)
        {
            return Biz.ClassBiz.PostStudentClass(json);
        }

        // GET: api/courses
        public IQueryable<course> Getcourse()
        {
            return db.course;
        }

        // GET: api/courses/5
        [ResponseType(typeof(course))]
        public async Task<IHttpActionResult> Getcourse(int id)
        {
            course course = await db.course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // PUT: api/courses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcourse(int id, course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.course_id)
            {
                return BadRequest();
            }

            db.Entry(course).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!courseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/courses
        [ResponseType(typeof(course))]
        public async Task<IHttpActionResult> Postcourse(course course)
        {
            if (!ModelState.IsValid)
            {
                return (IHttpActionResult)Helper.JsonConverter.Error(410, "数据访问出错");
            }

            db.course.Add(course);
            await db.SaveChangesAsync();

            return (IHttpActionResult)Helper.JsonConverter.BuildResult( "成功");
        }

        // DELETE: api/courses/5
        [ResponseType(typeof(course))]
        public async Task<IHttpActionResult> Deletecourse(int id)
        {
            course course = await db.course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            db.course.Remove(course);
            await db.SaveChangesAsync();

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool courseExists(int id)
        {
            return db.course.Count(e => e.course_id == id) > 0;
        }
    }
}