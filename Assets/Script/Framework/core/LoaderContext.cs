namespace Framework.core
{
    public class LoaderContext
    {
        private string path;

        public string Path
        {
            get { return path; }
        }

        private int contextId;

        public int ContextId
        {
            get { return contextId; }
        }

        public LoaderContext(string path, int contextId)
        {
            this.path = path;
            this.contextId = contextId;
        }
    }
}