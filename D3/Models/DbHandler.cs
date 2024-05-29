using System.Runtime.ConstrainedExecution;

namespace D3.Models
{
    public class DbHandler
    {
        public static User CheckUser(string username, string password)
        {
            User? user;
            using (LMSContext db = new LMSContext())
            {
                user = db.Users.SingleOrDefault(u => u.UserName == username && u.Password == password);
            }
            return user;
        }

        public static bool CheckUserById(int userID)
        {
            try
            {
                bool flag = false;
                using (LMSContext db = new LMSContext())
                {
                    User? user = db.Users.Find(userID);
                    if (user != null)
                    {
                        flag = true;
                    }
                }
                return flag;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckStudentById(int studentID)
        {
            try
            {
                bool flag = false;
                using (LMSContext db = new LMSContext())
                {
                    Student? student = db.Students.Find(studentID);
                    if (student != null)
                    {
                        flag = true;
                    }
                }
                return flag;
            }
            catch
            {
                return false;
            }
        }

        public static User GetUserById(int Id)
        {
            User? user;
            using(LMSContext db = new LMSContext())
            {
                user = db.Users.SingleOrDefault(u => u.Id == Id);
            }
            return user;
        }

        public static Student GetStudentById(int Id)
        {
            Student? std;
            using (LMSContext db = new LMSContext())
            {
                std = db.Students.Find(Id);
            }
            return std;
        }

        public static List<User> GetAllUser(int Id)
        {
            List<User> users;
            using (LMSContext db = new LMSContext())
            {
                users = db.Users.Where(u => u.Id != Id).ToList();
            }
            return users;
        }

        public static List<Student> GetAllStudents()
        {
            List<Student> students;

            using(LMSContext db = new LMSContext())
            {
                students = db.Students.ToList();
            }
            return students;
        }

        public static bool AddUser(User user)
        {
            try
            {
                using (LMSContext db = new LMSContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean AddStudent(Student student)
        {
            try
            {
                using(LMSContext db = new LMSContext())
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static bool DeleteUser(int userId)
        {
            try
            {
                Boolean flag = false;
                using (LMSContext db = new LMSContext())
                {
                    User? user = db.Users.Find(userId);
                    if (user != null)
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();
                        flag = true;
                    }
                }
                return flag;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteStudent(int studentId)
        {
            try
            {
                Boolean flag = false;
                using (LMSContext db = new LMSContext())
                {
                    Student? student = db.Students.Find(studentId);
                    if (student != null)
                    {
                        db.Students.Remove(student);
                        db.SaveChanges();
                        flag = true;
                    }
                }
                return flag;
            }
            catch
            {
                return false;
            }
        }

        public static bool EditUser(User user)
        {
            try
            {
                using (LMSContext db = new LMSContext())
                {
                    var existingUser = db.Users.FirstOrDefault(u => u.Id == user.Id);

                    if (existingUser != null)
                    {
                        existingUser.UserName = user.UserName;
                        existingUser.Password = user.Password;
                        existingUser.PhoneNumber = user.PhoneNumber;
                        existingUser.Email = user.Email;
                        existingUser.Role = user.Role;

                        db.SaveChanges();
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool EditStudent(Student student)
        {
            try
            {
                using (LMSContext db = new LMSContext())
                {
                    var existingStudent = db.Students.FirstOrDefault(s => s.Id == student.Id);

                    if (existingStudent != null)
                    {
                        existingStudent.UserName = student.UserName;
                        existingStudent.Email = student.Email;
                        existingStudent.Session = student.Session;
                        existingStudent.RollNumber = student.RollNumber;

                        db.SaveChanges();
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
