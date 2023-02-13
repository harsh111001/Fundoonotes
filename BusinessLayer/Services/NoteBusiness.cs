using BusinessLayer.Interfaces;
using ModelLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBusiness:INoteBusiness
    {
        private readonly INoteRepository noterepo;
        public NoteBusiness(INoteRepository noterepo)
        {
            this.noterepo = noterepo;
        }
        public NoteEntity AddNote(NoteModel note, long userId)
        {
            return noterepo.AddNote(note, userId);
        }
        public List<NoteEntity> GetAllNotesbyid(long userId)
        {
            return noterepo.GetAllNotesbyid(userId);
        }
        public bool DeleteNote(long noteId)
        {
            return noterepo.DeleteNote(noteId);
        }
        public NoteEntity UpdateNote(long noteId, NoteModel notesModel)
        {
            return noterepo.UpdateNote(noteId, notesModel);
        }
        public bool PinNote(long noteId)
        {
            return noterepo.PinNote(noteId);
        }
        public bool TrashNote(long noteId)
        {
            return noterepo.TrashNote(noteId);
        }
        public bool ArchiveNote(long noteId)
        {
            return noterepo.ArchiveNote(noteId);
        }
        public List<NoteEntity> GetAllNotes()
        {
            return noterepo.GetAllNotes();
        }
    }
}
