using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using Tascii.Server.Models;
namespace Tascii.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] //Sets the initial url for the route. https:localhost:7286/api/Home/
    public class HomeController : ControllerBase
    {
        //Each method is an API endpoint with excpetions for GetCurrentUser() and the constructor.
        private readonly TasciiDBContext _context;

        [NonAction]
        public User? GetCurrentUser()
        {
            return _context.Users.Find(HttpContext.Session.GetInt32("UserId"));
        } //Method to get userId from Session data.

        public HomeController(TasciiDBContext context)
        {
            _context = context;
        } //Constructor

        [HttpPost("Login")]
        public IActionResult Login(string username, string password)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Name == username); //Runs a query to find a username that matches the input. Returns the entire Row as a User object
            if (user != null)
            {//User Not Found
                return NotFound();
            }
            else if (password != user.Password)
            {//Password is incorrect
                return BadRequest();
            } 
            else
            { //Password matches and user is valid
                HttpContext.Session.SetInt32("UserId", user.Id); //Set the user ID in session data.
                return Ok();
            }

        } //Log in - Post -  Take data from form and Save the user in Session data if valid. Send to Board List

        [HttpPost("Logout")]
        public IActionResult Logout(loginContext login)
        {
            HttpContext.Session.Clear();
            return Ok();
        } //Logs out the current User and by removing their data from Session

        [HttpGet("redirectTo/{Route}")]
        public IActionResult RedirectToReact(string route)
        {
            return Ok("localhost:5173/" + route);
        } //Route Method if needed.

        //Get User List
        [HttpGet("GetBoards")]
        public IActionResult GetBoards()
        {
            User CurrentUser = GetCurrentUser();
            
            if (CurrentUser.Name == null)
            {
                return NotFound();
            }
            var boardList = (from bd in _context.Boards where bd.Id == CurrentUser.Id select bd).ToList();

            return Ok(boardList);
            
        } //Gets a list of boards owned by the current user in Session

        [HttpGet("GetNotes")]
        public IActionResult GetNotes([FromBody] GetNotesContextModel model)
        {
            User? CurrentUser = GetCurrentUser();
            if (CurrentUser == null)
            {
                return NotFound();
            }
            Boards CurrentBoard = _context.Boards.Find(model.BoardId);
            if (CurrentBoard.OwnerId != CurrentUser.Id)
            {
                return BadRequest();
            }

            var noteList = (from notes in _context.Notes where notes.BoardId == model.BoardId select notes).ToList();
            var JSnoteList = new List<JSNote>();
            int count = 1;
            //Convert to note list with JSID (Javascript ID) values
            foreach (var note in noteList)
            {
                JSNote jSNote = new JSNote()
                {
                    BoardId = note.BoardId,
                    yCoord = note.yCoord,
                    xCoord = note.xCoord,
                    content = note.content,
                    DBID = note.Id,
                    JSID = count
                };
                count++;
                JSnoteList.Add(jSNote);
            }
            
            HttpContext.Session.SetString("OriginalNoteListContext", JsonSerializer.Serialize(JSnoteList)); 
            //Serializes and stores the context for the original list as a string. Will need deserialized to use again.
            return Ok(new JsonResult(new {NoteData = noteList }));
        } //Get all notes related to a board and save the boardID state in session data.

        //Save Notepad data
        [HttpPost("SaveBoard")]
        public IActionResult SaveBoard([FromBody] SaveBoardContextModel model)
        {
            User currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return BadRequest();
            }
            List<JSNote> SessionList = JsonSerializer.Deserialize<List<JSNote>>(HttpContext.Session.GetString("OriginalNoteListContext"));
            List<JSNote> IncomingList = model.notes;

            List<Note> UpdateList = new List<Note>();
            List<Note> DeleteList = new List<Note>();
            List<Note> CreateList = new List<Note>();

            foreach(JSNote i in IncomingList)
            {
                bool present = false;
                foreach(JSNote s in SessionList)
                {
                    if (i.JSID == s.JSID) 
                    {
                        UpdateList.Add(new Note {
                            Id = i.DBID,
                            xCoord = i.xCoord,
                            yCoord = i.yCoord,
                            content = i.content,
                            BoardId = i.BoardId
                        });
                        present = true;
                    } //Does the JSID match one that existed previously 
                    else if (i.JSID == 0) 
                    {
                        CreateList.Add(new Note
                        {
                            Id = i.DBID,
                            xCoord = i.xCoord,
                            yCoord = i.yCoord,
                            content = i.content,
                            BoardId = i.BoardId
                        });
                        present = true;
                    } //A JSID of 0 indicates a new Note was Made
                } 
                if (present == false)
                {
                    DeleteList.Add(new Note
                    {
                        Id = i.DBID,
                        xCoord = i.xCoord,
                        yCoord = i.yCoord,
                        content = i.content,
                        BoardId = i.BoardId
                    });
                } //Incoming JSID does not match any Session JSID so the note was deleted.
            }
            //Carry out the coresponding options based on the now sorted lists.

            foreach(Note n in UpdateList)
            {
                Note updatedNote = _context.Notes.Single(b => b.Id == n.Id); //Find a note related to the id
                if (updatedNote != null)
                {
                    updatedNote.content = n.content;
                    updatedNote.xCoord = n.xCoord;
                    updatedNote.yCoord = n.yCoord;
                }
            }

            foreach (Note n in CreateList)
            {
                _context.Notes.Add(n);
            }

            foreach (Note n in DeleteList)
            {
                _context.Notes.Remove(n);
            }

            _context.SaveChanges();
            return Ok(); //Return Ok to indicate success

        }
        

        
        [HttpPost("NewUser")]
        public IActionResult MakeUser([FromBody] MakeUserContextModel model)
        {
            User newUser = null;
            newUser.Password = model.Password;
            newUser.Name = model.UserName;
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok();
        }//POST create new user

        [HttpPost("NewBoard")]
        public IActionResult MakeBoard([FromBody] MakeBoardContextModel model)
        {
            User currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return BadRequest();
            }
            Boards newBoard = null;
            newBoard.Name = model.name;
            newBoard.OwnerId = currentUser.Id;
            _context.Boards.Add(newBoard);
            _context.SaveChanges();
            return Ok();
        }//POST - Create a new board using a input name and CurrentUser as the owner
    }
}
