using EventLocator.Domain.Models;
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
        public Repository() 
        {
            Tags = [
                new Tag(Guid.NewGuid(), "POPULAR", "red", "This is a very popular event!"),
                new Tag(Guid.NewGuid(), "CLASSIC", "green", "This event is a classic!"),
                new Tag(Guid.NewGuid(), "BIG", "yellow", "This is a big event!"),
                new Tag(Guid.NewGuid(), "ANNUAL", "blue", "This event is held annualy!"),
                new Tag(Guid.NewGuid(), "WORLDWIDE", "yellow", "This is a worldwide event!"),
            ];

            EventTypes = [
                new EventType(Guid.NewGuid(), "MUSIC", "Music festival", "This event involves music", "../../../Resources/Images/add.png"),
                new EventType(Guid.NewGuid(), "CULTURAL", "Cultural festival", "This is a cultural event", "../../../Resources/Images/cancel.png"),
                new EventType(Guid.NewGuid(), "FILM", "Cultural festival", "This is a cultural event", "../../../Resources/Images/clock.png"),
                new EventType(Guid.NewGuid(), "NATURE", "Nature festival", "This is event is held in nature", "../../../Resources/Images/medicament.png"),
                new EventType(Guid.NewGuid(), "MARKET", "Market festival", "This is event is held as a market", "../../../Resources/Images/list.png")
            ];
            Events = [
                new Event(Guid.NewGuid(), "EXIT", "EXIT music festival", "Music festival exit", new EventType(Guid.NewGuid(), "MUSIC", "Music festival", "This event involves music", "icon"), Attendance.OVER_10000, "../../../Resources/Images/add.png", false, 123456.00m, "Srbija", "Novi Sad", [new DateTime(2023, 7, 4)], new DateTime(2024, 7, 6), [new Tag(Guid.NewGuid(), "POPULAR", "red", "This is a very popular event!"), new Tag(Guid.NewGuid(), "ANNUAL", "blue", "This event is held annualy!"),]),
                new Event(Guid.NewGuid(), "NB", "Nocni bazar", "Nocni bazar na ribljoj pijaci", new EventType(Guid.NewGuid(), "MARKET", "Market festival", "This is event is held as a market", "icon"), Attendance.FROM_1000_TO_5000, "ikonica", false, 5670.00m, "Srbija", "Novi Sad", [new DateTime(2024, 2, 19), new DateTime(2024, 3, 8), new DateTime(2024, 4, 19)], new DateTime(2024, 5, 8), [new Tag(Guid.NewGuid(), "CLASSIC", "green", "This event is a classic!")])
            ];
        }
        private static Repository instance;

        private List<Event> _events = [];
        private List<EventType> _eventTypes = [];
        private List<Tag> _tags = [];
        public static Repository Instance 
        {
            get
            {
                instance ??= new Repository();
                return instance;
            }
        }

        public List<Event> Events
        {
            get
            {
                return _events;
            }
            set
            {
                _events = value;
            }
        }

        public List<EventType> EventTypes
        {
            get
            {
                return _eventTypes;
            }
            set
            {
                _eventTypes = value;
            }
        }

        public List<Tag> Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                _tags = value;
            }
        }
        
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

            foreach(EventType type in EventTypes)
            {
                ComboBoxData<EventType> option = new(type.Label, type);
                options.Add(option);
            }

            return options;
        }

        public List<ComboBoxData<Tag>> tagDropdownOptions()
        {
            List<ComboBoxData<Tag>> options = [];

            foreach (Tag tag in Tags)
            {
                ComboBoxData<Tag> option = new(tag.Label, tag);
                options.Add(option);
            }

            return options;
        }

        public void AddEvent(Event e)
        {
            Events.Add(e);
        }
        public void EditEvent(Event e)
        {
            
        }
        public void RemoveEvent(Guid id)
        {
            Event? eventToRemove = Events.Find(foundEvent => foundEvent.Id == id);
            if (eventToRemove != null)
            {
                Events.Remove(eventToRemove);
            }
            else
            {
                throw new NullReferenceException("There was no event found with the given Id!");
            }
        }

        public void AddEventType(EventType type)
        {
            EventTypes.Add(type);
        }
        public void EditEventType(EventType type)
        {
            EventType? oldEventType = EventTypes.Find(eventType => eventType.Id == type.Id);

            if (oldEventType != null)
            {
                oldEventType.Label = type.Label;
                oldEventType.Name = type.Name;
                oldEventType.Description = type.Description;
                oldEventType.IconUrl = type.IconUrl;
            }
        }
        public void RemoveEventType(Guid id)
        {
            EventType? eventTypeToRemove = EventTypes.Find(foundEventType => foundEventType.Id == id);
            if (eventTypeToRemove != null)
            {
                EventTypes.Remove(eventTypeToRemove);
            }
            else
            {
                throw new NullReferenceException("There was no event type found with the given Id!");
            }
        }

        public void AddTag(Tag tag)
        {
            Tags.Add(tag);
        }
        public void EditTag(Tag tag)
        {
            Tag? oldTag = Tags.Find(foundTag => foundTag.Id == tag.Id);
            if(oldTag != null)
            {
                oldTag.Label = tag.Label;
                oldTag.Color = tag.Color;
                oldTag.Description = tag.Description;
            }
        }
        public void RemoveTag(Guid id)
        {
            Tag? tagToRemove = Tags.Find(foundTag => foundTag.Id == id);
            if(tagToRemove != null)
            {
                Tags.Remove(tagToRemove);
            }
            else
            {
                throw new NullReferenceException("There was no tag found with the given Id!");
            }
        }
    }
}
