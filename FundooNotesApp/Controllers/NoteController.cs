using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Linq;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness notebusiness;
        private readonly ILogger<NoteController> logger;
        public NoteController(INoteBusiness notebusiness,ILogger<NoteController> logger)
        {
            this.notebusiness = notebusiness;
            this.logger = logger;
        }
        [HttpPost("AddNote")]
        public IActionResult AddNote(NoteModel note)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
            var noteCreate=notebusiness.AddNote(note, userId); 
            if(noteCreate != null)
            {
                logger.LogInformation("Note Added Successfully");
                return Ok(new ResponseModel<NoteEntity> { Status=true,Message="note added successfully",Data=noteCreate});
            }
            else
            {
                logger.LogError("note adding failed");
                return BadRequest(new ResponseModel<NoteEntity> { Status=false,Message="failed"});
            }
        }
        [HttpGet("getallnotes")]
        public IActionResult GetAllNotes()
        {
            return Ok(notebusiness.GetAllNotes());
        }
        [HttpGet]
        public IActionResult GetAllNotesbyid()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
            return Ok(notebusiness.GetAllNotesbyid(userId));
        }
        [HttpDelete("DeleteNote")]
        public IActionResult DeleteNote(long noteid)
        {
            //var userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
            if (notebusiness.DeleteNote(noteid))
            {
                return Ok(new {status=true,message="success"});
            }
            else
            {
                return BadRequest(new {status=false,message="failure" });
            }
        }
        [HttpPut("UpdateNote")]
        public IActionResult UpdateNote(long Noteid,NoteModel note)
        {
            var updated=notebusiness.UpdateNote(Noteid,note);
            if (updated != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "Updated", Data = updated });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = "Updated failed" });
            }
        }
        [HttpPut("Pin")]
        public IActionResult PinNote(long Noteid)
        {
            var check=notebusiness.PinNote(Noteid);
            if (check)
            {
                return Ok(new {status=true,message="pineed"});
            }
            else
            {
                return Ok(new { status = false, message = "unpinned" });
            }
        }
        [HttpPut("Trash")]
        public IActionResult TrashNote(long Noteid)
        {
            var check=notebusiness.TrashNote(Noteid);
            if (check)
            {
                return Ok(new { status = true, message = "Trashed" });
            }
            else
            {
                return BadRequest(new { status = false, message = "restored" });
            }
        }
        [HttpPut("Archive")]
        public IActionResult ArchiveNote(long Noteid)
        {
            var check=notebusiness.ArchiveNote(Noteid);
            if (check)
            {
                return Ok(new { status = true, message = "Archived" });
            }
            else
            {
                return BadRequest(new { status = false, message = "unarchived" });
            }
        }
    }
}
