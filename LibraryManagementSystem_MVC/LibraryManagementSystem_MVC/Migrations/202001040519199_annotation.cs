namespace LibraryManagementSystem_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class annotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "MemberNo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "MemberNo", c => c.Int(nullable: false));
        }
    }
}
