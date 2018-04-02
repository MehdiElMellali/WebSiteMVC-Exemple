namespace JobOffersSOA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableApplyforjob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyForJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        applayDate = c.DateTime(nullable: false),
                        jobId = c.Int(nullable: false),
                        UserId = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Jobs", t => t.jobId, cascadeDelete: true)
                .Index(t => t.jobId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyForJobs", "jobId", "dbo.Jobs");
            DropForeignKey("dbo.ApplyForJobs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplyForJobs", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplyForJobs", new[] { "jobId" });
            DropTable("dbo.ApplyForJobs");
        }
    }
}
