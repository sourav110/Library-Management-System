namespace LibraryManagementSystem_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BorrowBooks", "Author");
            DropColumn("dbo.BorrowBooks", "Publisher");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BorrowBooks", "Publisher", c => c.String());
            AddColumn("dbo.BorrowBooks", "Author", c => c.String());
        }
    }
}
