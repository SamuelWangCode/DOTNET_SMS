using SMS.Helper;
using SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Biz
{
    public class StudentBiz
    {
        public static object GetAllStudents()
        {
            try
            {
                using (var context = new SMSModel())
                {

                    var a_student = (from each_student in context.student
                                        join each_user in context.user
                                        on each_student.s_id equals each_user.Id
                                        join each_dep in context.department
                                        on each_user.dep_id equals each_dep.dep_id
                                        select new
                                        {
                                            s_id = each_student.s_id,
                                            s_name = each_user.name,
                                            dep_id = each_dep.dep_id,
                                            dep_name = each_dep.dep_name,
                                            grade = each_student.grade,
                                            email = each_user.email,
                                            tel = each_user.tel,
                                            password = each_user.password
                                        });
                    var stuArray = a_student.ToList();
                    Console.WriteLine(stuArray);
                    return Helper.JsonConverter.BuildResult(stuArray);
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return Helper.JsonConverter.Error(410, "数据库中可能存在不一致现象，请检查");
            }
        }

        public static object PostStudent(object json)
        {
            Dictionary<string, string> body = JsonConverter.Decode(json);
            var Id = int.Parse(body["Id"]);
            var name = body["name"];
            var email = body["email"];
            var tel = body["tel"];
            var password = body["password"];
            var dep_id = int.Parse(body["dep_id"]);
            var grade = int.Parse(body["grade"]);
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
                    user.role = "student";
                    student student = new student();
                    student.s_id = Id;
                    student.grade = grade;
                    context.user.Add(user);
                    context.student.Add(student);
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