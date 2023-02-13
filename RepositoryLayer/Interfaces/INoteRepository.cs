using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRepository
    {
        public NoteEntity AddNote(NoteModel note, long userId);
        public List<NoteEntity> GetAllNotesbyid(long userId);
        public bool DeleteNote(long noteId);
        public NoteEntity UpdateNote(long noteId, NoteModel notesModel);
        public bool PinNote(long noteId);
        public bool TrashNote(long noteId);
        public bool ArchiveNote(long noteId);
        public List<NoteEntity> GetAllNotes();
    }
}
