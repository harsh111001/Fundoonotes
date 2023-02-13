using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entity;
using ModelLayer;
using System.Linq;

namespace RepositoryLayer.Services
{
    public class LabelRepository:ILabelRepository
    {
        private readonly FundooDBContext context;
        private readonly IConfiguration configuration;
        public LabelRepository(FundooDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public Label AddLabel(LabelModel label, long noteid, long userid)
        {
            Label addlabel = new Label();
            addlabel.Userid= userid;
            addlabel.LabelName= label.LabelName;
            addlabel.NoteId= noteid;
            context.LabelTable.Add(addlabel);
            context.SaveChanges();
            return addlabel;
        }
        public List<Label> GetLabel(long noteid,long userid)
        {
            var result=context.LabelTable.Where(a=>a.Userid==userid && a.NoteId==noteid).ToList();
            return result;
        }
    }
}
