using System;

namespace Station.Entity.DB2AdminPattern
{
    public class EditorEntity
    {
        public DateTime CreateDate { get; set; }

        public string Creator { get; set; }

        public DateTime UpdateDate { get; set; }

        public string Updater { get; set; }
    }
}