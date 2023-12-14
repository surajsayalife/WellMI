﻿namespace WellMI.Models
{
    public partial class EmailHistory
    {
        public long Id { get; set; }
        public string From { get; set; }
        public string FromName { get; set; }
        public string To { get; set; }
        public string ToName { get; set; }
        public string ReplyTo { get; set; }
        public string ReplyToName { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentFilePath { get; set; }
        public string AttachmentFileName { get; set; }
        public string AttachedDownloadId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsSent { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
