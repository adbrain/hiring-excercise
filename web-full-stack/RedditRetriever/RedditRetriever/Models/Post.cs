using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RedditRetriever.Models
{
    public class Post
    {
        [Key]
        public string CallId { get; set; }
        public string Domain { get; set; }
        [JsonProperty("banned_by")]
        public object BannedBy { get; set; }
        public string Subreddit { get; set; }
        [JsonProperty("selftext_html")]
        public string SelftextHtml { get; set; }
        public string Selftext { get; set; }
        public object Likes { get; set; }
        [JsonProperty("suggested_sort")]
        public object SuggestedSort { get; set; }
        [JsonProperty("user_reports")]
        public List<object> UserReports { get; set; }
        [JsonProperty("link_flair_text")]
        public string LinkFlairText { get; set; }
        [Key]
        public string Id { get; set; }
        [JsonProperty("from_kind")]
        public object FromKind { get; set; }
        public int Gilded { get; set; }
        public bool Archived { get; set; }
        public bool Clicked { get; set; }
        [JsonProperty("report_reasons")]
        public object ReportReasons { get; set; }
        public string Author { get; set; }
        public int Score { get; set; }
        [JsonProperty("approved_by")]
        public object ApprovedBy { get; set; }
        [JsonProperty("over_18")]
        public bool Over18 { get; set; }
        public bool Hidden { get; set; }
        [JsonProperty("num_comments")]
        public int NumComments { get; set; }
        public string Thumbnail { get; set; }
        [JsonProperty("subreddit_id")]
        public string SubredditId { get; set; }
        public object Edited { get; set; }
        [JsonProperty("link_flair_css_class")]
        public string LinkFlairCssClass { get; set; }
        [JsonProperty("author_flair_css_class")]
        public string AuthorFlairCssClass { get; set; }
        public int Downs { get; set; }
        public bool Saved { get; set; }
        [JsonProperty("removal_reason")]
        public object RemovalReason { get; set; }
        public bool Stickied { get; set; }
        public object From { get; set; }
        [JsonProperty("is_self")]
        public bool IsSelf { get; set; }
        [JsonProperty("from_id")]
        public object FromId { get; set; }
        public string Permalink { get; set; }
        public string Name { get; set; }
        public double Created { get; set; }
        public string Url { get; set; }
        [JsonProperty("author_flair_text")]
        public string AuthorFlairText { get; set; }
        public string Title { get; set; }
        [JsonProperty("created_utc")]
        public double CreatedUtc { get; set; }
        public string Distinguished { get; set; }
        [JsonProperty("mod_reports")]
        public List<object> ModReports { get; set; }
        public bool Visited { get; set; }
        [JsonProperty("num_reports")]
        public object NumReports { get; set; }
        public int Ups { get; set; }
        [JsonProperty("post_hint")]
        public string PostHint { get; set; }
    }
}
