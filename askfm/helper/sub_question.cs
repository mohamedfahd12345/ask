namespace askfm.helper
{
    public class sub_question
    {
        public int question_id { get; set; }
        public string question_description { get; set; }
        public string main_answer { get; set; }
        public List<string> answers = new List<string>();
        public string ask_descripion { get; set; }
    }
}
