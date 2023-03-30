using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private StudentRepository students;
        private SubjectRepository subjects;
        private UniversityRepository universities;

        public Controller()
        {
            students = new StudentRepository();
            subjects = new SubjectRepository();
            universities = new UniversityRepository();
        }
        
        public string AddStudent(string firstName, string lastName)
        {
            IStudent existingStudent = students.Models.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);

            if(existingStudent != null)
            {
                return String.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            int id = students.Models.Count+1;

            IStudent student = new Student(id, firstName, lastName);
            students.AddModel(student);

            return String.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, nameof(StudentRepository));
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if(subjectType != nameof(TechnicalSubject) &&
                subjectType != nameof(EconomicalSubject) &&
                subjectType!= nameof(HumanitySubject))
            {
                return string.Format(OutputMessages.SubjectTypeNotSupported,subjectType);
            }

            ISubject existingSubject = subjects.FindByName(subjectName);
            if(existingSubject != null)
            {
                return String.Format(OutputMessages.AlreadyAddedSubject,subjectName);
            }

            int id = subjects.Models.Count+1;
            ISubject newSubject = null!;

            if(subjectType == nameof(TechnicalSubject))
            {
                newSubject = new TechnicalSubject(id, subjectName);
            }
            else if(subjectType == nameof(EconomicalSubject))
            {
                newSubject = new EconomicalSubject(id, subjectName);
            }
            else if(subjectType == nameof(HumanitySubject))
            {
                newSubject = new HumanitySubject(id, subjectName);
            }


            subjects.AddModel(newSubject);
            return String.Format(OutputMessages.SubjectAddedSuccessfully,subjectType,subjectName,nameof(SubjectRepository));
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if(universities.FindByName(universityName)!= null)
            {
                return String.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }

            int id = universities.Models.Count+1;
            List<int> subjectIds = requiredSubjects.Select(s=> subjects.FindByName(s).Id).ToList();
            IUniversity newUniversity = new University(id, universityName, category, capacity, subjectIds);

            universities.AddModel(newUniversity);
            return String.Format(OutputMessages.UniversityAddedSuccessfully, universityName, nameof(UniversityRepository));
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            IStudent student = students.FindByName(studentName);
            string[] studentNames = studentName.Split(" ");
            if(student == null)
            {
                return String.Format(OutputMessages.StudentNotRegitered, studentNames[0], studentNames[1]);
            }

            IUniversity university = universities.FindByName(universityName);
            if(university == null)
            {
                return String.Format(OutputMessages.UniversityNotRegitered, universityName);
            }

            if(university.RequiredSubjects.Any(rs=> !student.CoveredExams.Contains(rs)))
            {
                return String.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
            }

            if(student.University != null && student.University.Name == universityName)
            {
                return String.Format(OutputMessages.StudentAlreadyJoined, studentNames[0], studentNames[1], universityName);
            }

            student.JoinUniversity(university);
            return String.Format(OutputMessages.StudentSuccessfullyJoined, studentNames[0], studentNames[1], universityName);
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = students.FindById(studentId);
            ISubject subject = subjects.FindById(subjectId);


            if(student == null)
            {
                return OutputMessages.InvalidStudentId;
            }

            if(subject == null)
            {
                return OutputMessages.InvalidSubjectId;
            }

            if(student.CoveredExams.Contains(subjectId))
            {
                return String.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);
            }

            student.CoverExam(subject);
            return String.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);
            int studentCount = students.Models.Where(s => s.University != null && s.University.Id == universityId).Count();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {studentCount}");
            sb.AppendLine($"University vacancy: {university.Capacity - studentCount}");

            return sb.ToString().TrimEnd();
        }
    }
}
