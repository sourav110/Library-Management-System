using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem_MVC.Models;
using Vereyon.Web;
using System.Data.Entity.Migrations;

namespace LibraryManagementSystem_MVC.Controllers
{
    public class BorrowBooksController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: BorrowBooks
        public async Task<ActionResult> Index()
        {
            var borrowBooks = db.BorrowBooks.Include(b => b.Book);
            return View(await borrowBooks.ToListAsync());
        }

        
        // GET: BorrowBooks/Create
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title");
            return View();
        }

        // POST: BorrowBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BorrowId,MemberNo,BookId,IsBorrowed")] BorrowBook borrowBook)
        {
            if (ModelState.IsValid)
            {
                borrowBook.IsBorrowed = true;

                db.BorrowBooks.Add(borrowBook);
                await db.SaveChangesAsync();
                FlashMessage.Confirmation("This book has been borrowed the member whose number is " + borrowBook.MemberNo);
                return RedirectToAction("Create");
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", borrowBook.BookId);
            return View(borrowBook);
        }

        public JsonResult IsMemberNotExists(string memberNo)
        {
            var members = db.Members.ToList();
            if (members.Any(m => m.MemberNo.ToLower() == memberNo.ToLower()))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBookById(int bookId)
        {
            var book = db.Books.FirstOrDefault(b => b.BookId == bookId);
            return Json(book);
        }

        public JsonResult GetBooksByMemberNo(string memberNo)
        {
            var borrowedBooks = db.BorrowBooks.Where(bb => bb.MemberNo == memberNo  && bb.IsBorrowed == true).ToList();
            return Json(borrowedBooks);
        }


        public ActionResult ReturnBook()
        {
            ViewBag.BookId = new SelectList(db.Books, "BookId", "BookTitle");
            return View();
        }

        [HttpPost]
        public ActionResult ReturnBook(BorrowBook borrow)
        {
            if (ModelState.IsValid)
            {
                BorrowBook borrowBook = new BorrowBook();
                borrowBook.BorrowId = borrow.BorrowId;
                borrowBook.MemberNo = borrow.MemberNo;
                borrowBook.BookId = borrow.BorrowId;
                borrowBook.IsBorrowed = false;

                db.BorrowBooks.AddOrUpdate(borrow);
                db.SaveChanges();
                FlashMessage.Confirmation("This book returned successfully");
                return RedirectToAction("ReturnBook");
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "BookTitle");
            return View();
        }


        // GET: BorrowBooks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowBook borrowBook = await db.BorrowBooks.FindAsync(id);
            if (borrowBook == null)
            {
                return HttpNotFound();
            }
            return View(borrowBook);
        }

        // POST: BorrowBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BorrowBook borrowBook = await db.BorrowBooks.FindAsync(id);
            db.BorrowBooks.Remove(borrowBook);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
