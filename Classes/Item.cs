namespace CharacterCreator.Classes
{
    class Item: Equipment
    {
        public Item() { }

        public Item(string name, string type, int quanity)
        {
            this.Name = name;
            this.Type = type;
            this.Quanity = quanity;
        }

        public override string ItemDetails()
        {
            return "This is just a regular item. Nothig special about it. I am serious. Go away.";
        }
    }
}
