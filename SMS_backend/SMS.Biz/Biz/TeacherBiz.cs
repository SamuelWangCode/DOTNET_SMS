using SMS.Helper;
using SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Biz
{
    public class TeacherBiz
    {
        public static object GetAllTeachers()
        {
            try
            {
                using (var context = new SMSModel())
                {

                    var a_teacher = (from each_teacher in context.teacher
                                     join each_user in context.user
                                     on each_teacher.t_id equals each_user.Id
                                     join each_dep in context.department
                                     on each_user.dep_id equals each_dep.dep_id
                                     select new
                                     {
                                         t_id = each_teacher.t_id,
                                         t_name = each_user.name,
                                         dep_id = each_dep.dep_id,
                                         dep_name = each_dep.dep_name,
                                         job_title = each_teacher.job_title,
                                         email = each_user.email,
                                         tel = each_user.tel,
                                         password = each_user.password
                                     });
                    var teaArray = a_teacher.ToList();
                    Console.WriteLine(teaArray);
                    return Helper.JsonConverter.BuildResult(teaArray);
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return Helper.JsonConverter.Error(410, "数据库中可能存在不一致现象，请检查");
            }
        }

        public static object PostTeacher(object json)
        {
            Dictionary<string, string> body = JsonConverter.Decode(json);
            var Id = int.Parse(body["Id"]);
            var name = body["name"];
            var email = body["email"];
            var tel = body["tel"];
            var password = body["password"];
            var dep_id = int.Parse(body["dep_id"]);
            var job_title = body["job_title"];
            try
            {
                using (var context = new SMSModel())
                {

                    user user = new user();
                    user.Id = Id;
                    user.name = name;
                    user.email = email;
                    user.tel = tel;
                    user.dep_id = dep_id;
                    user.password = password;
                    user.role = "teacher";
                    teacher teacher = new teacher();
                    teacher.t_id = Id;
                    teacher.job_title = job_title;
                    context.user.Add(user);
                    context.teacher.Add(teacher);
                    context.SaveChangesAsync();
                    return Helper.JsonConverter.BuildResult("成功");
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