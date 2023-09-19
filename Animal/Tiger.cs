namespace Animal
{
    public class Tiger : Felime
    {
        public Tiger(string name, double weight, string region)
            : base(name, "Tiger", weight, region) { }

        public override void Eat(Food food)
        {
            if (food is Meat)
            {
                _foodEaten += food.Quantity;
            }
            else
            {
                Console.WriteLine($"{GetType().Name}s are not eating that type of food!");
            }
        }

        public override void MakeSound()
        {
            Console.WriteLine("rrrr");
        }
    }
}
