using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using Vereyon.Web;

namespace LibraryManagementSystem.Controllers
{
    public class BorrowsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Borrows
        public async Task<ActionResult> Index()
        {
            var borrows = db.Borrows.Include(b => b.Book);
            return View(await borrows.ToListAsync());
        }

        // GET: Borrows/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = await db.Borrows.FindAsync(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            return View(borrow);
        }

        // GET: Borrows/Create
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.Books, "BookId", "BookTitle");
            return View();
        }

        // POST: Borrows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BorrowId,MemberNo,BookId,Author,Publisher")] Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                db.Borrows.Add(borrow);
                await db.SaveChangesAsync();
                FlashMessage.Confirmation("The book has been borrowed by member whose number is: ", borrow.MemberNo);
                //FlashMessage.Confirmation("The book has been borrowed");
                return RedirectToAction("Create");
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "BookTitle", borrow.BookId);
            return View(borrow);
        }

        public JsonResult IsMemberNotExists(string memberNo)
        {
            var members = db.Members.ToList();
            if (!members.Any(t => t.MemberNo.ToLower() == memberNo.ToLower()))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ReturnBook()
        {
            ViewBag.BookId = new SelectList(db.Books, "BookId", "BookTitle");
            return View();
        }

        [HttpPost]
        public ActionResult ReturnBook([Bind(Include = "BorrowId,MemberNo,BookId,Author,Publisher, IsReturn")] Borrow borrow)
        {

            ViewBag.BookId = new SelectList(db.Books, "BookId", "BookTitle");
            return View();
        }


        // GET: Borrows/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = await db.Borrows.FindAsync(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "BookTitle", borrow.BookId);
            return View(borrow);
        }

        // POST: Borrows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BorrowId,MemberNo,BookId,Author,Publisher")] Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrow).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "BookTitle", borrow.BookId);
            return View(borrow);
        }


        public JsonResult GetBookById(int bookId)
        {
            var book = db.Books.FirstOrDefault(s => s.BookId == bookId);
            return Json(book);
        }


        public JsonResult GetBooksByMemberNo(string memberNo)
        {
            var books = db.Borrows.Where(m => m.MemberNo == memberNo).ToList();
            return Json(books);
        }



        // GET: Borrows/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = await db.Borrows.FindAsync(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            return View(borrow);
        }

        // POST: Borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Borrow borrow = await db.Borrows.FindAsync(id);
            db.Borrows.Remove(borrow);
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
