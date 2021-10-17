using Support_Chat_App.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Support_Chat_App.Data.Helpers
{
    public static class DataFeeder
    {
        /// <summary>
        /// Seed Initial Data Set
        /// </summary>
        /// <param name="context"></param>
        public static void SeedData(SupportChatContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            SeedUserTypes(context);
            SeedAgentTypes(context);
            SeedShiftTypes(context);
            SeedTeamTypes(context);
            SeedUsers(context);
        }

        /// <summary>
        /// Seed initial data set to user type table
        /// </summary>
        /// <param name="context"></param>
        private static void SeedUserTypes(SupportChatContext context)
        {
            var userTypeList = new List<UserType>
            {
                new UserType { Name = "Client" },
                new UserType { Name = "Agent" },
                new UserType { Name = "Admin" }
            };
            context.UserTypes.AddRange(userTypeList);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed initial data set to shift type table
        /// </summary>
        /// <param name="context"></param>
        private static void SeedShiftTypes(SupportChatContext context)
        {
            var shiftTypeList = new List<ShiftType>
            {
                new ShiftType { Name = "Morning" },
                new ShiftType { Name = "Evening" },
                new ShiftType { Name = "Night" }
            };
            context.ShiftTypes.AddRange(shiftTypeList);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed initial data set to team type table
        /// </summary>
        /// <param name="context"></param>
        private static void SeedTeamTypes(SupportChatContext context)
        {
            var teamTypeList = new List<TeamType>
            {
                new TeamType { Name = "A", ShiftTypeId = 1 },
                new TeamType { Name = "B", ShiftTypeId = 2 },
                new TeamType { Name = "C", ShiftTypeId = 3 },
                new TeamType { Name = "Overflow", ShiftTypeId = 4 },
            };
            context.TeamTypes.AddRange(teamTypeList);
            context.SaveChanges();
        }     

        /// <summary>
        /// Seed initial data set to agent type table
        /// </summary>
        /// <param name="context"></param>
        private static void SeedAgentTypes(SupportChatContext context)
        {
            var userAgentTypeList = new List<AgentSeniorityType>
            {
                new AgentSeniorityType
                {
                    Name = "Junior",
                    SeniorityMultiplier = 0.4
                },
                new AgentSeniorityType
                {
                    Name = "Mid-Level",
                    SeniorityMultiplier = 0.6
                },
                new AgentSeniorityType
                {
                    Name = "Senior",
                    SeniorityMultiplier = 0.8
                },
                new AgentSeniorityType
                {
                    Name = "Team Lead",
                    SeniorityMultiplier = 0.5
                }
            };
            context.AgentSeniorityTypes.AddRange(userAgentTypeList);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed initial data set to users table
        /// </summary>
        /// <param name="context"></param>
        private static void SeedUsers(SupportChatContext context)
        {
            var userList = new List<User>
            {
                new User
                {
                    Email = "admin@supportchat.com",
                    Password = "admin",
                    UserTypeId = 3,
                    AgentSeniorityTypeId = 1,
                },
                new User
                {
                    Email = "john@supportchat.com",
                    Password = "john",
                    UserTypeId = 1,
                    AgentSeniorityTypeId = 1
                },
                //Team A
                new User
                {
                    Email = "teamA1@supportchat.com",
                    Password = "teamA1",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 4,
                    TeamTypeId = 1
                },
                new User
                {
                    Email = "teamA2@supportchat.com",
                    Password = "teamA2",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 2,
                    TeamTypeId = 1
                },
                new User
                {
                    Email = "teamA3@supportchat.com",
                    Password = "teamA3",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 2,
                    TeamTypeId = 1
                },
                new User
                {
                    Email = "teamA4@supportchat.com",
                    Password = "teamA4",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 1,
                    TeamTypeId = 1
                },
                //Team B
                new User
                {
                    Email = "teamB1@supportchat.com",
                    Password = "teamB1",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 3,
                    TeamTypeId = 2
                },
                new User
                {
                    Email = "teamB2@supportchat.com",
                    Password = "teamB2",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 2,
                    TeamTypeId = 2
                },
                new User
                {
                    Email = "teamB3@supportchat.com",
                    Password = "teamB3",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 1,
                    TeamTypeId = 2
                },
                new User
                {
                    Email = "teamB4@supportchat.com",
                    Password = "teamB4",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 1,
                    TeamTypeId = 2
                },
                //Team C
                new User
                {
                    Email = "teamC1@supportchat.com",
                    Password = "teamC1",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 2,
                    TeamTypeId = 3
                },
                new User
                {
                    Email = "teamC2@supportchat.com",
                    Password = "teamC2",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 2,
                    TeamTypeId = 3
                },
                //Team Overflow
                new User
                {
                    Email = "ovflw1@supportchat.com",
                    Password = "ovflw1",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 1,
                    TeamTypeId = 4
                },
                new User
                {
                    Email = "ovflw2@supportchat.com",
                    Password = "ovflw2",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 1,
                    TeamTypeId = 4
                },
                new User
                {
                    Email = "ovflw3@supportchat.com",
                    Password = "ovflw3",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 1,
                    TeamTypeId = 4
                },
                new User
                {
                    Email = "ovflw4@supportchat.com",
                    Password = "ovflw4",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 1,
                    TeamTypeId = 4
                },
                new User
                {
                    Email = "ovflw5@supportchat.com",
                    Password = "ovflw5",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 1,
                    TeamTypeId = 4
                },
                new User
                {
                    Email = "ovflw6@supportchat.com",
                    Password = "ovflw6",
                    UserTypeId = 2,
                    AgentSeniorityTypeId = 1,
                    TeamTypeId = 4
                },
            };
            context.Users.AddRange(userList);
            context.SaveChanges();
        }
    }
}
