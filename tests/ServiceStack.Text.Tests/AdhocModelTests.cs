using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using NUnit.Framework;
using ServiceStack.Common.Extensions;
using ServiceStack.Text.Jsv;

namespace ServiceStack.Text.Tests
{
    [TestFixture]
    public class AdhocModelTests
        : TestBase
    {
        public enum FlowPostType
        {
            Content,
            Text,
            Promo,
        }

        public class FlowPostTransient
        {
            public FlowPostTransient()
            {
                this.TrackUrns = new List<string>();
            }

            public long Id { get; set; }

            public string Urn { get; set; }

            public Guid UserId { get; set; }

            public DateTime DateAdded { get; set; }

            public DateTime DateModified { get; set; }

            public Guid? TargetUserId { get; set; }

            public long? ForwardedPostId { get; set; }

            public Guid OriginUserId { get; set; }

            public string OriginUserName { get; set; }

            public Guid SourceUserId { get; set; }

            public string SourceUserName { get; set; }

            public string SubjectUrn { get; set; }

            public string ContentUrn { get; set; }

            public IList<string> TrackUrns { get; set; }

            public string Caption { get; set; }

            public Guid CaptionUserId { get; set; }

            public string CaptionSourceName { get; set; }

            public string ForwardedPostUrn { get; set; }

            public FlowPostType PostType { get; set; }

            public Guid? OnBehalfOfUserId { get; set; }

            public static FlowPostTransient Create()
            {
                return new FlowPostTransient
                {
                    Caption = "Caption",
                    CaptionSourceName = "CaptionSourceName",
                    CaptionUserId = Guid.NewGuid(),
                    ContentUrn = "ContentUrn",
                    DateAdded = DateTime.Now,
                    DateModified = DateTime.Now,
                    ForwardedPostId = 1,
                    ForwardedPostUrn = "ForwardedPostUrn",
                    Id = 1,
                    OnBehalfOfUserId = Guid.NewGuid(),
                    OriginUserId = Guid.NewGuid(),
                    OriginUserName = "OriginUserName",
                    PostType = FlowPostType.Content,
                    SourceUserId = Guid.NewGuid(),
                    SourceUserName = "SourceUserName",
                    SubjectUrn = "SubjectUrn ",
                    TargetUserId = Guid.NewGuid(),
                    TrackUrns = new List<string> { "track1", "track2" },
                    Urn = "Urn ",
                    UserId = Guid.NewGuid(),
                };
            }

            public bool Equals(FlowPostTransient other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return other.Id == Id && Equals(other.Urn, Urn) && other.UserId.Equals(UserId) && other.DateAdded.RoundToMs().Equals(DateAdded.RoundToMs()) && other.DateModified.RoundToMs().Equals(DateModified.RoundToMs()) && other.TargetUserId.Equals(TargetUserId) && other.ForwardedPostId.Equals(ForwardedPostId) && other.OriginUserId.Equals(OriginUserId) && Equals(other.OriginUserName, OriginUserName) && other.SourceUserId.Equals(SourceUserId) && Equals(other.SourceUserName, SourceUserName) && Equals(other.SubjectUrn, SubjectUrn) && Equals(other.ContentUrn, ContentUrn) && TrackUrns.EquivalentTo(other.TrackUrns) && Equals(other.Caption, Caption) && other.CaptionUserId.Equals(CaptionUserId) && Equals(other.CaptionSourceName, CaptionSourceName) && Equals(other.ForwardedPostUrn, ForwardedPostUrn) && Equals(other.PostType, PostType) && other.OnBehalfOfUserId.Equals(OnBehalfOfUserId);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof(FlowPostTransient)) return false;
                return Equals((FlowPostTransient)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int result = Id.GetHashCode();
                    result = (result * 397) ^ (Urn != null ? Urn.GetHashCode() : 0);
                    result = (result * 397) ^ UserId.GetHashCode();
                    result = (result * 397) ^ DateAdded.GetHashCode();
                    result = (result * 397) ^ DateModified.GetHashCode();
                    result = (result * 397) ^ (TargetUserId.HasValue ? TargetUserId.Value.GetHashCode() : 0);
                    result = (result * 397) ^ (ForwardedPostId.HasValue ? ForwardedPostId.Value.GetHashCode() : 0);
                    result = (result * 397) ^ OriginUserId.GetHashCode();
                    result = (result * 397) ^ (OriginUserName != null ? OriginUserName.GetHashCode() : 0);
                    result = (result * 397) ^ SourceUserId.GetHashCode();
                    result = (result * 397) ^ (SourceUserName != null ? SourceUserName.GetHashCode() : 0);
                    result = (result * 397) ^ (SubjectUrn != null ? SubjectUrn.GetHashCode() : 0);
                    result = (result * 397) ^ (ContentUrn != null ? ContentUrn.GetHashCode() : 0);
                    result = (result * 397) ^ (TrackUrns != null ? TrackUrns.GetHashCode() : 0);
                    result = (result * 397) ^ (Caption != null ? Caption.GetHashCode() : 0);
                    result = (result * 397) ^ CaptionUserId.GetHashCode();
                    result = (result * 397) ^ (CaptionSourceName != null ? CaptionSourceName.GetHashCode() : 0);
                    result = (result * 397) ^ (ForwardedPostUrn != null ? ForwardedPostUrn.GetHashCode() : 0);
                    result = (result * 397) ^ PostType.GetHashCode();
                    result = (result * 397) ^ (OnBehalfOfUserId.HasValue ? OnBehalfOfUserId.Value.GetHashCode() : 0);
                    return result;
                }
            }
        }

