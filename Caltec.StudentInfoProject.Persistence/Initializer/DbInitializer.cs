using Caltec.StudentInfoProject.Domain;
using System.Security.Cryptography;

namespace Caltec.StudentInfoProject.Persistence.Initializer
{
    public static class DbInitializer
    {
        private static readonly int _nbStudents = 1000;
        private static readonly int _nbDegrees = 20;
        private static readonly int _studentPerClasses = 25;

        public static void Initialize(StudentInfoDbContext context)
        {
            context.Database.EnsureCreated();

            var degrees = CreateDegrees(_nbDegrees);
            var students = CreateStudents(_nbStudents);
            var classes = CreateClassesAndFees(_studentPerClasses, students, degrees);

            context.StudentClasses.AddRange(classes);

            context.SaveChanges();
        }

        private static List<Degree> CreateDegrees(int nbDegree)
        {
            List<Degree> degrees = new List<Degree>();

            for (int i = 0; i < nbDegree; i++)
            {
                degrees.Add(new Degree()
                {
                    Name = MockingData.Degrees[NextInt(0, MockingData.Degrees.Count)] + i.ToString(),
                    NbYear = NextInt(1, 5),
                    FeesPerYearPerStudent = NextInt(1000, 10000),
                    NbPayment = NextInt(1, 12)
                });
            }

            return degrees;
        }

        private static List<Student> CreateStudents(int nbStudent)
        {
            List<Student> students = new List<Student>();

            for (int i = 0; i < nbStudent; i++)
            {
                var firstName = MockingData.FirstNames[NextInt(0, MockingData.FirstNames.Count)];
                var lastName = MockingData.LastNames[NextInt(0, MockingData.LastNames.Count)];
                var email = $" {firstName.Replace(" ", string.Empty)}.{lastName.Replace(" ", string.Empty)}@caltech.com";
                students.Add(new Student()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = MockingData.Phones[NextInt(0, MockingData.Phones.Count)],
                    Address = MockingData.Addresses[NextInt(0, MockingData.Addresses.Count)],
                    City = MockingData.Cities[NextInt(0, MockingData.Cities.Count)],
                    Country = MockingData.Countries[NextInt(0, MockingData.Countries.Count)]
                });
            }

            return students;
        }

        private static List<StudentClass> CreateClassesAndFees(int nbStudentPerClass, List<Student> students, List<Degree> degrees)
        {
            List<StudentClass> classes = new List<StudentClass>();

            int countStudentPerClass = 0;
            int classCount = 0;
            Degree currentDegree = degrees[NextInt(0, degrees.Count)];
            var currentClass = new StudentClass()
            {
                Degree = degrees[NextInt(0, degrees.Count)],
                Name = $"Class {currentDegree.Name} {classCount}",
            };
            foreach (var s in students)
            {
                currentClass.Students.Add(s);
                s.Fees = new List<SchoolFees>();
                for (int i = 1; i < currentDegree.NbPayment; i++)
                {
                    s.Fees.Add(new SchoolFees()
                    {
                        Class = currentClass,
                        Amount = currentDegree.FeesPerYearPerStudent / currentDegree.NbPayment,
                        PaymentDate = DateTime.Now.AddMonths(i),
                        PaymentMethod = MockingData.PaymentMethods[NextInt(0, MockingData.PaymentMethods.Count)],
                        PaymentReference = "Ref" + i.ToString(),
                        PaymentNote = "Note" + i.ToString(),
                        PaymentStatus = MockingData.PaymentStatus[NextInt(0, MockingData.PaymentStatus.Count)]
                    });
                }

                if (countStudentPerClass == nbStudentPerClass)
                {
                    classes.Add(currentClass);
                    countStudentPerClass = 0;
                    classCount++;
                    currentClass = new StudentClass()
                    {
                        Degree = degrees[NextInt(0, degrees.Count)],
                        Name = $"Class {currentDegree.Name} {classCount}",
                    };
                }

                countStudentPerClass++;
            }

            return classes;
        }

        private static int NextInt(int minValue, int maxValue)
        {
            return RandomNumberGenerator.GetInt32(minValue, maxValue);
        }
    }
}
