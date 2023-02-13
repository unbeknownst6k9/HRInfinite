

/*
 * purpose: this is a struct that only stores data when resume object is generated
 */
namespace DataStruct
{
    public struct Education
    {
        public string name { get; set; }
        public int yearStart { get; set; }
        public int yearEnd() { return yearStart + 4; }
    }
}
