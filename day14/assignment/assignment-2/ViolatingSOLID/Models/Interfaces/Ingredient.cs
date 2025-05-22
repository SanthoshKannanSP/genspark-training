namespace assignment_2.ViolatingSOLID.Models.Interfaces
{
    public interface Ingredient
    {
        public string Name { get; }
        public int Cost { get; }
        public void AddIcecream();
        public void AddTopping();
    }
}
