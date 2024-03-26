using Contracts;

while (true)
{
    Console.Write("Write expression: ");
    string expression = Console.ReadLine();
    try
    {


        Calculator calculator = new Calculator();
        Console.Write($"Expression: {expression} = ");
        //double result = calculator.retRes;
        Console.WriteLine(await calculator.Evaluate(expression));
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}
//Console.WriteLine("Enter an expression:");
