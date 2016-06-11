namespace CharacterCreator.Interfaces
{
    public interface IViewableCharacter
    {
        /// <summary>
        /// Takes CharacterIndex and checks if the character exists in the CharacterList.
        /// </summary>
        void ViewableCharacter(int characterIndex);
    }
}
