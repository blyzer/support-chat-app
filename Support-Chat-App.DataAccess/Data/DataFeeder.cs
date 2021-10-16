using Support_Chat_App.DataAccess.Entities;
using Support_Chat_App.Entities;
using System.Linq;

namespace Support_Chat_App.DataAccess.Data
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
            SeedUsers(context);
        }

        /// <summary>
        /// Seed initial data set to user type table
        /// </summary>
        /// <param name="context"></param>
        private static void SeedUserTypes(SupportChatContext context)
        {
            var userTypeList = new UserType[]
            {
                new UserType
                {
                   Name = "Client"
                },
                new UserType
                {
                    Name = "Agent"
                }
            };
            context.UserTypes.AddRange(userTypeList);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed initial data set to agent type table
        /// </summary>
        /// <param name="context"></param>
        private static void SeedAgentTypes(SupportChatContext context)
        {
            var userAgentTypeList = new AgentSeniorityType[]
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
            var userList = new User[]
            {
                new User
                {
                    Email = "admin@supportchat.com",
                    Password = "admin",
                    UserTypeId = 1,
                    AgentSeniorityTypeId = 1
                },
                new User
                {
                    Email = "admin2@supportchat.com",
                    Password = "admin",
                    UserTypeId = 1,
                    AgentSeniorityTypeId = 1
                }
            };
            context.Users.AddRange(userList);
            context.SaveChanges();
        }
    }
}
