namespace MusicViewer
{
    public partial class Author
    {
        public override string ToString()
        {
            return this.Name;
        }
    }

    public partial class Music
    {
        public override string ToString()
        {
            return this.Title;
        }
    }

    //public partial class MusicFile
    //{
    //    public override string ToString()
    //    {
    //        return this.mp3.Length.ToString();
    //    }
    //}
}
