using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness labelbusiness;
        public LabelController(ILabelBusiness labelbusiness)
        {
            this.labelbusiness = labelbusiness;
        }
        [Authorize]
        [HttpPost("AddLabel")]
        public IActionResult AddLabel(LabelModel label,long noteid)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
            var createlabel=labelbusiness.AddLabel(label, noteid,userId);
            return Ok(createlabel);
        }
        [Authorize]
        [HttpGet("GetLabel")]
        public IActionResult Getlabel(long noteid)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
            var result = labelbusiness.GetLabel(noteid, userId);
            return Ok(result);
        }
    }
}
