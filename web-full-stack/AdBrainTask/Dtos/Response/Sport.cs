using System;

namespace AdBrainTask.Dtos.Response
{
    public class Sport
    {
        public string Id { get; set; }

        public string Author { get; set; }

        public string Domain { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime DateCreated { get; set; }
    }
}