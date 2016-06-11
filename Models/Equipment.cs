namespace CharacterCreator.Classes
{
    public class Equipment
    {
        public string Type { get; set; }
        public int Quanity { get; set; }
        public string Name { get; set; }

        public Equipment() { }
        public Equipment(string type) { this.Type = type; }

        public virtual string ItemDetails()
        {
            return "I don't how you were able to view this.. um.. congrats, I guess. Now go away.";
        }
    }
}