        [Test]
        public void Can_Deserialize_text()
        {
            var dtoString = "[{Id:1,Urn:urn:post:3a944f18-920c-498a-832d-cf38fed3d0d7/1,UserId:3a944f18920c498a832dcf38fed3d0d7,DateAdded:2010-02-17T12:04:45.2845615Z,DateModified:2010-02-17T12:04:45.2845615Z,OriginUserId:3a944f18920c498a832dcf38fed3d0d7,OriginUserName:testuser1,SourceUserId:3a944f18920c498a832dcf38fed3d0d7,SourceUserName:testuser1,SubjectUrn:urn:track:1,ContentUrn:urn:track:1,TrackUrns:[],CaptionUserId:3a944f18920c498a832dcf38fed3d0d7,CaptionSourceName:testuser1,PostType:Content}]";
            var fromString = TypeSerializer.DeserializeFromString<List<FlowPostTransient>>(dtoString);
        }

        [Test]
        public void Can_Serialize_single_FlowPostTransient()
        {
            var dto = FlowPostTransient.Create();
            SerializeAndCompare(dto);
        }

        [Test]
        public void Can_serialize_jsv_dates()
        {
            var now = DateTime.Now;

            var jsvDate = TypeSerializer.SerializeToString(now);
            var fromJsvDate = TypeSerializer.DeserializeFromString<DateTime>(jsvDate);
            Assert.That(fromJsvDate, Is.EqualTo(now));
        }

        [Test]
        public void Can_serialize_json_dates()
        {
            var now = DateTime.Now;

            var jsonDate = JsonSerializer.SerializeToString(now);
            var fromJsonDate = JsonSerializer.DeserializeFromString<DateTime>(jsonDate);

            Assert.That(fromJsonDate.RoundToMs(), Is.EqualTo(now.RoundToMs()));
        }

        [Test]
        public void Can_Serialize_multiple_FlowPostTransient()
        {
            var dtos = new List<FlowPostTransient> {
				FlowPostTransient.Create(), 
				FlowPostTransient.Create()
			};
            Serialize(dtos);
        }

        [DataContract]
        public class TestObject
        {
            [DataMember]
            public string Value { get; set; }
            public TranslatedString ValueNoMember { get; set; }

            public bool Equals(TestObject other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Equals(other.Value, Value);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof(TestObject)) return false;
                return Equals((TestObject)obj);
            }

            public override int GetHashCode()
            {
                return (Value != null ? Value.GetHashCode() : 0);
            }
        }

        public class Test
        {
            public string Val { get; set; }
        }

        public class TestResponse
        {
            public TestObject Result { get; set; }
        }

        public class TranslatedString : ListDictionary
        {
            public string CurrentLanguage { get; set; }

            public string Value
            {
                get
                {
                    if (this.Contains(CurrentLanguage))
                        return this[CurrentLanguage] as string;
                    return null;
                }
                set
                {
                    if (this.Contains(CurrentLanguage))
                        this[CurrentLanguage] = value;
                    else
                        Add(CurrentLanguage, value);
                }
            }

            public TranslatedString()
            {
                CurrentLanguage = "en";
            }

            public static void SetLanguageOnStrings(string lang, params TranslatedString[] strings)
            {
                foreach (TranslatedString str in strings)
                    str.CurrentLanguage = lang;
            }
        }

        [Test]
        public void Should_ignore_non_DataMember_TranslatedString()
        {
            var dto = new TestObject
            {
                Value = "value",
                ValueNoMember = new TranslatedString
                {
                    {"key1", "val1"},
                    {"key2", "val2"},
                }
            };
            SerializeAndCompare(dto);
        }
        
        public interface IParent
        {
            int Id { get; set; }
            string ParentName { get; set; }
        }

        public class Parent : IParent
        {
            public int Id { get; set; }
            public string ParentName { get; set; }
            public Child Child { get; set; }
        }

        public class Child
        {
            public int Id { get; set; }
            public string ChildName { get; set; }
            public IParent Parent { get; set; }
        }

        [Test]
        public void Can_Serailize_Cyclical_Dependency_via_interface()
        {
            var dto = new Parent
            {
                Id = 1,
                ParentName = "Parent",
                Child = new Child { Id = 2, ChildName = "Child" }
            };
            dto.Child.Parent = dto;

            var fromDto = Serialize(dto, includeXml:false);

            var parent = (Parent)fromDto.Child.Parent;
            Assert.That(parent.Id, Is.EqualTo(dto.Id));
            Assert.That(parent.ParentName, Is.EqualTo(dto.ParentName));
        }

    }
}
