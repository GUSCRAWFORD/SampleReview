namespace SampleReview.DataDriver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalyzedItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReviewCount = c.Int(),
                        AverageRating = c.Int(),
                        LowestRating = c.Int(),
                        HighestRating = c.Int(),
                        Popularity = c.Int(),
                        Date = c.DateTimeOffset(precision: 7),
                        Name = c.String(nullable: false, maxLength: 50),
                        Icon = c.String(maxLength: 50),
                        Color = c.String(maxLength: 6),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Icon = c.String(maxLength: 50),
                        Color = c.String(maxLength: 6),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 280),
                        Rating = c.Int(nullable: false),
                        Reviewing = c.Int(nullable: false),
                        Date = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Reviewing)
                .Index(t => t.Reviewing);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Reviewing", "dbo.Items");
            DropIndex("dbo.Reviews", new[] { "Reviewing" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Items");
            DropTable("dbo.AnalyzedItems");
        }
    }
}
