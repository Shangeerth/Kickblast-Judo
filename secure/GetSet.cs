using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Programming_Assigment.Database
{
    internal class Athleteclz
    {

        // Athlete properties
        public int AthleteID { get; private set; }
        public string Name { get; private set; }
        public int Weight { get; private set; }
        public int Height { get; private set; }
        public string Address { get; private set; }
        public string NIC { get; private set; }
        public int Contact { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public int categoryId { get; private set; }
        public string CategoryName { get; private set; }





        public Athleteclz(int athleteID, string name, int weight, int height, string address, string nic, int contact, DateTime dob, int categoryIds, string categoryName)
        {
            AthleteID = athleteID;
            Name = name;
            Weight = weight;
            Height = height;
            Address = address;
            NIC = nic;
            Contact = contact;
            DateOfBirth = dob;
            categoryId = categoryIds;
            CategoryName = categoryName;
        }

    }

    internal class Trainergetset
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Qualification { get; private set; }
        public string NIC { get; private set; }
        public string Contact { get; private set; }
        public int Experience { get; private set; }
        public decimal Salary { get; private set; }


        public Trainergetset(int id, string name, int age, string qualification, string nic, string contact, int experience, decimal salary)
        {
            Id = id;
            Name = name;
            Age = age;
            Qualification = qualification;
            NIC = nic;
            Contact = contact;
            Experience = experience;
            Salary = salary;
        }



    }

    public class PrivateCoaching
    {
        public int PrivateCoachingID { get; private set; }

        public DateTime CoachingDate { get; private set; }

        public int HoursPerWeek { get; private set; }

        public decimal FeesPerHour { get; private set; }

        public int AthleteID { get; private set; }

        public int TrainerID { get; private set; }

        // Constructor for full initialization (e.g., when reading from DB)
        public PrivateCoaching(int privateCoachingID, DateTime coachingDate, int hoursPerWeek, decimal feesPerHour, int athleteID, int trainerID)
        {
            PrivateCoachingID = privateCoachingID;
            CoachingDate = coachingDate;
            HoursPerWeek = hoursPerWeek;
            FeesPerHour = feesPerHour;
            AthleteID = athleteID;
            TrainerID = trainerID;
        }


    }



    internal class weight
    {
        public int CategoryID { get; private set; }
        public string Name { get; private set; }
        public int MinWeight { get; private set; }
        public int? MaxWeight { get; private set; }

        public weight(int categoryId, string name, int minWeight, int? maxWeight)
        {
            CategoryID = categoryId;
            Name = name;
            MinWeight = minWeight;
            MaxWeight = maxWeight;
        }
    }

    public class Plan
    {
        public int PlanID { get; private set; }
        public string Name { get; private set; }
        public decimal WeeklyFee { get; private set; }
        public int SessionsPerWeek { get; private set; }

        public Plan(int planID, string name, decimal weeklyFee, int sessionsPerWeek)
        {
            PlanID = planID;
            Name = name;
            WeeklyFee = weeklyFee;
            SessionsPerWeek = sessionsPerWeek;
        }


    }
    public class AthleteTrainingPlan
    {
        public int ID { get; private set; }
        public DateTime SessionDate { get; private set; }
        public string Time { get; private set; }
        public int AthleteID { get; private set; }
        public int PlanID { get; private set; }
        public decimal TrainingPlanFee { get; private set; }

        // Optional: Constructor to initialize
        public AthleteTrainingPlan(int id, DateTime sessionDate, string time, int athleteId, int planId, decimal trainingPlanFee)
        {
            ID = id;
            SessionDate = sessionDate;
            Time = time;
            AthleteID = athleteId;
            PlanID = planId;
            TrainingPlanFee = trainingPlanFee;
        }
        public class Athelan
        {
            public int ID { get; private set; }
            public DateTime SessionDate { get; private set; }
            public string Time { get; private set; }
            public int AthleteID { get; private set; }
            public string AthleteName { get; private set; }
            public int PlanID { get; private set; }
            public string PlanName { get; private set; }
            public int SessionsPerWeek { get; private set; }
            public decimal WeeklyFee { get; private set; }

            public int athletesession { get; private set; }

            public Athelan(int id, DateTime sessionDate, string time, int athleteSession, int athleteId, string athleteName, int planId, string planName, int sessionsPerWeek, decimal weeklyFee)
            {
             
                   
                ID = id;
                SessionDate = sessionDate;
                Time = time;
                AthleteID = athleteId;
                athletesession = athleteSession;
                AthleteName = athleteName;
                PlanID = planId;
                PlanName = planName;
                SessionsPerWeek = sessionsPerWeek;
                WeeklyFee = weeklyFee;
                
            }
        }

        public class Competitionget
        {
            public int CompetitionID { get; private set; }
            public DateTime CompetitionDate { get; private set; }
            public string CompetitionTime { get; private set; }
            public decimal CompetitionFee { get; private set; }
            public string CompetitionName { get; private set; }

            public Competitionget(int competitionID, DateTime competitionDate, string competitionTime, decimal competitionFee, string competitionName)
            {
                CompetitionID = competitionID;
                CompetitionDate = competitionDate;
                CompetitionTime = competitionTime;
                CompetitionFee = competitionFee;
                CompetitionName = competitionName;
            }

        }

        public class AthleteCompetitionDetails
        {
            public int AthleteCompetitionID { get; private set; }
            public int AthleteID { get; private set; }
            public string AthleteName { get; private set; }
            public string Trainingplan { get; private set; }
            public int CompetitionID { get; private set; }
            public string CompetitionName { get; private set; }
            public DateTime CompetitionDate { get; private set; }
            public string CompetitionTime { get; private set; }

            public decimal fees { get; private set; }

            public AthleteCompetitionDetails(
                int athleteCompetitionID,
                int athleteID,
                string athleteName,
                string weightCategory,
                int competitionID,
                string competitionName,
                DateTime competitionDate,
                string competitionTime,
                decimal fee)
            {
                AthleteCompetitionID = athleteCompetitionID;
                AthleteID = athleteID;
                AthleteName = athleteName;
                Trainingplan = weightCategory;
                CompetitionID = competitionID;
                CompetitionName = competitionName;
                CompetitionDate = competitionDate;
                CompetitionTime = competitionTime;
                fees = fee;
            }
        }


        public class AthleteCompetitionInfo
        {
            public string AthleteName { get; set; }
            public decimal CompetitionFee { get; set; }
            public DateTime CompetitionDate { get; set; }

            // Constructor
            public AthleteCompetitionInfo(string athleteName, decimal competitionFee, DateTime competitionDate)
            {
                AthleteName = athleteName;
                CompetitionFee = competitionFee;
                CompetitionDate = competitionDate;
            }
        }



    }
}
