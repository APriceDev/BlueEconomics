namespace BlueEconomics.Platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FAQ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FAQ_Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        OccupationId = c.Int(nullable: false),
                        FAQ_QuestionSource_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Occupations", t => t.OccupationId, cascadeDelete: true)
                .ForeignKey("dbo.FAQ_QuestionSource", t => t.FAQ_QuestionSource_Id)
                .Index(t => t.OccupationId)
                .Index(t => t.FAQ_QuestionSource_Id);
            
            CreateTable(
                "dbo.FAQ_Response",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        FAQ_ResponseSource_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FAQ_ResponseSource", t => t.FAQ_ResponseSource_Id)
                .Index(t => t.FAQ_ResponseSource_Id);
            
            CreateTable(
                "dbo.FAQ_ResponseSource",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Organization = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FAQ_QuestionSource",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FAQ_ResponseFAQ_Question",
                c => new
                    {
                        FAQ_Response_Id = c.Int(nullable: false),
                        FAQ_Question_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FAQ_Response_Id, t.FAQ_Question_Id })
                .ForeignKey("dbo.FAQ_Response", t => t.FAQ_Response_Id, cascadeDelete: true)
                .ForeignKey("dbo.FAQ_Question", t => t.FAQ_Question_Id, cascadeDelete: true)
                .Index(t => t.FAQ_Response_Id)
                .Index(t => t.FAQ_Question_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.FAQ_ResponseFAQ_Question", new[] { "FAQ_Question_Id" });
            DropIndex("dbo.FAQ_ResponseFAQ_Question", new[] { "FAQ_Response_Id" });
            DropIndex("dbo.FAQ_Response", new[] { "FAQ_ResponseSource_Id" });
            DropIndex("dbo.FAQ_Question", new[] { "FAQ_QuestionSource_Id" });
            DropIndex("dbo.FAQ_Question", new[] { "OccupationId" });
            DropForeignKey("dbo.FAQ_ResponseFAQ_Question", "FAQ_Question_Id", "dbo.FAQ_Question");
            DropForeignKey("dbo.FAQ_ResponseFAQ_Question", "FAQ_Response_Id", "dbo.FAQ_Response");
            DropForeignKey("dbo.FAQ_Response", "FAQ_ResponseSource_Id", "dbo.FAQ_ResponseSource");
            DropForeignKey("dbo.FAQ_Question", "FAQ_QuestionSource_Id", "dbo.FAQ_QuestionSource");
            DropForeignKey("dbo.FAQ_Question", "OccupationId", "dbo.Occupations");
            DropTable("dbo.FAQ_ResponseFAQ_Question");
            DropTable("dbo.FAQ_QuestionSource");
            DropTable("dbo.FAQ_ResponseSource");
            DropTable("dbo.FAQ_Response");
            DropTable("dbo.FAQ_Question");
        }
    }
}
