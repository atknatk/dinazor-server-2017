

namespace Dinazor.Core.Model
{
    public class PagingRequest
    {
        public int draw { get; set; }
        public PagingRequestColumn[] columns { get; set; }
        public PagingRequestOrder[] order { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public PagingRequestSearch search { get; set; }

    }

    public class PagingRequestColumn
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public PagingRequestSearch search { get; set; }

    }

    public class PagingRequestSearch
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }

    public class PagingRequestOrder
    {
        public int column { get; set; }
        //asc desc
        public string dir { get; set; }
    }
}
