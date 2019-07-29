namespace MovieRentalMVCApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataInUsersTable3 : DbMigration
    {
        public override void Up()
        {
            Sql(@" INSERT INTO[dbo].[AspNetUsers]([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'0d252522-0bac-4f3c-8b00-4ee7bf90a14a', N'ayushi@user.com', 0, N'ANWYtmltZab3j9nNZF7Gli2YYpOfMBwru4WDWAk8lzSk6W5E4bNuRhDqmgqCD60HJg==', N'91192bd9-338a-4719-9ad4-b09e2ab9e742', NULL, 0, 0, NULL, 1, 0, N'ayushi@user.com')
                   INSERT INTO[dbo].[AspNetUsers]([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled],[AccessFailedCount], [UserName]) VALUES(N'4833b7f0-ae6a-4958-bb55-b6ea2de80aaa', N'ayushi@admin.com', 0, N'AJh7514cxitl2w3md+4FBCDC/3Bw9P9FGGALmBCWjS1c1PVesAVMQbKE6+HxeFxE3Q==', N'662a940d-5b4c-4e20-b3e7-9a41c97dab93', NULL, 0, 0, NULL, 1, 0, N'ayushi@admin.com')
                   INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'e9482b67-9094-4876-a10d-823f3006bb3c', N'Admin')
                   INSERT INTO[dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES(N'4833b7f0-ae6a-4958-bb55-b6ea2de80aaa', N'e9482b67-9094-4876-a10d-823f3006bb3c')
                 ");
        }
        
        public override void Down()
        {
        }
    }
}
