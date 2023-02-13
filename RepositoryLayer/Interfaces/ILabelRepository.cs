using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRepository
    {
        public List<Label> GetLabel(long noteid, long userid);
        public Label AddLabel(LabelModel label, long noteid, long userid);
    }
}
