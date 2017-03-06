using Hangfire;
using Library.App.Helper;
using Library.Data.Models;
using Library.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.App.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly Lazy<IUnitofWork> _unitofWork;
        public BookController(Lazy<IUnitofWork> unitofWork)
        {
            _unitofWork = unitofWork;
        }
        private IUnitofWork UnitofWork => _unitofWork.Value;
        // GET: Book
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> Detail(int id)
        {
            var bookHistory = UnitofWork.HistoryRepository.GetBookHistory(id); 
            return View(bookHistory);
        }

        public async Task<JsonResult> FilterBooks(int Id)
        {
            List<Book> books = new List<Book>();
            switch(Id)
            {
                case 1:
                    books = UnitofWork.BookRepository.GetAvailableBooks().ToList();
                    break;
                case 2:
                    var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                    var currentUserId = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Single().Value;
                    int userId = -1;
                    int.TryParse(currentUserId, out userId);
                    books = UnitofWork.BookRepository.GetUserBooks(userId).ToList();
                    break;
                default:
                    books = UnitofWork.BookRepository.GetAllBooks().ToList();
                    break;
            } 
            var _partialView = Extensions.RenderViewToString(this.ControllerContext, "_tableData", books);
            return Json(new { partialView = _partialView }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Reminder()
        {
            try
            {
                List<string> books = null;
                string bookDataFormat = "Book Name: {0} <br/>   Borrow Date: {1} <br/>  End Borrow Date: {2}";
                var usersBooks = UnitofWork.UserBookRepository.GetUsersBooks();
                foreach (var userBooks in usersBooks)
                {
                    books = new List<string>();
                    foreach (var bookData in userBooks.Books)
                    {
                        books.Add(string.Format(bookDataFormat, bookData.BookName, bookData.BorrowDate.Date.ToString("dd/MM/yyyy"), bookData.EndBorrowDate.Date.ToString("dd/MM/yyyy")));
                    }
                    BackgroundJob.Enqueue(() => Help.SendMail(userBooks.User.Email, userBooks.User.FullName, books));
                }
            }
            catch(Exception ex)
            {
                return Json(new { result = "fail" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}