using EventLocator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Data
{
    public class Repository
    {
        private readonly EventLocatorDbContext _dbContext;
        private static Repository instance;
        public static Repository Instance
        {
            get
            {
                instance ??= new Repository();
                return instance;
            }
        }
        public Repository() 
        {
            _dbContext = new EventLocatorDbContext();
        }
        #region events
        public IEnumerable<Event> GetAllEvents()
        {
            return _dbContext.Events.Include(e=> e.Tags).ToList();
        }
        public Event? GetEventById(Guid id)
        {
            return _dbContext.Events.Find(id);
        }
        public void AddEvent(Event e)
        {    
            _dbContext.Events.Add(e);
            _dbContext.SaveChanges();
        }
        public bool EditEvent(Event e)
        {
            Event? eventToEdit = _dbContext.Events.Find(e.Id);
            if(eventToEdit != null)
            {
                eventToEdit.Label = e.Label;
                eventToEdit.Name = e.Name;
                eventToEdit.Description = e.Description;
                eventToEdit.Type = e.Type;
                eventToEdit.Attendance = e.Attendance;
                eventToEdit.IconUrl = e.IconUrl;
                eventToEdit.IsCharity = e.IsCharity;
                eventToEdit.AverageHostingExpenses = e.AverageHostingExpenses;
                eventToEdit.Country = e.Country;
                eventToEdit.City = e.City;
                eventToEdit.PreviousEventDates = e.PreviousEventDates;
                eventToEdit.EventDate = e.EventDate;
                eventToEdit.Tags = e.Tags;

                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteEvent(Guid id)
        {
            Event? eventToDelete = _dbContext.Events.Find(id);
            if (eventToDelete != null)
            {
                _dbContext.Events.Remove(eventToDelete);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion events
        #region eventTypes
        public IEnumerable<EventType> GetAllEventTypes()
        {
            return _dbContext.EventTypes.ToList();
        }

        public EventType? GetEventTypeById(Guid id)
        {
            return _dbContext.EventTypes.Find(id);
        }

        public void AddEventType(EventType eventType)
        {
            _dbContext.EventTypes.Add(eventType);
            _dbContext.SaveChanges();
        }

        public bool EditEventType(EventType eventType)
        {
            EventType? eventTypeToUpdate = _dbContext.EventTypes.Find(eventType.Id);
            if (eventTypeToUpdate != null)
            {
                eventTypeToUpdate.Label = eventType.Label;
                eventTypeToUpdate.Name = eventType.Name;
                eventTypeToUpdate.Description = eventType.Description;
                eventTypeToUpdate.IconUrl = eventType.IconUrl;
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteEventType(Guid id)
        {
            EventType? eventTypeToDelete = _dbContext.EventTypes.Find(id);
            if (eventTypeToDelete != null)
            {
                _dbContext.EventTypes.Remove(eventTypeToDelete);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion eventTypes
        #region tags
        public IEnumerable<Tag> GetAllTags()
        {
            return _dbContext.Tags.ToList();
        }

        public Tag? GetTagById(Guid id)
        {
            return _dbContext.Tags.Find(id);
        }

        public void AddTag(Tag tag)
        {
            _dbContext.Tags.Add(tag);
            _dbContext.SaveChanges();
        }

        public bool EditTag(Tag tag)
        {
            Tag? tagToEdit = _dbContext.Tags.Find(tag.Id);
            if (tagToEdit != null)
            {
                tagToEdit.Label = tag.Label;
                tagToEdit.Description = tag.Description;
                tagToEdit.Color = tag.Color;

                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTag(Guid id)
        {
            Tag? tagToDelete = _dbContext.Tags.Find(id);
            if (tagToDelete != null)
            {
                _dbContext.Tags.Remove(tagToDelete);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion tags
        #region dropdowns
        public List<ComboBoxData<Attendance>> attendanceDropdownOptions()
        {
            return
            [
                new ComboBoxData<Attendance>("< 1000", Attendance.FROM_1000_TO_5000),
                new ComboBoxData<Attendance>("1.000 - 5.000", Attendance.FROM_5000_TO_10000),
                new ComboBoxData<Attendance>("5000 - 10.000", Attendance.FROM_5000_TO_10000),
                new ComboBoxData<Attendance>("> 10.000", Attendance.OVER_10000)
            ];
        }
        public List<ComboBoxData<EventType>> eventTypeDropdownOptions()
        {
            List<ComboBoxData<EventType>> options = [];
            List<EventType> eventTypes = new(GetAllEventTypes());

            foreach (EventType type in eventTypes)
            {
                ComboBoxData<EventType> option = new(type.Label, type);
                options.Add(option);
            }
            return options;
        }
        public List<ComboBoxData<Tag>> tagDropdownOptions()
        {
            List<ComboBoxData<Tag>> options = [];
            List<Tag> tags = new(GetAllTags());

            foreach (Tag tag in tags)
            {
                ComboBoxData<Tag> option = new(tag.Label, tag);
                options.Add(option);
            }

            return options;
        }
        #endregion dropdowns
    }
}
