using BusinessLayer.Interfaces;
using ModelLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBusiness:ILabelBusiness
    {
        private readonly ILabelRepository labelrepo;
        public LabelBusiness (ILabelRepository labelrepo)
        {
            this.labelrepo = labelrepo;
        }
        public Label AddLabel(LabelModel label, long noteid, long userid)
        {
            return labelrepo.AddLabel(label, noteid,userid);
        }
        public List<Label> GetLabel(long noteid, long userid)
        {
            return labelrepo.GetLabel(noteid,userid);
        }
    }
}
