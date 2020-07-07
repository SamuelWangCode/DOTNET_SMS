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
    public class usersController : ApiController
    {
        private SMSModel db = new SMSModel();

        [HttpPost]
        public object Login(object json)
        {
            return Biz.UserBiz.Login(json);
        }

        [HttpPost]
        public object GetUserById(object json)
        {
            return Biz.UserBiz.GetUserById(json);
        }

        [HttpPost]
        public object changePassword(object json)
        {
            return Biz.UserBiz.ChangePassword(json);
        }

        // GET: api/users
        public IQueryable<user> Getuser()
        {
            return db.user;
        }

        // GET: api/users/5
        [ResponseType(typeof(user))]
        public async Task<Object> Getuser(int id)
        {
            user user = await db.user.FindAsync(id);
            if (user == null)
            {
                return Helper.JsonConverter.Error(420, "查无此人");
            }

            return Helper.JsonConverter.BuildResult(user);
        }

        // PUT: api/users/5
        [ResponseType(typeof(void))]
        public async Task<Object> Putuser(int id, user user)
        {
            if (!ModelState.IsValid)
            {
                return Helper.JsonConverter.Error(410, "数据访问出错");
            }

            if (id != user.Id)
            {
                return Helper.JsonConverter.Error(420, "查无此人");
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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

        // POST: api/users
        [ResponseType(typeof(user))]
        public async Task<Object> Postuser(user user)
        {
            if (!ModelState.IsValid)
            {
                return Helper.JsonConverter.Error(410, "数据访问出错");
            }

            db.user.Add(user);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (userExists(user.Id))
                {
                    return Helper.JsonConverter.Error(420, "用户已存在");
                }
                else
                {
                    throw;
                }
            }

            return Helper.JsonConverter.BuildResult("成功");
        }

        // DELETE: api/users/5
        [ResponseType(typeof(user))]
        public async Task<IHttpActionResult> Deleteuser(int id)
        {
            user user = await db.user.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.user.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(int id)
        {
            return db.user.Count(e => e.Id == id) > 0;
        }
    }
}