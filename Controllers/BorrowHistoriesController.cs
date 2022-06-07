using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibApp.Models;
using PagedList;

namespace LibApp.Controllers
{
    public class BorrowHistoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        List<Book> books = new List<Book>();
        List<Customer> customers = new List<Customer>();
        List<BorrowHistory> bor = new List<BorrowHistory>();

    public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            var borrowView = (from s in db.BorrowHistories
                      join sa in db.Books on s.BookId equals sa.BookId into st2
                      from sa in st2.DefaultIfEmpty()
                      join cu in db.Customers on s.CustomerId equals cu.CustomerId into st3
                      from cu in st3.DefaultIfEmpty()
                      select new BorrowView
                      {
                         
                         Books = sa,
                         Customers = cu,
                         borrowHistories = s
                      });

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                borrowView = borrowView.Where(cu => cu.Customers.Imie.Contains(searchString)||cu.Books.Tytul.Contains(searchString));   //Contains(searchString));
            }

           

            return View(borrowView);
            //return View(db.BorrowHistories.ToList());
        }
    // GET: BorrowHistories/Create
    public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var borrowHistory = new BorrowHistory { BookId = book.BookId, BorrowDate = DateTime.Now, Book = book };
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Imie");
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BorrowHistoryId,BookId,CustomerId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                db.BorrowHistories.Add(borrowHistory);
                db.SaveChanges();
                return RedirectToAction("Index", "Books");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Imie", borrowHistory.CustomerId);
            borrowHistory.Book = db.Books.Find(borrowHistory.BookId);
            return View(borrowHistory);
        }

        // GET: BorrowHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowHistory borrowHistory = db.BorrowHistories
                .Include(b => b.Book)
                .Include(c => c.Customer)
                .Where(b => b.BookId == id && b.ReturnDate == null)
                .FirstOrDefault();
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BorrowHistoryId,BookId,CustomerId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                var borrowHistoryItem = db.BorrowHistories.Include(i => i.Book)
                    .FirstOrDefault(i => i.BorrowHistoryId == borrowHistory.BorrowHistoryId);
                if (borrowHistoryItem == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                borrowHistoryItem.ReturnDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index", "Books");
            }
            return View(borrowHistory);
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
