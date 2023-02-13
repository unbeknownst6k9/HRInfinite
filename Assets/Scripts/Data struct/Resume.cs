/*
 * purpose: This is a single struct that stores all the data for Resume
 */
namespace DataStruct
{
    public struct Resume
    {
        public string firstName { get; set; }
        public string realFirstName { get; set; }
        public string lastName { get; set; }
        public string realLastName { get; set; }
        public int age { get; set; }
        public int realAge { get; set; }
        public string email { get; set; }
        public string realEmail { get; set; }
        public Education[] Educations;
        public Skill[] Skills;
        public string description { get; set; }
        public float score { get; set; }
    }
}