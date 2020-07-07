using SMS.Helper;
using SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMS.Biz
{
    public class UserBiz
    {
        public static object Login(object json)
        {
            try
            {
                Dictionary<string, string> body = JsonConverter.Decode(json);

                var Id = body["Id"];
                var password = body["password"];

                using (var context = new SMSModel())
                {

                    var users = context.user;
                    foreach (var a_user in users)
                    {
                        if (a_user.Id.ToString() == Id)
                        {
                            if (a_user.password == password)
                            {
                                var data = new
                                {
                                    Id = a_user.Id,
                                    role = a_user.role
                                };
                                return Helper.JsonConverter.BuildResult(data);
                            }
                            else
                            {
                                return Helper.JsonConverter.Error(400, "用户名或密码错误");
                            }
                        }
                    }
                    return Helper.JsonConverter.Error(400, "用户名或密码错误");
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return Helper.JsonConverter.Error(410, "数据库中可能存在不一致现象，请检查");
            }
        }
        public static object GetUserById(object json)
        {
            try
            {
                Dictionary<string, string> body = JsonConverter.Decode(json);
                var Id = body["Id"];
                var context = new SMSModel();
                var a_user = (from mUser in context.user
                           join mDep in context.department
                           on mUser.dep_id equals mDep.dep_id
                           where mUser.Id.ToString() == Id
                           select new
                           {
                               Id = mUser.Id,
                               name = mUser.name,
                               password = mUser.password,
                               dep_id = mUser.dep_id,
                               dep_name = mDep.dep_name,
                               email = mUser.email,
                               tel = mUser.tel,
                               role = mUser.role
                           });
                var the_user = a_user.Single();
                if (a_user.Single().role == "teacher")
                {
                    var a_teacher = (from mUser in context.user
                                     join mTeacher in context.teacher
                                     on mUser.Id equals mTeacher.t_id
                                     where mUser.Id.ToString() == Id
                                     select new
                                     {
                                         job_title = mTeacher.job_title
                                     });
                    var the_teacher = a_teacher.Single();
                    var data = new
                    {
                        Id = the_user.Id,
                        name = the_user.name,
                        password = the_user.password,
                        dep_id = the_user.dep_id,
                        dep_name = the_user.dep_name,
                        email = the_user.email,
                        tel = the_user.tel,
                        role = the_user.role,
                        job_title = the_teacher.job_title
                    };
                    return Helper.JsonConverter.BuildResult(data);
                }
                else
                {
                    var a_student = (from mUser in context.user
                                     join mStudent in context.student
                                     on mUser.Id equals mStudent.s_id
                                     where mUser.Id.ToString() == Id
                                     select new
                                     {
                                         grade = mStudent.grade
                                     });
                    var the_student = a_student.Single();
                    var data = new
                    {
                        Id = the_user.Id,
                        name = the_user.name,
                        password = the_user.password,
                        dep_id = the_user.dep_id,
                        dep_name = the_user.dep_name,
                        email = the_user.email,
                        tel = the_user.tel,
                        role = the_user.role,
                        grade = the_student.grade
                    };
                    return Helper.JsonConverter.BuildResult(data);
                }            
            }
            catch(Exception e)
            {
                Console.Write(e);
                return Helper.JsonConverter.Error(410, "数据库中可能存在不一致现象，请检查");
            }
        }
        public static object ChangePassword(object json)
        {
            try
            {
                Dictionary<string, string> body = JsonConverter.Decode(json);

                var Id = int.Parse(body["Id"]);
                var oldPassword = body["oldPassword"];
                var newPassword = body["newPassword"];

                using (var context = new SMSModel())
                {
                    var a_user = context.user.Find(Id);
                    if(a_user.password == oldPassword)
                    {
                        a_user.password = newPassword;
                        context.SaveChangesAsync();
                        return Helper.JsonConverter.BuildResult("成功");
                    }
                    else
                    {
                        return Helper.JsonConverter.Error(410, "密码不正确");
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return Helper.JsonConverter.Error(410, "数据库中可能存在不一致现象，请检查");
            }
        }
    }
}