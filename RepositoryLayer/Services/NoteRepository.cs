using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRepository: INoteRepository
    {
        private readonly FundooDBContext context;
        private readonly IConfiguration configuration;

        public NoteRepository(FundooDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public NoteEntity AddNote(NoteModel note,long userId)
        {
            NoteEntity noteEntity = new NoteEntity();
            noteEntity.Title= note.Title;
            noteEntity.Description= note.Description;
            noteEntity.Color= note.Color;
            noteEntity.Image= note.Image;
            noteEntity.Reminder= note.Reminder;
            noteEntity.IsArchived= note.IsArchived;
            noteEntity.IsPinned= note.IsPinned;
            noteEntity.IsTrash= note.IsTrash;
            noteEntity.CreatedAt= DateTime.Now;
            noteEntity.ModifiededAt= DateTime.Now;
            noteEntity.UserId= userId;
            context.NoteTable.Add(noteEntity);
            int result=context.SaveChanges();
            if (result > 0)
            {
                return noteEntity;
            }
            else
            {
                return null;
            }
        }
        public List<NoteEntity> GetAllNotesbyid(long userId) 
        {
            var result = context.NoteTable.Where(a => a.UserId == userId).ToList();
            return result;
        }
        public bool DeleteNote(long noteId)
        {
            try
            {
                NoteEntity result = context.NoteTable.FirstOrDefault(x => x.NoteId == noteId);

                if (result != null)
                {
                    context.NoteTable.Remove(result);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity UpdateNote(long noteId, NoteModel notesModel)
        {
            try
            {
                var noteEntity = context.NoteTable.FirstOrDefault(e => e.NoteId == noteId);
                if (noteEntity != null)
                {
                    noteEntity.Title = notesModel.Title;
                    noteEntity.Description = notesModel.Description;
                    noteEntity.Reminder = notesModel.Reminder;
                    noteEntity.Color = notesModel.Color;
                    noteEntity.Image = notesModel.Image;
                    noteEntity.IsArchived = notesModel.IsArchived;
                    noteEntity.IsPinned = notesModel.IsPinned;
                    noteEntity.IsTrash = notesModel.IsTrash;
                    noteEntity.CreatedAt = notesModel.CreatedAt;
                    noteEntity.ModifiededAt = notesModel.ModifiededAt;

                    context.SaveChanges();
                    return noteEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool PinNote(long noteId)
        {
            var result=context.NoteTable.FirstOrDefault(x=> x.NoteId == noteId);
            if(result.IsPinned)
            {
                result.IsPinned = false;
                context.SaveChanges();
                return false;
            }
            else
            {
                result.IsPinned = true;
                context.SaveChanges();
                return true;
            }
        }
        public bool TrashNote(long noteId)
        {
            var result = context.NoteTable.FirstOrDefault(x => x.NoteId == noteId);
            if (result.IsTrash)
            {
                result.IsTrash = false;
                context.SaveChanges();
                return false;
            }
            else
            {
                result.IsTrash = true;
                context.SaveChanges();
                return true;
            }
        }
        public bool ArchiveNote(long noteId)
        {
            var result = context.NoteTable.FirstOrDefault(x => x.NoteId == noteId);
            if (result.IsArchived)
            {
                result.IsArchived = false;
                context.SaveChanges();
                return false;
            }
            else
            {
                result.IsArchived = true;
                context.SaveChanges();
                return true;
            }
        }
        public List<NoteEntity> GetAllNotes()
        {
            var list=context.NoteTable.ToList();
            return list;
        }
    }
}
