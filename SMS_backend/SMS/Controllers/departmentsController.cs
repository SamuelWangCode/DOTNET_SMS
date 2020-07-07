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
    public class departmentsController : ApiController
    {
        private SMSModel db = new SMSModel();

        // GET: api/departments
        public IQueryable<department> Getdepartment()
        {
            return db.department;
        }

        // GET: api/departments/5
        [ResponseType(typeof(department))]
        public async Task<IHttpActionResult> Getdepartment(int id)
        {
            department department = await db.department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // PUT: api/departments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putdepartment(int id, department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != department.dep_id)
            {
                return BadRequest();
            }

            db.Entry(department).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!departmentExists(id))
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

        // POST: api/departments
        [ResponseType(typeof(department))]
        public async Task<IHttpActionResult> Postdepartment(department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.department.Add(department);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = department.dep_id }, department);
        }

        // DELETE: api/departments/5
        [ResponseType(typeof(department))]
        public async Task<IHttpActionResult> Deletedepartment(int id)
        {
            department department = await db.department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            db.department.Remove(department);
            await db.SaveChangesAsync();

            return Ok(department);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool departmentExists(int id)
        {
            return db.department.Count(e => e.dep_id == id) > 0;
        }
    }
}