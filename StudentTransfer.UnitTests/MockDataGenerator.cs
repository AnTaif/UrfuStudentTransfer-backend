using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Entities.Auth;
using StudentTransfer.Dal.Entities.User;
using StudentTransfer.Dal.Enums;
using StudentTransfer.Utils;

namespace StudentTransfer.UnitTests;

public static class MockDataGenerator
{
    public static List<ApplicationEntity> GenerateUserApplications(AppUser user, int count = 1)
    {
        var applications = new List<ApplicationEntity>();
        var direction = GenerateDirection();
        
        for (var i = 0; i < count; i++)
        {
            var application = new ApplicationEntity
            {
                Type = ApplicationType.Recovery,
                DetailedType = ApplicationDetailedType.RecoveryToBudget,
                AppUserId = user.Id,
                User = user,
                CurrentStatus = Status.Sent,
                StatusUpdates = new List<ApplicationStatus>() {new ApplicationStatus
                    {
                        Status = Status.Sent,
                        Comment = "sent",
                        Date = DateTime.UtcNow
                    }
                },
                InitialDate = DateTime.UtcNow,
                Direction = direction,
                IsActive = true
            };
            
            applications.Add(application);
        }

        return applications;
    }

    public static Direction GenerateDirection()
    {
        var guid = Guid.NewGuid().ToString();
        
        return new Direction
        {
            Code = guid,
            Name = guid,
            Level = EducationLevel.Bachelor,
            Course = 1,
            Form = EducationForm.FullTime
        };
    }
}