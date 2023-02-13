using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBusiness
    {
        public Label AddLabel(LabelModel label, long noteid,long userid);
        public List<Label> GetLabel(long noteid, long userid);
    }
}
