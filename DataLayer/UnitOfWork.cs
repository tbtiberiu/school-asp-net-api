using DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork
    {
        public StudentsRepository Students { get; }
        public ClassRepository Classes { get; }
        public GradesRepository Grades { get; }
        public UsersRepository Users { get; }

        private readonly AppDbContext _dbContext;

        public UnitOfWork
        (
            AppDbContext dbContext,
            StudentsRepository studentsRepository,
            ClassRepository classesRepository,
            GradesRepository gradesRepository,
            UsersRepository usersRepository
        )
        {
            _dbContext = dbContext;
            Students = studentsRepository;
            Classes = classesRepository;
            Grades = gradesRepository;
            Users = usersRepository;
        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                var errorMessage = "Error when saving to the database: "
                    + $"{exception.Message}\n\n"
                    + $"{exception.InnerException}\n\n"
                    + $"{exception.StackTrace}\n\n";

                Console.WriteLine(errorMessage);
            }
        }
    }
}
