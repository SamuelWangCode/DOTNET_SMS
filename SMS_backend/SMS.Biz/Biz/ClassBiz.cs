using SMS.Helper;
using SMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CppMathDll;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Web;

namespace SMS.Biz
{
    public class ClassBiz
    { 

        public static object GetAllClasses()
        {
            try
            {
                using (var context = new SMSModel())
                {
                    var classes = context.course;
                    var list = new List<object>();
                    if (!classes.Any())
                    {
                        return Helper.JsonConverter.Error(400, "现在还没有已经创建的班级");
                    }

                    //foreach (var a_class in classes)
                    //{
                    //    var a_course = (from each_course in context.course
                    //                    where each_course.course_id == a_class.course_id
                    //                    select new
                    //                    {
                    //                        course_id = each_course.course_id,
                    //                        course_name = each_course.course_name,
                    //                        broadCast = each_course.broadCast,
                    //                        dep_id = each_course.dep_id,
                    //                        t_id = each_course.t_id,
                    //                        credits = each_course.credits
                    //                    });
                    //    if (!a_course.Any())
                    //        continue;
                    //    var the_course = a_course.Single();
                    //foreach (var a_class in classes)
                    //{
                    //    var a_course = (from each_course in context.course
                    //                    join each_department in context.department
                    //                    on each_course.dep_id equals each_department.dep_id
                    //                    where each_course.course_id == a_class.course_id
                    //                    select new
                    //                    {
                    //                        dep_name = each_department.dep_name
                    //                    });
                    //    var a_teacher = (from each_course in context.course
                    //                    join each_user in context.user
                    //                    on each_course.t_id equals each_user.Id
                    //                    where each_course.course_id == a_class.course_id
                    //                    select new
                    //                    {
                    //                        t_name = each_user.name
                    //                    });
                    //    list.Add(new
                    //    {
                    //        course_id = a_class.course_id,
                    //        course_name = a_class.course_name,
                    //        broadCast = a_class.broadCast,
                    //        dep_id = a_class.dep_id,
                    //        dep_name = a_course.Single().dep_name,
                    //        t_id = a_class.t_id,
                    //        t_name = a_teacher.Single().t_name,
                    //        credits = a_class.credits
                    //    });

                    var a_class = (from each_class in context.course
                                     join each_user in context.user
                                     on each_class.t_id equals each_user.Id
                                     join each_dep in context.department
                                     on each_user.dep_id equals each_dep.dep_id
                                     select new
                                     {
                                         course_id = each_class.course_id,
                                         course_name = each_class.course_name,
                                         dep_id = each_dep.dep_id,
                                         dep_name = each_dep.dep_name,
                                         credits = each_class.credits,
                                         t_id = each_class.t_id,
                                         t_name = each_user.name,
                                     });
                    var stuArray = a_class.ToList();
                    Console.WriteLine(stuArray);
                    return Helper.JsonConverter.BuildResult(stuArray);
                }
            }
            catch (Exception e)
            {
                System.Console.Write(e.Message);
                return Helper.JsonConverter.Error(410, "数据库中可能存在不一致现象，请检查");
            }
        }
        public static object FindClassesById(object json)
        {
            try
            {
                Dictionary<string, string> body = JsonConverter.Decode(json);

                var Id = int.Parse(body["Id"]);

                using (var context = new SMSModel())
                {
                    var attend_classes = context.student_class;
                    var list = new List<object>();
                    var a_user = context.user.Find(Id);
                    if (a_user.role == "student") {
                        var a_class = (from each_class in context.course
                                       join each_stuclass in context.student_class
                                       on each_class.course_id equals each_stuclass.course_id
                                       where each_stuclass.s_id == Id
                                       select new
                                       {
                                           course_id = each_class.course_id,
                                           course_name = each_class.course_name,
                                           t_id = each_class.t_id,
                                           broadCast = each_class.broadCast,
                                           credits = each_class.credits,
                                           grade = each_stuclass.grade
                                       });
                        var the_teacher = a_class.ToArray();
                        List<int> creditsArray = new List<int>();
                        List<int> gradesArray = new List<int>();
                        int length = the_teacher.Length;
                        for (int i = 0; i < length; i++)
                        {
                            if (the_teacher[i].credits != null)
                            {
                                creditsArray.Add((int)the_teacher[i].credits);
                            }
                            else
                            {
                                creditsArray.Add(0);
                            }
                            if (the_teacher[i].grade != null)
                            {
                                gradesArray.Add((int)the_teacher[i].grade);
                            }
                            else
                            {
                                gradesArray.Add(0);
                            }
                        }
                        int sum1 = 0;
                        int sum2 = 0;
                        for(int i = 0; i < length; i++)
                        {
                            sum1 = CppMathDll.Class1.calGrades(sum1, creditsArray[i], gradesArray[i]);
                            sum2 = sum2 + creditsArray[i];
                        }
                        double final_grade = (double)sum1 / (double)sum2;
                        var data = new
                        {
                            final_grade = final_grade
                        };
                        
;
                        return Helper.JsonConverter.BuildResult(the_teacher);
                    }
                    else if(a_user.role == "teacher")
                    {
                        foreach (var a_course in context.course)
                        {
                            if (a_course.t_id == a_user.Id)
                            {
                                list.Add(new
                                {
                                    course_id = a_course.course_id,
                                    course_name = a_course.course_name,
                                    t_id = a_course.t_id,
                                    broadCast = a_course.broadCast,
                                    credits = a_course.credits,
                                    t_name = a_user.name
                                });
                            }
                        }
                        return Helper.JsonConverter.BuildResult(list);

                    }
                    else
                    {
                        return Helper.JsonConverter.Error(400,"该用户是管理员");
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return Helper.JsonConverter.Error(410, "数据库中可能存在不一致现象，请检查");
            }
        }
        public static object ChangeBroadCast(object json)
        {
            try
            {
                Dictionary<string, string> body = JsonConverter.Decode(json);

                var course_id = int.Parse(body["course_id"]);
                var broadCast = body["broadCast"];

                using (var context = new SMSModel())
                {
                    var a_course = context.course.Find(course_id);
                    if (a_course == null)
                    {
                        return Helper.JsonConverter.Error(410, "查无此班级");
                    }
                    else
                    {
                        a_course.broadCast = broadCast;
                        context.SaveChangesAsync();
                        return Helper.JsonConverter.BuildResult("成功");
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return Helper.JsonConverter.Error(410, "数据库中可能存在不一致现象，请检查");
            }
        }
        public static object GetStudentsByCourse(object json)
        {
            try
            {
                Dictionary<string, string> body = JsonConverter.Decode(json);

                var course_id = int.Parse(body["course_id"]);

                var list = new List<object>();

                using (var context = new SMSModel())
                {
                    var a_course = context.course.Find(course_id);
                    if (a_course == null)
                    {
                        return Helper.JsonConverter.Error(410, "查无此班级");
                    }
                    else
                    {
                        var a_student = (from each_stuclass in context.student_class
                                         join each_user in context.user
                                         on each_stuclass.s_id equals each_user.Id
                                         where each_stuclass.course_id == a_course.course_id
                                         select new
                                         {
                                             s_id = each_stuclass.s_id,
                                             s_name = each_user.name,
                                             grade = each_stuclass.grade
                                         });
                        var stuArray = a_student.ToList();
                        Console.WriteLine(stuArray);
                        return Helper.JsonConverter.BuildResult(stuArray);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return Helper.JsonConverter.Error(410, "数据库中可能存在不一致现象，请检查");
            }
        }
        public static object GiveScore(object json)
        {
            try
            {
                Dictionary<string, string> body = JsonConverter.Decode(json);

                var s_id = int.Parse(body["s_id"]);
                var course_id = int.Parse(body["course_id"]);
                var grade = int.Parse(body["grade"]);

                using (var context = new SMSModel())
                {
                    var a_user = context.student_class.Find(course_id, s_id);
                    
                    a_user.grade = grade;
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

        public static object PostClass(object json)
        {
            Dictionary<string, string> body = JsonConverter.Decode(json);
            var course_name = body["course_name"];
            var dep_id = int.Parse(body["dep_id"]);
            var t_id = int.Parse(body["t_id"]);
            var credits = int.Parse(body["credits"]);
            try
            {
                using (var context = new SMSModel())
                {

                    course course = new course();
                    course.course_name = course_name;
                    course.dep_id = dep_id;
                    course.t_id = t_id;
                    course.credits = credits;
                    course.broadCast = "";
                    context.course.Add(course);
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

        public static object PostStudentClass(object json)
        {
            Dictionary<string, string> body = JsonConverter.Decode(json);
            var course_id = int.Parse(body["course_id"]);
            var s_id = int.Parse(body["s_id"]);
            try
            {
                using (var context = new SMSModel())
                {

                    student_class stuclass = new student_class();
                    stuclass.course_id = course_id;
                    stuclass.s_id = s_id;
                    context.student_class.Add(stuclass);
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