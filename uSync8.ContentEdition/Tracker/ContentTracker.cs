﻿using System.Collections.Generic;

using Umbraco.Core.Models;

using uSync8.Core.Serialization;
using uSync8.Core.Tracking;

namespace uSync8.ContentEdition.Tracker
{
    public class ContentTracker : SyncBaseTracker<IContent>, ISyncNodeTracker<IContent>
    {
        public ContentTracker(ISyncSerializer<IContent> serializer) : base(serializer)
        {
        }

        protected override TrackedItem TrackChanges()
        {
            return new TrackedItem(serializer.ItemType, true)
            {
                Children = new List<TrackedItem>()
                {
                    new TrackedItem("", "/Info")
                    {
                        Children = new List<TrackedItem>()
                        {
                            new TrackedItem("Parent", "/Parent", true),
                            new TrackedItem("Path", "/Path", true),
                            new TrackedItem("SortOrder", "/SortOrder", true),
                            new TrackedItem("FileHash", "/FileHash", true),
                            new TrackedItem("Template", "/Template", true),
                            new TrackedItem("CreateDate", "/CreateDate", true),

                            new TrackedItem("Name", "/NodeName", false)
                            {
                                Attributes = new List<string>() { "Default" },
                                Children = new List<TrackedItem>()
                                {
                                    new TrackedItem("", "/Name")
                                    {
                                        Repeating = new RepeatingInfo("Culture", string.Empty, "Culture")
                                        {
                                            KeyIsAttribute = true,
                                            NameIsAttribute = true
                                        }
                                    }
                                }
                            },

                            new TrackedItem("Published", "/Published")
                            {
                                Attributes = new List<string>() { "Default" },
                                Children = new List<TrackedItem>()
                                {
                                    new TrackedItem("", "/Published")
                                    {
                                        Repeating = new RepeatingInfo("Culture", string.Empty, "Culture")
                                        {
                                            KeyIsAttribute = true,
                                            NameIsAttribute = true
                                        }
                                    }
                                }
                            },
                            new TrackedItem("Schedule", "/Schedule")
                            {
                                Children = new List<TrackedItem>()
                                {
                                    new TrackedItem("", "/ContentSchedule")
                                    {
                                        Repeating = new RepeatingInfo("Culture", "/ContentSchedule", "Culture"){
                                            KeyIsAttribute = false,
                                            MatchingKeys = new List<RepeatingKey>()
                                            {
                                                new RepeatingKey { Key = "Culture", IsAttribute = false},
                                                new RepeatingKey { Key = "Action", IsAttribute = false}
                                            },
                                        },
                                        Children = new List<TrackedItem>()
                                        {
                                            new TrackedItem("Culture", "/Culture", true),
                                            new TrackedItem("Date", "/Date", true),
                                            new TrackedItem("Action", "/Action", true)
                                        }
                                    }
                                }
                            }
                        }
                    },

                    new TrackedItem("Property", "/Properties")
                    {
                        HasChildProperties = true,
                        Children = new List<TrackedItem>()
                        {
                            new TrackedItem("", "/Value")
                            {
                                Repeating = new RepeatingInfo(string.Empty, string.Empty,string.Empty)
                                {
                                    ElementsInOrder = true
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}